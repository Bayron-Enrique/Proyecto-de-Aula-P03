using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    /// <summary>Adaptador de Telegram; las consultas y reglas se resuelven en LOGICA.</summary>
    public sealed class BotTelegram
    {
        private readonly TelegramBotClient cliente;
        private readonly VinculacionTelegramService vinculacion;
        private readonly AgendaService agendaService;
        private readonly VehiculoService vehiculoService;
        // Guarda en qué paso está cada chat: cédula o código.
        private readonly List<Conversacion> conversaciones = new List<Conversacion>();
        private readonly object candadoConversaciones = new object();

        public BotTelegram(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("El token de Telegram es obligatorio.", "token");

            var clientes = new ClienteRepository();
            var vehiculos = new VehiculoRepository();
            vinculacion = new VinculacionTelegramService(clientes, new Email());
            agendaService = new AgendaService(new AgendaRepository(), clientes, vehiculos,
                new EmpleadoRepository(), new ArticuloRepository());
            vehiculoService = new VehiculoService(vehiculos, clientes);
            cliente = new TelegramBotClient(token);
        }

        public void Iniciar(CancellationToken cancellationToken)
        {
            var opciones = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery }
            };

            cliente.StartReceiving(ProcesarActualizacionAsync, ProcesarErrorAsync, opciones, cancellationToken);
        }

        private async Task ProcesarActualizacionAsync(ITelegramBotClient bot, Update actualizacion, CancellationToken cancellationToken)
        {
            if (actualizacion.Type == UpdateType.Message && actualizacion.Message != null)
            {
                Message mensaje = actualizacion.Message;
                string texto = mensaje.Text;
                if (string.IsNullOrWhiteSpace(texto)) return;

                if (texto.StartsWith("/start", StringComparison.OrdinalIgnoreCase) ||
                    texto.StartsWith("/menu", StringComparison.OrdinalIgnoreCase))
                {
                    await bot.SendMessage(mensaje.Chat.Id,
                        "¡Hola! 👋 Soy el asistente de GOT-AUTO. ¿Qué necesitas consultar?",
                        replyMarkup: CrearMenu(), cancellationToken: cancellationToken);
                    return;
                }

                if (texto.StartsWith("/cancelar", StringComparison.OrdinalIgnoreCase))
                {
                    CancelarVinculacion(mensaje.Chat.Id);
                    await bot.SendMessage(mensaje.Chat.Id, "Vinculación cancelada. Puedes volver a empezar cuando quieras.",
                        replyMarkup: CrearMenu(), cancellationToken: cancellationToken);
                    return;
                }

                if (texto.StartsWith("/vincular", StringComparison.OrdinalIgnoreCase))
                {
                    if (!EsChatPrivado(mensaje))
                    {
                        await PedirChatPrivado(bot, mensaje.Chat.Id, cancellationToken);
                        return;
                    }

                    GuardarConversacion(mensaje.Chat.Id, new PasoConversacion { Paso = Paso.EsperandoCedula });
                    await bot.SendMessage(mensaje.Chat.Id,
                        "Escribe tu número de cédula para iniciar la vinculación. Envíalo solo en este chat privado.",
                        replyMarkup: CrearBotonCancelar(),
                        cancellationToken: cancellationToken);
                    return;
                }

                PasoConversacion estado;
                estado = BuscarConversacion(mensaje.Chat.Id);
                if (!EsChatPrivado(mensaje) && estado != null)
                {
                    await PedirChatPrivado(bot, mensaje.Chat.Id, cancellationToken);
                    return;
                }

                if (estado != null)
                {
                    if (estado.Paso == Paso.EsperandoCedula)
                    {
                        ActualizarPaso(mensaje.Chat.Id, Paso.EsperandoCodigo);
                        string respuesta = vinculacion.SolicitarCodigo(texto, mensaje.Chat.Id);
                        await bot.SendMessage(mensaje.Chat.Id, respuesta, replyMarkup: CrearBotonCancelar(),
                            cancellationToken: cancellationToken);
                        return;
                    }

                    bool vinculado;
                    string resultado = vinculacion.ConfirmarCodigo(mensaje.Chat.Id, texto, out vinculado);
                    if (vinculado || !vinculacion.HaySolicitudPendiente(mensaje.Chat.Id))
                        EliminarConversacion(mensaje.Chat.Id);
                    await bot.SendMessage(mensaje.Chat.Id, resultado,
                        replyMarkup: vinculado ? CrearMenu() : CrearBotonCancelar(),
                        cancellationToken: cancellationToken);

                    if (vinculado && estado != null && estado.Accion != null)
                    {
                        Cliente clienteVinculado = vinculacion.BuscarClienteVinculado(mensaje.Chat.Id);
                        if (clienteVinculado != null)
                            await MostrarConsulta(bot, mensaje.Chat.Id, clienteVinculado, estado.Accion.Value, cancellationToken);
                    }
                    return;
                }

                await bot.SendMessage(mensaje.Chat.Id,
                    "Escribe /start para ver las opciones o /vincular para asociar tu cuenta.",
                    cancellationToken: cancellationToken);
                return;
            }

            if (actualizacion.Type != UpdateType.CallbackQuery || actualizacion.CallbackQuery == null)
                return;

            CallbackQuery callback = actualizacion.CallbackQuery;
            await bot.AnswerCallbackQuery(callback.Id, cancellationToken: cancellationToken);
            if (callback.Message == null) return;

            if (callback.Data == "cancelar_vinculacion")
            {
                CancelarVinculacion(callback.Message.Chat.Id);
                await bot.SendMessage(callback.Message.Chat.Id,
                    "Vinculación cancelada. Puedes volver a empezar cuando quieras.",
                    replyMarkup: CrearMenu(), cancellationToken: cancellationToken);
                return;
            }

            if (callback.Data == "servicios")
            {
                await bot.SendMessage(callback.Message.Chat.Id, ConsultarServicios(), cancellationToken: cancellationToken);
                return;
            }

            AccionConsulta? accion = callback.Data == "citas" ? AccionConsulta.Citas :
                callback.Data == "vehiculos" ? AccionConsulta.Vehiculos : (AccionConsulta?)null;
            if (accion == null)
            {
                await bot.SendMessage(callback.Message.Chat.Id,
                    "No reconocí esa opción. Escribe /start para abrir el menú.", cancellationToken: cancellationToken);
                return;
            }

            if (callback.Message.Chat.Type != ChatType.Private)
            {
                await PedirChatPrivado(bot, callback.Message.Chat.Id, cancellationToken);
                return;
            }

            try
            {
                Cliente clienteVinculado = vinculacion.BuscarClienteVinculado(callback.Message.Chat.Id);
                if (clienteVinculado != null)
                {
                    await MostrarConsulta(bot, callback.Message.Chat.Id, clienteVinculado, accion.Value, cancellationToken);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("No se pudo verificar la vinculación: " + ex);
                await bot.SendMessage(callback.Message.Chat.Id,
                    "No pude consultar tus datos en este momento. Inténtalo más tarde.", cancellationToken: cancellationToken);
                return;
            }

            GuardarConversacion(callback.Message.Chat.Id, new PasoConversacion
            {
                Paso = Paso.EsperandoCedula,
                Accion = accion
            });
            await bot.SendMessage(callback.Message.Chat.Id,
                "Para consultar tus datos, escribe tu número de cédula. Envíalo solo en este chat privado.",
                replyMarkup: CrearBotonCancelar(),
                cancellationToken: cancellationToken);
        }

        private async Task MostrarConsulta(ITelegramBotClient bot, long chatId, Cliente clienteVinculado,
            AccionConsulta accion, CancellationToken cancellationToken)
        {
            try
            {
                if (accion == AccionConsulta.Vehiculos)
                {
                    var vehiculos = vehiculoService.ConsultarPorClienteCedula(clienteVinculado.Cedula);
                    string texto = vehiculos.Count == 0
                        ? "No encontramos vehículos asociados a tu cuenta."
                        : "Tus vehículos:\n" + string.Join("\n", vehiculos.Select(v =>
                            "• " + v.Placa + " — " + (v.Marca ?? "") + " " + (v.Linea ?? "") + " (" + v.Modelo + ")"));
                    await bot.SendMessage(chatId, texto, cancellationToken: cancellationToken);
                    return;
                }

                var citas = agendaService.ConsultarPorClienteCedula(clienteVinculado.Cedula)
                    .OrderByDescending(c => c.Fecha).ToList();
                string respuesta = citas.Count == 0
                    ? "No encontramos citas asociadas a tus vehículos."
                    : "Tus citas:\n" + string.Join("\n", citas.Select(c =>
                        "• " + c.Fecha.ToString("dd/MM/yyyy HH:mm") + " — " + c.Vehiculo.Placa +
                        " — " + (c.ArticuloServicio == null ? "Servicio" : c.ArticuloServicio.Nombre) +
                        " — " + c.Estado));
                await bot.SendMessage(chatId, respuesta, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("No se pudo consultar información del cliente en Telegram: " + ex);
                await bot.SendMessage(chatId,
                    "No pude consultar tus datos en este momento. Inténtalo más tarde.", cancellationToken: cancellationToken);
            }
        }

        private static bool EsChatPrivado(Message mensaje)
        {
            return mensaje.Chat.Type == ChatType.Private;
        }

        private void GuardarConversacion(long chatId, PasoConversacion paso)
        {
            lock (candadoConversaciones)
            {
                Conversacion anterior = conversaciones.Find(c => c.ChatId == chatId);
                if (anterior != null) conversaciones.Remove(anterior);
                conversaciones.Add(new Conversacion { ChatId = chatId, Paso = paso });
            }
        }

        private PasoConversacion BuscarConversacion(long chatId)
        {
            lock (candadoConversaciones)
            {
                Conversacion conversacion = conversaciones.Find(c => c.ChatId == chatId);
                return conversacion == null ? null : conversacion.Paso;
            }
        }

        private void ActualizarPaso(long chatId, Paso nuevoPaso)
        {
            lock (candadoConversaciones)
            {
                Conversacion conversacion = conversaciones.Find(c => c.ChatId == chatId);
                if (conversacion != null) conversacion.Paso.Paso = nuevoPaso;
            }
        }

        private void EliminarConversacion(long chatId)
        {
            lock (candadoConversaciones)
            {
                Conversacion conversacion = conversaciones.Find(c => c.ChatId == chatId);
                if (conversacion != null) conversaciones.Remove(conversacion);
            }
        }

        private static Task PedirChatPrivado(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken)
        {
            return bot.SendMessage(chatId,
                "Por seguridad, la vinculación debe hacerse en un chat privado con GOT-AUTO. Abre el bot directamente y escribe /vincular.",
                cancellationToken: cancellationToken);
        }

        private static InlineKeyboardMarkup CrearMenu()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("🧰 Servicios", "servicios") },
                new[] { InlineKeyboardButton.WithCallbackData("📅 Mis citas", "citas") },
                new[] { InlineKeyboardButton.WithCallbackData("🚗 Mis vehículos", "vehiculos") }
            });
        }

        private static InlineKeyboardMarkup CrearBotonCancelar()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("❌ Cancelar vinculación", "cancelar_vinculacion") }
            });
        }

        private void CancelarVinculacion(long chatId)
        {
            vinculacion.CancelarSolicitud(chatId);
            EliminarConversacion(chatId);
        }

        private static string ConsultarServicios()
        {
            try
            {
                var articulos = new ArticuloService(new ArticuloRepository(), new ProveedorRepository())
                    .Consultar().OfType<Servicio>().OrderBy(servicio => servicio.Nombre).ToList();
                if (articulos.Count == 0) return "Por ahora no hay servicios registrados.";

                return "Servicios disponibles:\n" + string.Join("\n", articulos.Select(servicio =>
                    "• " + servicio.Nombre + " — " + servicio.PrecioVenta.ToString("C") +
                    (servicio.Duracion > 0 ? " · " + servicio.Duracion + " min" : string.Empty)));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("No se pudieron consultar servicios desde Telegram: " + ex);
                return "No pude consultar los servicios en este momento. Inténtalo más tarde.";
            }
        }

        private static Task ProcesarErrorAsync(ITelegramBotClient bot, Exception exception,
            HandleErrorSource origen, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine("Error de Telegram (" + origen + "): " + exception);
            return Task.CompletedTask;
        }

        private enum Paso { EsperandoCedula, EsperandoCodigo }
        private enum AccionConsulta { Citas, Vehiculos }

        private sealed class PasoConversacion
        {
            public Paso Paso { get; set; }
            public AccionConsulta? Accion { get; set; }
        }

        private sealed class Conversacion
        {
            public long ChatId { get; set; }
            public PasoConversacion Paso { get; set; }
        }
    }
}

using DATOS;
using ENTIDADES;
using LOGICA;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    /// <summary>
    /// Adaptador de Telegram. El proceso ejecutable lo inicia y mantiene activo.
    /// </summary>
    public sealed class BotTelegram
    {
        private readonly TelegramBotClient cliente;

        public BotTelegram(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("El token de Telegram es obligatorio.", "token");

            cliente = new TelegramBotClient(token);
        }

        public void Iniciar(CancellationToken cancellationToken)
        {
            var opciones = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery }
            };

            cliente.StartReceiving(
                ProcesarActualizacionAsync,
                ProcesarErrorAsync,
                opciones,
                cancellationToken);
        }

        private async Task ProcesarActualizacionAsync(ITelegramBotClient bot, Update actualizacion, CancellationToken cancellationToken)
        {
            if (actualizacion.Type == UpdateType.Message && actualizacion.Message != null)
            {
                string texto = actualizacion.Message.Text;
                if (string.IsNullOrWhiteSpace(texto)) return;

                if (texto.StartsWith("/start", StringComparison.OrdinalIgnoreCase) ||
                    texto.StartsWith("/menu", StringComparison.OrdinalIgnoreCase))
                {
                    await bot.SendMessage(
                        actualizacion.Message.Chat.Id,
                        "¡Hola! Soy el asistente de GOT-AUTO. Elige una opción:",
                        replyMarkup: CrearMenu(),
                        cancellationToken: cancellationToken);
                    return;
                }

                await bot.SendMessage(
                    actualizacion.Message.Chat.Id,
                    "Escribe /start para ver las opciones disponibles.",
                    cancellationToken: cancellationToken);
                return;
            }

            if (actualizacion.Type != UpdateType.CallbackQuery || actualizacion.CallbackQuery == null)
                return;

            CallbackQuery callback = actualizacion.CallbackQuery;
            await bot.AnswerCallbackQuery(callback.Id, cancellationToken: cancellationToken);

            if (callback.Message == null) return;

            string respuesta;
            switch (callback.Data)
            {
                case "servicios":
                    respuesta = ConsultarServicios();
                    break;
                case "citas":
                case "vehiculos":
                    respuesta = "Para consultar tus datos primero debemos vincular de forma segura tu cuenta de Telegram con tu cliente del sistema. Esa vinculación aún no está configurada.";
                    break;
                default:
                    respuesta = "No reconocí esa opción. Escribe /start para abrir el menú.";
                    break;
            }

            await bot.SendMessage(callback.Message.Chat.Id, respuesta, cancellationToken: cancellationToken);
        }

        private static InlineKeyboardMarkup CrearMenu()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("Servicios disponibles", "servicios") },
                new[] { InlineKeyboardButton.WithCallbackData("Mis citas", "citas") },
                new[] { InlineKeyboardButton.WithCallbackData("Mis vehículos", "vehiculos") }
            });
        }

        private static string ConsultarServicios()
        {
            try
            {
                var articulos = new ArticuloService(new ArticuloRepository(), new ProveedorRepository())
                    .Consultar()
                    .OfType<Servicio>()
                    .OrderBy(servicio => servicio.Nombre)
                    .ToList();

                if (articulos.Count == 0)
                    return "Por ahora no hay servicios registrados.";

                return "Servicios disponibles:\n" + string.Join("\n", articulos.Select(servicio =>
                    "• " + servicio.Nombre + " — " + servicio.PrecioVenta.ToString("C") +
                    (servicio.Duracion > 0 ? " · " + servicio.Duracion + " min" : string.Empty)));
            }
            catch (Exception)
            {
                return "No pude consultar los servicios en este momento. Inténtalo más tarde.";
            }
        }

        private static Task ProcesarErrorAsync(ITelegramBotClient bot, Exception exception, HandleErrorSource origen, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine("Error de Telegram (" + origen + "): " + exception.Message);
            return Task.CompletedTask;
        }
    }
}

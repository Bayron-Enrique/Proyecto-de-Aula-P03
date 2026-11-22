using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace LOGICA
{
    /// <summary>Reglas sencillas para verificar el correo y vincular el chat.</summary>
    public class VinculacionTelegramService
    {
        private const int MinutosVigencia = 10;
        private const int IntentosPermitidos = 5;

        private readonly ClienteRepository clientes;
        private readonly Email email;
        // Una lista conserva el código pendiente de cada chat.
        private readonly List<Solicitud> solicitudes = new List<Solicitud>();
        // Protege la lista si llegan dos mensajes casi al mismo tiempo.
        private readonly object candado = new object();

        public VinculacionTelegramService(ClienteRepository clientes, Email email)
        {
            this.clientes = clientes;
            this.email = email;
        }

        // Busca el cliente y le manda el código. La respuesta al chat siempre es igual
        // para no revelar si la cédula está registrada.
        public string SolicitarCodigo(string cedula, long chatId)
        {
            string codigo = CrearCodigo();
            Cliente cliente = null;
            bool correoEnviado = false;

            try
            {
                if (CedulaValida(cedula))
                {
                    cliente = clientes.BuscarCedula(cedula.Trim());
                    if (cliente != null && !string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
                    {
                        string mensajeCorreo;
                        correoEnviado = email.EnviarCodigoVinculacion(
                            cliente, codigo, out mensajeCorreo, MinutosVigencia);
                        if (!correoEnviado) Trace.TraceError(mensajeCorreo);
                    }
                }
            }
            catch (Exception error)
            {
                // El error técnico queda para diagnóstico; el chat recibe la misma respuesta.
                Trace.TraceError("Error al solicitar código de Telegram: " + error);
            }

            Solicitud solicitud = new Solicitud
            {
                ChatId = chatId,
                Cedula = correoEnviado ? cliente.Cedula : null,
                Codigo = codigo,
                Vence = DateTime.UtcNow.AddMinutes(MinutosVigencia),
                CorreoEnviado = correoEnviado
            };

            lock (candado)
            {
                Solicitud anterior = solicitudes.Find(s => s.ChatId == chatId);
                if (anterior != null) solicitudes.Remove(anterior);
                solicitudes.Add(solicitud);
            }

            return "Si los datos coinciden con un cliente registrado y tiene un correo válido, recibirá un código. " +
                   "Escríbelo aquí en los próximos 10 minutos para continuar.";
        }

        // Compara el código. Solo si coincide se llama al procedimiento de Oracle.
        public string ConfirmarCodigo(long chatId, string codigo, out bool vinculado)
        {
            vinculado = false;
            Solicitud solicitud;

            lock (candado)
            {
                solicitud = solicitudes.Find(s => s.ChatId == chatId);
                if (solicitud == null || DateTime.UtcNow >= solicitud.Vence)
                {
                    if (solicitud != null) solicitudes.Remove(solicitud);
                    return MensajeCodigoIncorrecto();
                }

                if (!solicitud.CorreoEnviado || string.IsNullOrWhiteSpace(codigo) ||
                    codigo.Trim() != solicitud.Codigo)
                {
                    solicitud.Intentos++;
                    if (solicitud.Intentos >= IntentosPermitidos) solicitudes.Remove(solicitud);
                    return MensajeCodigoIncorrecto();
                }

                solicitudes.Remove(solicitud);
            }

            try
            {
                clientes.VincularTelegram(solicitud.Cedula, chatId);
                vinculado = true;
                return "¡Listo! Tu cuenta quedó vinculada. Ya puedes consultar tus citas y vehículos.";
            }
            catch (Exception error)
            {
                Trace.TraceError("Error al vincular el chat de Telegram: " + error);
                return "No pudimos completar la vinculación. Inténtalo más tarde o contacta al taller.";
            }
        }

        public bool HaySolicitudPendiente(long chatId)
        {
            lock (candado)
            {
                Solicitud solicitud = solicitudes.Find(s => s.ChatId == chatId);
                if (solicitud == null) return false;
                if (DateTime.UtcNow < solicitud.Vence) return true;
                solicitudes.Remove(solicitud);
                return false;
            }
        }

        public void CancelarSolicitud(long chatId)
        {
            lock (candado)
            {
                Solicitud solicitud = solicitudes.Find(s => s.ChatId == chatId);
                if (solicitud != null) solicitudes.Remove(solicitud);
            }
        }

        public Cliente BuscarClienteVinculado(long chatId)
        {
            return clientes.BuscarPorTelegramChatId(chatId);
        }

        private static string CrearCodigo()
        {
            // Genera un número aleatorio de seis cifras para el correo.
            byte[] bytes = new byte[4];
            using (RandomNumberGenerator generador = RandomNumberGenerator.Create())
                generador.GetBytes(bytes);

            uint numero = BitConverter.ToUInt32(bytes, 0) % 1000000;
            return numero.ToString("D6");
        }

        private static bool CedulaValida(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula)) return false;
            cedula = cedula.Trim();
            if (cedula.Length < 6 || cedula.Length > 10) return false;

            foreach (char digito in cedula)
                if (digito < '0' || digito > '9') return false;

            return true;
        }

        private static string MensajeCodigoIncorrecto()
        {
            return "El código no es válido o venció. Inicia de nuevo con /vincular.";
        }

        private class Solicitud
        {
            public long ChatId { get; set; }
            public string Cedula { get; set; }
            public string Codigo { get; set; }
            public DateTime Vence { get; set; }
            public int Intentos { get; set; }
            public bool CorreoEnviado { get; set; }
        }
    }
}

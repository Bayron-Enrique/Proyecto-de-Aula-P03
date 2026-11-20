using ENTIDADES;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace LOGICA
{
    public class Email
    {
        // Colores de la marca (los mismos del programa)
        private const string ColorEncabezado = "#111114";
        private const string ColorAcento = "#009688";
        private const string NombreTaller = "GOT-AUTO";

        // ───────────── Correos que envía el sistema ─────────────

        public bool EnviarConfirmacionRegistro(Cliente cliente, out string mensaje)
        {
            mensaje = null;

            if (cliente == null)
            {
                mensaje = "No se puede enviar el correo porque el cliente es nulo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
            {
                mensaje = "El cliente no tiene correo electronico registrado.";
                return false;
            }

            string nombre = Codificar(cliente.NombreCompleto);

            string contenido =
                "<p style='margin:0 0 16px 0;font-size:15px;color:#333333;'>Hola <strong>" + nombre + "</strong>,</p>" +
                "<p style='margin:0 0 16px 0;font-size:15px;color:#333333;line-height:22px;'>" +
                "Tu registro en el sistema del taller se realizó correctamente. " +
                "Desde ahora podemos llevar el historial de tus vehículos y avisarte cuando toque su mantenimiento.</p>";

            string html = ArmarPlantilla("¡Bienvenido!", contenido);
            string texto = "Hola " + cliente.NombreCompleto + ", tu registro en el sistema del taller se realizo correctamente.";

            return Enviar(cliente.CorreoElectronico, "Confirmacion de registro", html, texto, out mensaje);
        }

        public bool EnviarRecordatorioMantenimiento(NotificacionMantenimiento notificacion, out string mensaje)
        {
            mensaje = null;

            string destinatario = notificacion == null || notificacion.Vehiculo == null || notificacion.Vehiculo.Cliente == null
                ? null : notificacion.Vehiculo.Cliente.CorreoElectronico;

            if (string.IsNullOrWhiteSpace(destinatario))
            {
                mensaje = "El propietario no tiene un correo electrónico registrado.";
                return false;
            }

            string servicio = notificacion.Articulo == null ? "Mantenimiento" : notificacion.Articulo.Nombre;
            string observacion = string.IsNullOrWhiteSpace(notificacion.Comentario) ? "Sin observaciones" : notificacion.Comentario;
            string vehiculo = notificacion.Vehiculo.Marca + " " + notificacion.Vehiculo.Linea;

            // Tabla con los datos del recordatorio (las tablas se ven bien en todos los clientes de correo)
            string detalles =
                "<table role='presentation' width='100%' cellpadding='0' cellspacing='0' " +
                "style='background-color:#f4f6f8;border-radius:6px;margin:8px 0 20px 0;'>" +
                Fila("Vehículo", vehiculo) +
                Fila("Placa", notificacion.Vehiculo.Placa) +
                Fila("Fecha", notificacion.Fecha.ToString("dd/MM/yyyy HH:mm")) +
                Fila("Servicio", servicio) +
                Fila("Observación", observacion) +
                "</table>";

            string contenido =
                "<p style='margin:0 0 16px 0;font-size:15px;color:#333333;'>Hola <strong>" +
                Codificar(notificacion.Vehiculo.Cliente.NombreCompleto) + "</strong>,</p>" +
                "<p style='margin:0 0 8px 0;font-size:15px;color:#333333;line-height:22px;'>" +
                "Te recordamos que tu vehículo tiene un mantenimiento programado:</p>" +
                detalles +
                "<p style='margin:0;font-size:13px;color:#777777;line-height:20px;'>" +
                "Si ya realizaste este servicio o necesitas reprogramarlo, comunícate con nosotros.</p>";

            string html = ArmarPlantilla("Recordatorio de mantenimiento", contenido);

            string texto = string.Format(
                "Hola {0}, le recordamos el mantenimiento programado para {1:dd/MM/yyyy HH:mm}. " +
                "Vehiculo: {2} ({3}). Servicio: {4}. Observacion: {5}",
                notificacion.Vehiculo.Cliente.NombreCompleto, notificacion.Fecha,
                notificacion.Vehiculo.Placa, vehiculo, servicio, observacion);

            return Enviar(destinatario, "Recordatorio de mantenimiento del vehículo " + notificacion.Vehiculo.Placa,
                          html, texto, out mensaje);
        }

        // ───────────── Plantilla visual ─────────────

        // Fila de la tabla de detalles: etiqueta a la izquierda, valor a la derecha
        private static string Fila(string etiqueta, string valor)
        {
            return "<tr>" +
                   "<td style='padding:10px 16px;font-size:13px;color:#777777;width:120px;'>" + Codificar(etiqueta) + "</td>" +
                   "<td style='padding:10px 16px;font-size:14px;color:#222222;font-weight:bold;'>" + Codificar(valor) + "</td>" +
                   "</tr>";
        }

        // Marco común de todos los correos: encabezado, contenido y pie.
        // Solo usa tablas y estilos "inline" porque Outlook y Gmail ignoran gran parte del CSS moderno.
        private static string ArmarPlantilla(string titulo, string contenidoHtml)
        {
            return
                "<!DOCTYPE html><html><head><meta charset='utf-8'></head>" +
                "<body style='margin:0;padding:0;background-color:#eceff1;font-family:Segoe UI,Arial,sans-serif;'>" +
                "<table role='presentation' width='100%' cellpadding='0' cellspacing='0' style='background-color:#eceff1;padding:24px 0;'>" +
                "<tr><td align='center'>" +

                // Tarjeta central de 600 px
                "<table role='presentation' width='600' cellpadding='0' cellspacing='0' " +
                "style='max-width:600px;width:100%;background-color:#ffffff;border-radius:8px;overflow:hidden;'>" +

                // Encabezado oscuro con el nombre del taller
                "<tr><td style='background-color:" + ColorEncabezado + ";padding:22px 32px;'>" +
                EncabezadoMarca() +
                "</td></tr>" +

                // Franja de acento
                "<tr><td style='background-color:" + ColorAcento + ";height:4px;font-size:0;line-height:0;'>&nbsp;</td></tr>" +

                // Título y contenido
                "<tr><td style='padding:32px;'>" +
                "<h1 style='margin:0 0 20px 0;font-size:22px;color:#111114;'>" + Codificar(titulo) + "</h1>" +
                contenidoHtml +
                "</td></tr>" +

                // Pie
                "<tr><td style='background-color:#f4f6f8;padding:16px 32px;font-size:12px;color:#999999;'>" +
                "Este es un mensaje automático de " + NombreTaller + ". Por favor no respondas a este correo." +
                "</td></tr>" +

                "</table></td></tr></table></body></html>";
        }

        // Evita que un nombre con caracteres como < o & rompa el diseño del correo
        private static string Codificar(string texto)
        {
            return WebUtility.HtmlEncode(texto ?? "");
        }

        // ───────────── Logo ─────────────

        // Busca el archivo del logo. Primero en la configuración (Email.Logo.Path),
        // y si no está, usa logo.png junto al ejecutable. Devuelve null si no existe.
        private static string RutaLogo()
        {
            string ruta = ConfigurationManager.AppSettings["Email.Logo.Path"];
            if (string.IsNullOrWhiteSpace(ruta))
            {
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");
            }

            return File.Exists(ruta) ? ruta : null;
        }

        // Si hay logo, muestra la imagen; si no, escribe el nombre del taller en texto
        private static string EncabezadoMarca()
        {
            if (RutaLogo() != null)
            {
                return "<img src='cid:logo' alt='" + NombreTaller + "' height='40' " +
                       "style='display:block;border:0;height:40px;'>";
            }

            return "<span style='font-size:20px;font-weight:bold;color:#ffffff;letter-spacing:1px;'>" +
                   NombreTaller + "</span>";
        }

        // ───────────── Envío (código común a todos los correos) ─────────────

        private bool Enviar(string destinatario, string asunto, string html, string textoPlano, out string mensaje)
        {
            string host = LeerConfiguracion("Email.Smtp.Host", "TALLER_EMAIL_SMTP_HOST");
            string puertoTexto = LeerConfiguracion("Email.Smtp.Port", "TALLER_EMAIL_SMTP_PORT");
            string usuario = LeerConfiguracion("Email.Smtp.User", "TALLER_EMAIL_SMTP_USER");
            string contrasena = LeerConfiguracion("Email.Smtp.Password", "TALLER_EMAIL_SMTP_PASSWORD");
            string remitente = LeerConfiguracion("Email.Smtp.From", "TALLER_EMAIL_SMTP_FROM");
            string sslTexto = LeerConfiguracion("Email.Smtp.EnableSsl", "TALLER_EMAIL_SMTP_ENABLE_SSL");

            int puerto;
            bool habilitarSsl;
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(remitente) ||
                !int.TryParse(puertoTexto, out puerto) || !bool.TryParse(sslTexto, out habilitarSsl))
            {
                mensaje = "La configuracion SMTP esta incompleta. No se envio el correo.";
                return false;
            }

            try
            {
                using (MailMessage correo = new MailMessage())
                using (SmtpClient smtp = new SmtpClient(host, puerto))
                {
                    correo.From = new MailAddress(remitente, "GOT-AUTO");
                    correo.To.Add(destinatario);
                    correo.Subject = asunto;

                    // Dos versiones del mismo mensaje: el cliente de correo muestra la mejor que soporte
                    correo.AlternateViews.Add(
                        AlternateView.CreateAlternateViewFromString(textoPlano, null, MediaTypeNames.Text.Plain));
                    AlternateView vistaHtml =
                        AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

                    // Adjunta el logo dentro del propio correo; en el HTML se llama con src='cid:logo'
                    string rutaLogo = RutaLogo();
                    if (rutaLogo != null)
                    {
                        LinkedResource logo = new LinkedResource(rutaLogo);
                        logo.ContentId = "logo";
                        vistaHtml.LinkedResources.Add(logo);
                    }

                    correo.AlternateViews.Add(vistaHtml);

                    smtp.EnableSsl = habilitarSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(usuario, contrasena);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(correo);
                }

                mensaje = "Correo enviado.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        private static string LeerConfiguracion(string clave, string variableAmbiente)
        {
            string valorAmbiente = Environment.GetEnvironmentVariable(variableAmbiente);
            if (!string.IsNullOrWhiteSpace(valorAmbiente))
            {
                return valorAmbiente;
            }

            return ConfigurationManager.AppSettings[clave];
        }
    }
}
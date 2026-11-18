using ENTIDADES;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace LOGICA
{
    public class Email
    {
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
                mensaje = "La configuracion SMTP esta incompleta. El cliente se guardo, pero no se envio el correo.";
                return false;
            }

            try
            {
                using (MailMessage correo = new MailMessage())
                using (SmtpClient smtp = new SmtpClient(host, puerto))
                {
                    correo.From = new MailAddress(remitente);
                    correo.To.Add(cliente.CorreoElectronico);
                    correo.Subject = "Confirmacion de registro";
                    correo.Body = string.Format(
                        "Hola {0}, tu registro en el sistema del taller se realizo correctamente.",
                        cliente.NombreCompleto);
                    correo.IsBodyHtml = false;

                    smtp.EnableSsl = habilitarSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(usuario, contrasena);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(correo);
                }

                mensaje = "Correo de confirmacion enviado.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
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

            string host = LeerConfiguracion("Email.Smtp.Host", "TALLER_EMAIL_SMTP_HOST");
            string puertoTexto = LeerConfiguracion("Email.Smtp.Port", "TALLER_EMAIL_SMTP_PORT");
            string usuario = LeerConfiguracion("Email.Smtp.User", "TALLER_EMAIL_SMTP_USER");
            string contrasena = LeerConfiguracion("Email.Smtp.Password", "TALLER_EMAIL_SMTP_PASSWORD");
            string remitente = LeerConfiguracion("Email.Smtp.From", "TALLER_EMAIL_SMTP_FROM");
            string sslTexto = LeerConfiguracion("Email.Smtp.EnableSsl", "TALLER_EMAIL_SMTP_ENABLE_SSL");
            int puerto;
            bool habilitarSsl;
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(remitente) || !int.TryParse(puertoTexto, out puerto) || !bool.TryParse(sslTexto, out habilitarSsl))
            {
                mensaje = "La configuración SMTP está incompleta.";
                return false;
            }

            try
            {
                using (MailMessage correo = new MailMessage())
                using (SmtpClient smtp = new SmtpClient(host, puerto))
                {
                    correo.From = new MailAddress(remitente);
                    correo.To.Add(destinatario);
                    correo.Subject = "Recordatorio de mantenimiento del vehículo " + notificacion.Vehiculo.Placa;
                    correo.Body = string.Format("Hola {0}, le recordamos el mantenimiento programado para {1:dd/MM/yyyy HH:mm}. Vehículo: {2} ({3} {4}). Servicio/artículo: {5}. Observación: {6}",
                        notificacion.Vehiculo.Cliente.NombreCompleto, notificacion.Fecha, notificacion.Vehiculo.Placa,
                        notificacion.Vehiculo.Marca, notificacion.Vehiculo.Linea,
                        notificacion.Articulo == null ? "Mantenimiento" : notificacion.Articulo.Nombre,
                        string.IsNullOrWhiteSpace(notificacion.Comentario) ? "Sin observaciones" : notificacion.Comentario);
                    smtp.EnableSsl = habilitarSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(usuario, contrasena);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(correo);
                }
                mensaje = "Recordatorio enviado.";
                return true;
            }
            catch (Exception ex) { mensaje = ex.Message; return false; }
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

using System;
using System.Diagnostics;
using System.Threading;
using TelegramBot;

namespace TelegramBot.Host
{
    internal static class Program
    {
        private static void Main()
        {
            // Muestra en la consola los errores técnicos que LOGICA registra con Trace.
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Error));
            Trace.AutoFlush = true;

            string token = Environment.GetEnvironmentVariable("TALLER_TELEGRAM_BOT_TOKEN");
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.Error.WriteLine("Configura la variable de entorno TALLER_TELEGRAM_BOT_TOKEN con el token del bot.");
                Console.WriteLine("Presiona una tecla para cerrar.");
                Console.ReadKey();
                return;
            }

            using (var cancellation = new CancellationTokenSource())
            {
                Console.CancelKeyPress += (sender, args) =>
                {
                    args.Cancel = true;
                    cancellation.Cancel();
                };

                try
                {
                    var bot = new BotTelegram(token);
                    bot.Iniciar(cancellation.Token);
                    Console.WriteLine("Bot GOT-AUTO iniciado. Déjalo abierto para mantenerlo activo.");
                    Console.WriteLine("Presiona Ctrl+C para detenerlo.");

                    while (!cancellation.IsCancellationRequested)
                        Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("No se pudo iniciar el bot: " + ex.Message);
                    Console.WriteLine("Presiona una tecla para cerrar.");
                    Console.ReadKey();
                }
            }
        }
    }
}

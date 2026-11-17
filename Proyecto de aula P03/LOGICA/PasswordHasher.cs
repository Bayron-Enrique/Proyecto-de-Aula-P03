using System;
using System.Security.Cryptography;

namespace LOGICA
{
    internal static class PasswordHasher
    {
        private const int Iteraciones = 120000;
        private const int LongitudSalt = 16;
        private const int LongitudHash = 32;

        public static string Crear(string password)
        {
            byte[] salt = new byte[LongitudSalt];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var derivador = new Rfc2898DeriveBytes(password, salt, Iteraciones))
                return Iteraciones + "." + Convert.ToBase64String(salt) + "." + Convert.ToBase64String(derivador.GetBytes(LongitudHash));
        }

        public static bool Verificar(string password, string guardado)
        {
            try
            {
                string[] partes = guardado.Split('.');
                int iteraciones = int.Parse(partes[0]);
                byte[] salt = Convert.FromBase64String(partes[1]);
                byte[] esperado = Convert.FromBase64String(partes[2]);
                using (var derivador = new Rfc2898DeriveBytes(password, salt, iteraciones))
                {
                    byte[] actual = derivador.GetBytes(esperado.Length);
                    int diferencia = actual.Length ^ esperado.Length;
                    for (int i = 0; i < actual.Length && i < esperado.Length; i++) diferencia |= actual[i] ^ esperado[i];
                    return diferencia == 0;
                }
            }
            catch { return false; }
        }
    }
}

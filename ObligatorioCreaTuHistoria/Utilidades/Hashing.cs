using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades
{
    public class Hashing
    {

        public static string Hash(string password)
        {
            // Salt aleatorio
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 2, // hilos
                MemorySize = 65536,      // 64 MB
                Iterations = 4           // tiempo
            };

            byte[] hash = argon2.GetBytes(32);

            // Guardar: salt + hash (base64)
            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);



            /*

            Salt → evita ataques con tablas rainbow
            Iterations → más lento = más seguro
            MemorySize → clave de Argon2 (mata GPUs)
            Parallelism → uso de CPU

            ** Valores recomendados (prácticos)

            Para producción hoy:

            Memory: 64 MB – 256 MB
            Iterations: 3–5
            Parallelism: 2–4

            Ajustar según el tiempo objetivo (~100–300 ms por hash)

            ** Detalles importantes
            Siempre guardar salt + hash
            Nunca guardar la contraseña
            Usar FixedTimeEquals (evita timing attacks)
            No hardcodear parámetros           
            */
        }
        public static bool Verify(string contrasena, string storedHash)
        {
            var parts = storedHash.Split('.');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(contrasena))
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                MemorySize = 65536,
                Iterations = 4
            };

            byte[] newHash = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }


    }
}

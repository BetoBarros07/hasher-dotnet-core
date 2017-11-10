using System;
using System.Text;

using static System.Security.Cryptography.SHA256;

namespace PasswordEncrypter.Algorithms
{
    public class Sha256Algorithm : IAlgorithm
    {
        public string Crypt(string value)
        {
            using (var algorithm = Create())
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                var stringHash = BitConverter.ToString(hashedBytes);
                return stringHash.Replace("-", "").ToLower();
            }
        }

        public string Crypt(string value, string salt, SaltType saltType)
        {
            string saltValue;
            if (saltType == SaltType.Before)
                saltValue = $"{salt}{value}";
            else
                saltValue = $"{value}{salt}";
            return Crypt(saltValue);
        }

        public string Crypt(string value, string beforeSalt, string afterSalt)
        {
            var saltValue = $"{beforeSalt}{value}{afterSalt}";
            return Crypt(saltValue);
        }
    }
}
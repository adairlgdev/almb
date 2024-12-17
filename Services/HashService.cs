using almb.Models;
using System.Security.Cryptography;
using System.Text;

namespace almb.Services
{
    public class HashService
    {
        private static readonly HashAlgorithm _algorithm = SHA512.Create();

        public static string Encrypt(string stringToEncrypt)
        {
            var encryptedString = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToEncrypt));

            var sb = new StringBuilder();

            foreach (var character in encryptedString)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool IsEqual(string stringToDecrypt, string stringToCompare)
        {
            if (string.IsNullOrEmpty(stringToDecrypt))
                throw new NullReferenceException("Informe uma string para descriptografar");

            var encryptedString = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToDecrypt));

            var sb = new StringBuilder();
            foreach (var character in encryptedString)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString() == stringToCompare;
        }
    }
}

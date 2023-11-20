using System.Security.Cryptography;

namespace backend.DataAccess.Utilities
{
    public class Crypto
    {
        public static string? Encrypt(string pass)
        {
            using SHA512 sha = new SHA512Managed();
            using MemoryStream st = new();
            using BinaryWriter bw = new(st);
            bw.Write(pass);
            var starr = st.ToArray();

            return Convert.ToBase64String(sha.ComputeHash(starr));
        }
    }
}

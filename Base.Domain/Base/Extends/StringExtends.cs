using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Base.Domain.Base.Extends
{
    public static partial class StringExtends
    {
        public static string RetornaApenasNumeros(this string input) => ApenasNumero().Replace(input, string.Empty);
        
        [GeneratedRegex("[^0-9]")]
        private static partial Regex ApenasNumero();

        public static string Criptografar(this string input)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

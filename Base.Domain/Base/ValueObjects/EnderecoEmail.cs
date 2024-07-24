using Base.Domain.Notification.Entities;
using Base.Domain.Resources;
using System.Text.RegularExpressions;

namespace Base.Domain.Base.ValueObjects
{
    public class EnderecoEmail : Notificavel
    {
        public String Endereco { get; private set; } = String.Empty;

        /// <summary>
        /// Ef Core.
        /// </summary>
        protected EnderecoEmail() { }

        /// <summary>
        /// Construtor principal.
        /// </summary>
        /// <param name="valor"></param>
        public EnderecoEmail(String valor)
        {
            Endereco = valor?.ToLower() ?? String.Empty;

            if (!SeObrigatorioNaoInformado(String.IsNullOrEmpty(Endereco), Textos.Email))
            {
                SeValorInvalido(!ValidarEmail(Endereco), Textos.Email);
            }
        }

        private static bool ValidarEmail(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
            {
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(endereco, pattern, RegexOptions.IgnoreCase);
        }
    }
}

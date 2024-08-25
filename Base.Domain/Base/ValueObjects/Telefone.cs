using Base.Domain.Notification.Entities;
using Base.Domain.Resources;

namespace Base.Domain.Base.ValueObjects
{
    public class Telefone : Notificavel
    {
        public String Numero { get; private set; } = String.Empty;

        /// <summary>
        /// EF Core.
        /// </summary>
        protected Telefone()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="numero"></param>
        public Telefone(String numero)
        {
            Numero = numero;

            if (!SeObrigatorioNaoInformado(String.IsNullOrEmpty(numero), Textos.Telefone))
            {
                SeInadequadoTamanhoMinimoMaximo(Textos.Telefone, numero, 10, 15); 
                SeValorInvalido(!System.Text.RegularExpressions.Regex.IsMatch(numero, @"^\+\d{1,3}\s?\d{8,14}$"), Textos.Telefone);
            }
        }

        public override string ToString()
        {
            return Numero;
        }

        public static implicit operator string(Telefone telefone) => telefone.Numero;

        public static implicit operator Telefone(string numero) => new(numero);
    }
}

using Base.Domain.Notification.Entities;
using Base.Domain.Resources;

namespace Base.Domain.Base.ValueObjects
{
    public class Monetario : Notificavel
    {
        public decimal Valor { get; private set; } = 0;

        /// <summary>
        /// EF Core.
        /// </summary>
        protected Monetario()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="valor"></param>
        public Monetario(decimal valor)
        {
            Valor = valor;

            SeObrigatorioNaoInformado(valor <= 0, Textos.Monetario);
        }

        public override string ToString()
        {
            return Valor.ToString("C2");
        }

        public static implicit operator decimal(Monetario monetario) => monetario.Valor;

        public static implicit operator Monetario(decimal valor) => new(valor);
    }
}

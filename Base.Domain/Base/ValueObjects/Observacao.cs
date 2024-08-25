using Base.Domain.Base.Entities;
using Base.Domain.Resources;

namespace Base.Domain.Base.ValueObjects
{
    public class Observacao : BaseEntity
    {
        public String Valor { get; private set; } = String.Empty;

        /// <summary>
        /// Ef Core.
        /// </summary>
        protected Observacao()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="valor"></param>
        public Observacao(String valor)
        {
            Valor = valor;

            if (!SeObrigatorioNaoInformado(String.IsNullOrEmpty(valor), Textos.Observacao))
            {
                SeInadequadoTamanhoMinimoMaximo(Textos.Observacao, valor, 20, 2000);
            }
        }

        public override string ToString()
        {
            return Valor;
        }

        public static implicit operator string(Observacao value) => value.Valor;

        public static implicit operator Observacao(string value) => new(value);
    }
}

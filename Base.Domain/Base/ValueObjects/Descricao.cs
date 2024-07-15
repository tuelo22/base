using Base.Domain.Notification.Entities;
using Base.Domain.Resources;

namespace Base.Domain.Base.ValueObjects
{
    public sealed class Descricao : Notificavel
    {
        public String Valor { get; private set; }

        /// <summary>
        /// Ef Core.
        /// </summary>
        protected Descricao()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="valor"></param>
        public Descricao(String valor)
        {
            Valor = valor;

            if(!SeObrigatorioNaoInformado(String.IsNullOrEmpty(valor), Textos.Descricao))
            {
                SeInadequadoTamanhoMinimoMaximo(Textos.Descricao, valor, 20, 255);
            }            
        }

        public override string ToString()
        {
            return Valor;
        }
    }
}

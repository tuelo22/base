using Base.Domain.Notification.Interfaces;
using Base.Domain.Resources;
using System.Drawing;

namespace Base.Domain.Notification.Entities
{
    public abstract class Notificavel : INotificavel
    {
        private readonly List<Mensagem> Mensagens = [];

        public virtual IReadOnlyCollection<Mensagem> GetMensagens() => Mensagens;

        protected void AddMensagem(Mensagem mensagem)
        {
            Mensagens.Add(mensagem);
        }

        protected void AddMensagens(INotificavel notificavel)
        {
            var mensagens = notificavel.GetMensagens().ToList();

            Mensagens.AddRange(mensagens);
        }

        public void Limpar()
        {
            Mensagens.Clear();
        }

        public virtual bool Valido()
        {
            return Mensagem.Sucesso(GetMensagens());
        }

        protected Boolean SeObrigatorioNaoInformado(Boolean condicao, string campo, string? complemento = null)
        {
            if (condicao)
            {
                var mensagem = String.Format(Textos.Obrigatorio, campo);

                AddMensagem(Mensagem.Error(mensagem, complemento));
            }

            return condicao;
        }

        protected void SeExcedeTamanhoMaximo(string campo, string texto, int tamanhoMaximo)
        {
            if ((!String.IsNullOrEmpty(texto)) && texto.Length > tamanhoMaximo)
            {
                var mensagem = String.Format(Textos.TamanoMaximo, campo, tamanhoMaximo);

               AddMensagem(Mensagem.Error(mensagem));
            }
        }

        protected void SeInadequadoTamanhoMinimoMaximo(string campo, string texto, int tamanhoMinimo, int tamanhoMaximo)
        {
            if ((!String.IsNullOrEmpty(texto)) && (texto.Length > tamanhoMaximo || texto.Length < tamanhoMinimo))
            {
                var mensagem = String.Format(Textos.TamanoMinimoMaximo, campo, tamanhoMinimo, tamanhoMaximo);

                AddMensagem(Mensagem.Error(mensagem));
            }
        }

        protected bool SeValorInvalido(Boolean condicao, string campo, string? Complemento = null)
        {
            if (condicao)
            {
                var mensagem = String.Format(Textos.Invalido, campo);

                AddMensagem(Mensagem.Error(mensagem, Complemento));
            }

            return condicao;
        }

        protected bool SeValorInexistente(Boolean condicao, string campo, string? Complemento = null)
        {
            if (condicao)
            {
                var mensagem = String.Format(Textos.Inexistente, campo);

                AddMensagem(Mensagem.Error(mensagem, Complemento));
            }

            return condicao;
        }
    }
}

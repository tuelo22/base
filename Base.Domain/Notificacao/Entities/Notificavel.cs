using Base.Domain.Notification.Interfaces;

namespace Base.Domain.Notification.Entities
{
    public abstract class Notificavel : INotificavel
    {
        private readonly List<Mensagem> Mensagens = [];

        public virtual IReadOnlyCollection<Mensagem> GetMensagens() => Mensagens;

        public void AddMensagem(Mensagem mensagem)
        {
            Mensagens.Add(mensagem);
        }

        public void AddMensagens(INotificavel notificavel)
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
    }
}

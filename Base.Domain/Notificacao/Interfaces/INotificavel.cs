using Base.Domain.Notification.Entities;

namespace Base.Domain.Notification.Interfaces
{
    public interface INotificavel
    {
        IReadOnlyCollection<Mensagem> GetMensagens();

        void AddMensagens(INotificavel notificavel);

        void AddMensagem(Mensagem mensagem);

        void Limpar();

        bool Valido();
    }
}

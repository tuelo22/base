using Base.Domain.Notification.Entities;

namespace Base.Domain.Notification.Interfaces
{
    public interface INotificavel
    {
        IReadOnlyCollection<Mensagem> GetMensagens();

        void AddMensagens(INotificavel notificavel);

        void Limpar();

        bool Valido();
    }
}

using Base.Domain.Notification.Entities;

namespace Base.Domain.Notification.Interfaces
{
    public interface INotificavel
    {
        IReadOnlyCollection<Mensagem> GetMensagens();

        void Limpar();

        bool Valido();
    }
}

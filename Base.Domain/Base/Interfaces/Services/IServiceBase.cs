using Base.Domain.Base.DTO.Arguments;
using Base.Domain.Notification.Interfaces;

namespace Base.Domain.Base.Interfaces.Services
{
    public interface IServiceBase : INotificavel
    {
        ResponseBaseDTO GetRetorno();
    }
}

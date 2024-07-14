using Base.Domain.Base.DTO.Arguments;
using Base.Domain.Base.Interfaces.Services;
using Base.Domain.Notification.DTO;
using Base.Domain.Notification.Entities;

namespace Base.Domain.Base.Service
{
    public class ServiceBase: Notificavel, IServiceBase
    {
        public List<MensagemDTO> GetMensagensDTO()
        {
            return GetMensagens().Select(x => (MensagemDTO)x).ToList();
        }
        public ResponseBaseDTO GetRetorno() => new ()
        {
            Mensagens = GetMensagensDTO()
        };
    }
}

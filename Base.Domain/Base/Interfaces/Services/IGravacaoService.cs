using Base.Domain.Base.DTO.Arguments;

namespace Base.Domain.Base.Interfaces.Services
{
    public interface IGravacaoService : IServiceBase
    {
        public ResponseBaseDTO Gravar(RequestBaseDTO dto);
    }
}

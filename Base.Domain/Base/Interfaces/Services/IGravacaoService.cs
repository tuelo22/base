using Base.Domain.Base.DTO.Arguments;

namespace Base.Domain.Base.Interfaces.Services
{
    public interface IGravacaoService : IServiceBase
    {
        ResponseBaseDTO Gravar(RequestBaseDTO dto);
    }
}

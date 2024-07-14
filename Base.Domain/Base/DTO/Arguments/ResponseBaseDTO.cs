using Base.Domain.Notification.DTO;

namespace Base.Domain.Base.DTO.Arguments
{
    /// <summary>
    /// Resposta padrão com a lista de mensagens geradas na requisição.
    /// </summary>
    public class ResponseBaseDTO
    {
        /// <summary>
        /// Lista de mensagens.
        /// </summary>
        public List<MensagemDTO> Mensagens { get; set; } = [];
    }
}

using Base.Domain.Notification.Enum;
using Base.Domain.Resources;

namespace Base.Domain.Notification.Entities
{
    public sealed class Mensagem
    {
        public String Texto { get; set; }
        public String? Complemento { get; set; }

        public TipoMensagem Tipo { get; set; }

        private Mensagem(String mensagem, TipoMensagem tipoMensagem, String? complemento = null)
        {
            this.Texto = mensagem;
            this.Tipo = tipoMensagem;
            this.Complemento = complemento;
        }

        public static Mensagem Info(String mensagem) => new (mensagem, TipoMensagem.Info);
        public static Mensagem Warn(String mensagem) => new (mensagem, TipoMensagem.Warn);
        public static Mensagem Error(String mensagem, string? complemento = null) => new (mensagem, TipoMensagem.Error, complemento);
        public static Mensagem Fatal(String mensagem, string? complemento) => new(mensagem, TipoMensagem.Fatal, complemento);
    
        public static bool Sucesso(IReadOnlyCollection<Mensagem> notifications) => !notifications.Any(x => x.Tipo == TipoMensagem.Fatal || x.Tipo == TipoMensagem.Error);
  
    }
}

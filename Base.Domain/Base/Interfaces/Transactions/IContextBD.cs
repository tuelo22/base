using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Base.Domain.Base.Interfaces.Transactions
{
    public interface IContextBD
    {
        /// <summary>
        /// Acesso ao banco do contexto. Todo <c>DbContext</c> já tem essa propriedade, então quem herda de <c>DbContext</c> continua
        /// compilando sem mudança. O <see cref="IUnitOfWork.ExecutarEmTransacaoAsync"/> abre a transação por aqui.
        /// </summary>
        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

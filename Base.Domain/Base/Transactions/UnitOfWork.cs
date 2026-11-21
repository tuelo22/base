using Base.Domain.Base.Interfaces.Transactions;

namespace Base.Domain.Base.Transactions
{
    public class UnitOfWork(IContextBD _context) : IUnitOfWork
    {
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExecutarEmTransacaoAsync(Func<Task<bool>> acao)
        {
            await using var transacao = await _context.Database.BeginTransactionAsync();

            try
            {
                if (!await acao())
                {
                    await transacao.RollbackAsync();
                    return false;
                }

                await _context.SaveChangesAsync();
                await transacao.CommitAsync();

                return true;
            }
            catch
            {
                await transacao.RollbackAsync();
                throw;
            }
        }
    }
}

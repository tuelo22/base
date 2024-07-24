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
    }
}

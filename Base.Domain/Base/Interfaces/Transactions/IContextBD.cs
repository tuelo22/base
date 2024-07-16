namespace Base.Domain.Base.Interfaces.Transactions
{
    public interface IContextBD
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

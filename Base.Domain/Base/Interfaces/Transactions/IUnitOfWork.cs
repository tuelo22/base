namespace Base.Domain.Base.Interfaces.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();
    }
}

using PRO.Repositories.IncomeRepository;

namespace PRO.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IIncomeRepository IncomeRepository { get; }
        Task<int> SaveChangesAsync();

        Task RollbackTransactionAsync();
    }
}

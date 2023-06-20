using PRO.Repositories.AccountRepository;
using PRO.Repositories.ExpenseRepository;
using PRO.Repositories.IncomeRepository;

namespace PRO.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IIncomeRepository IncomeRepository { get; }
        IAccountRepository AccountRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
        Task<int> SaveChangesAsync();

        Task RollbackTransactionAsync();
    }
}

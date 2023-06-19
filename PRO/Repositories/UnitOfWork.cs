using Microsoft.EntityFrameworkCore.Storage;
using PRO.Data;
using PRO.Repositories.IncomeRepository;

namespace PRO.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IIncomeRepository _incomeRepository;
        private IDbContextTransaction _transaction;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _incomeRepository = new IncomeRepository.IncomeRepository(_context);

        }

        public IIncomeRepository IncomeRepository => _incomeRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

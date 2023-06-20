using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;

namespace PRO.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
        }

        public void UpdateAccount(Account account)
        {
            _dbContext.Entry(account).State = EntityState.Modified;
        }

        public void RemoveAccount(Account account)
        {
            _dbContext.Accounts.Remove(account);
        }


        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(account => account.Email == email);
        }

    }
}

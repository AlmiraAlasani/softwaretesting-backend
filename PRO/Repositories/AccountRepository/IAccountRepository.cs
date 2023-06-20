using PRO.Models;

namespace PRO.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task<Account> GetAccountByEmail(string email);
        Task AddAccountAsync(Account account);
        void UpdateAccount(Account account);
        void RemoveAccount(Account account);
    }
}

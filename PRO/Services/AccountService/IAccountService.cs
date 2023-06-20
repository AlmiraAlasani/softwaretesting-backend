using PRO.DTOs;

namespace PRO.Services.AccountService
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();
        Task<AccountDTO> GetAccountByIdAsync(int id);
        Task<AccountDTO> GetAccountByEmail(string email);
        Task<AccountDTO> CreateAccountAsync(AccountDTO accountDto);
        Task UpdateAccountAsync(int id, AccountDTO accountDto);
        Task DeleteAccountAsync(int id);
    }
}

using AutoMapper;
using PRO.DTOs;
using PRO.Models;
using PRO.Repositories;

namespace PRO.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAccountsAsync();
            return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        public async Task<AccountDTO> GetAccountByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(id);
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<AccountDTO> CreateAccountAsync(AccountDTO accountDto)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDto);
                _unitOfWork.AccountRepository.AddAccountAsync(account);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<AccountDTO>(account);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task UpdateAccountAsync(int id, AccountDTO accountDto)
        {
            try
            {
                var account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(id);
                if (account == null)
                {
                    throw new ArgumentException("Account not found.");
                }
                _unitOfWork.AccountRepository.UpdateAccount(account);
                _mapper.Map(accountDto, account);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                throw new ArgumentException("Account not found.");
            }
            _unitOfWork.AccountRepository.RemoveAccount(account);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<AccountDTO> CreateItemAsync(AccountDTO accountDto)
        {
            throw new NotImplementedException();
        }
    }
}

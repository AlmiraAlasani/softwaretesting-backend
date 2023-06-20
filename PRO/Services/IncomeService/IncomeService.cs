using AutoMapper;
using PRO.DTOs;
using PRO.Models;
using PRO.Repositories;
using System.Security.Principal;

namespace PRO.Services.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IncomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IncomeDTO>> GetAllIncomesAsync()
        {
            var incomes = await _unitOfWork.IncomeRepository.GetAllIncomesAsync();
            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public async Task<IncomeDTO> GetIncomeByIdAsync(int id)
        {
            var income = await _unitOfWork.IncomeRepository.GetIncomeByIdAsync(id);
            return _mapper.Map<IncomeDTO>(income);
        }

        public async Task<IncomeDTO> CreateIncomeAsync(IncomeDTO incomeDto)
        {
            try
            {
                var income = _mapper.Map<Income>(incomeDto);
                _unitOfWork.IncomeRepository.AddIncomeAsync(income);
                Account account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(1);
                account.Balance += income.Amount;
                _unitOfWork.AccountRepository.UpdateAccount(account);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<IncomeDTO>(income);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task UpdateIncomeAsync(int id, IncomeDTO incomeDto)
        {
            try
            {
                var income = await _unitOfWork.IncomeRepository.GetIncomeByIdAsync(id);
                if (income == null)
                {
                    throw new ArgumentException("Income not found.");
                }
                _unitOfWork.IncomeRepository.UpdateIncome(income);
                _mapper.Map(incomeDto, income);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task DeleteIncomeAsync(int id)
        {
            var income = await _unitOfWork.IncomeRepository.GetIncomeByIdAsync(id);
            if (income == null)
            {
                throw new ArgumentException("Income not found.");
            }
            _unitOfWork.IncomeRepository.RemoveIncome(income);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<IncomeDTO> CreateItemAsync(IncomeDTO incomeDto)
        {
            throw new NotImplementedException();
        }
    }
}

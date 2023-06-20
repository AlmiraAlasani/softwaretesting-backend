using AutoMapper;
using PRO.DTOs;
using PRO.Models;
using PRO.Repositories;

namespace PRO.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpensesAsync()
        {
            var expenses = await _unitOfWork.ExpenseRepository.GetAllExpensesAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO> GetExpenseByIdAsync(int id)
        {
            var expense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);
            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDto)
        {
            try
            {
                var expense = _mapper.Map<Expense>(expenseDto);
                _unitOfWork.ExpenseRepository.AddExpenseAsync(expense);

                Account account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(1);
                account.Balance -= expense.Amount;
                _unitOfWork.AccountRepository.UpdateAccount(account);

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<ExpenseDTO>(expense);


            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task UpdateExpenseAsync(int id, ExpenseDTO expenseDto)
        {
            try
            {
                var expense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);
                if (expense == null)
                {
                    throw new ArgumentException("Expense not found.");
                }
                _unitOfWork.ExpenseRepository.UpdateExpense(expense);
                _mapper.Map(expenseDto, expense);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                throw new ArgumentException("Expense not found.");
            }
            _unitOfWork.ExpenseRepository.RemoveExpense(expense);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<ExpenseDTO> CreateItemAsync(ExpenseDTO expenseDto)
        {
            throw new NotImplementedException();
        }
    }
}

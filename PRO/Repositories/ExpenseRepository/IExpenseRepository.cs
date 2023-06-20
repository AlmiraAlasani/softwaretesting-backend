using PRO.Models;

namespace PRO.Repositories.ExpenseRepository
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        Task AddExpenseAsync(Expense expense);
        void UpdateExpense(Expense expense);
        void RemoveExpense(Expense expense);
    }
}

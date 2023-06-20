using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;

namespace PRO.Repositories.ExpenseRepository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _dbContext;

        public ExpenseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _dbContext.Expenses.ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _dbContext.Expenses.FindAsync(id);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public void UpdateExpense(Expense expense)
        {
            _dbContext.Entry(expense).State = EntityState.Modified;
        }

        public void RemoveExpense(Expense expense)
        {
            _dbContext.Expenses.Remove(expense);
        }
    }
}

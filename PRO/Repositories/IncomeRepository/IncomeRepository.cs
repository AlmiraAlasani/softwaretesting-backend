using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;

namespace PRO.Repositories.IncomeRepository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _dbContext;

        public IncomeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Income>> GetAllIncomesAsync()
        {
            return await _dbContext.Incomes.ToListAsync();
        }

        public async Task<Income> GetIncomeByIdAsync(int id)
        {
            return await _dbContext.Incomes.FindAsync(id);
        }

        public async Task AddIncomeAsync(Income income)
        {
            await _dbContext.Incomes.AddAsync(income);
        }

        public void UpdateIncome(Income income)
        {
            _dbContext.Entry(income).State = EntityState.Modified;
        }

        public void RemoveIncome(Income income)
        {
            _dbContext.Incomes.Remove(income);
        }
    }
}

using PRO.Models;

namespace PRO.Repositories.IncomeRepository
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<Income>> GetAllIncomesAsync();
        Task<Income> GetIncomeByIdAsync(int id);
        Task AddIncomeAsync(Income income);
        void UpdateIncome(Income income);
        void RemoveIncome(Income income);
    }
}

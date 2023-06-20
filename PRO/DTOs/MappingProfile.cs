using AutoMapper;
using PRO.Models;

namespace PRO.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Income, IncomeDTO>();
            CreateMap<IncomeDTO, Income>();
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<ExpenseDTO, Expense>();
        }
    }
}

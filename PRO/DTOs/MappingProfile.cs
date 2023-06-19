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
        }
    }
}

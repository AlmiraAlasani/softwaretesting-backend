using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRO.DTOs;
using PRO.Models;
using PRO.Services.IncomeService;

namespace PRO.Controllers
{
    [ApiController]
    [Route("api/incomes")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IMapper _mapper;

        public IncomeController(IIncomeService incomeService, IMapper mapper)
        {
            _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetAllIncomesAsync()
        {
            var incomes = await _incomeService.GetAllIncomesAsync();
            var incomeDTOs = _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
            return Ok(incomeDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IncomeDTO>> GetIncomeByIdAsync(int id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            var incomeDTO = _mapper.Map<IncomeDTO>(income);
            return Ok(incomeDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IncomeDTO>> CreateIncomeAsync(IncomeDTO incomeDTO)
        {
            await _incomeService.CreateIncomeAsync(incomeDTO);
            var newIncomeDTO = _mapper.Map<IncomeDTO>(incomeDTO);
            return Ok(newIncomeDTO);
        }
        

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateIncomeAsync(int id, IncomeDTO incomeDTO)
        {
            if (id != incomeDTO.Id)
            {
                return BadRequest();
            }
            var income = _mapper.Map<Income>(incomeDTO);
            try
            {
                await _incomeService.UpdateIncomeAsync(id, incomeDTO);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteIncomeAsync(int id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            await _incomeService.DeleteIncomeAsync(income.Id);
            return NoContent();
        }
    }
}

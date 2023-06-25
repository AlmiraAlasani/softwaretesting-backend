using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRO.DTOs;
using PRO.Models;
using PRO.Services.ExpenseService;

namespace PRO.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetAllExpensesAsync()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            var expenseDTOs = _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
            return Ok(expenseDTOs);
        }

        [HttpGet("{id}")]
        // [Authorize]
        public async Task<ActionResult<ExpenseDTO>> GetExpenseByIdAsync(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            var expenseDTO = _mapper.Map<ExpenseDTO>(expense);
            return Ok(expenseDTO);
        }

        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<ExpenseDTO>> CreateExpenseAsync(ExpenseDTO expenseDTO)
        {
            await _expenseService.CreateExpenseAsync(expenseDTO);
            var newExpenseDTO = _mapper.Map<ExpenseDTO>(expenseDTO);
            return Ok(newExpenseDTO);
        }


        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult> UpdateExpenseAsync(int id, ExpenseDTO expenseDTO)
        {
            if (id != expenseDTO.Id)
            {
                return BadRequest();
            }
            var expense = _mapper.Map<Expense>(expenseDTO);
            try
            {
                await _expenseService.UpdateExpenseAsync(id, expenseDTO);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult> DeleteExpenseAsync(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            await _expenseService.DeleteExpenseAsync(expense.Id);
            return NoContent();
        }
    }
}

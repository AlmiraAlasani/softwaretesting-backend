using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRO.DTOs;
using PRO.Models;
using PRO.Services.AccountService;

namespace PRO.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDTOs = _mapper.Map<IEnumerable<AccountDTO>>(accounts);
            return Ok(accountDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountDTO = _mapper.Map<AccountDTO>(account);
            return Ok(accountDTO);
        }
        [HttpGet("email/{email}")]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> GetAccountByEmail(string email)
        {
            var account = await _accountService.GetAccountByEmail(email);
            if (account == null)
            {
                return NotFound();
            }
            var accountDTO = _mapper.Map<AccountDTO>(account);
            return Ok(accountDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> CreateAccountAsync(AccountDTO accountDTO)
        {
            await _accountService.CreateAccountAsync(accountDTO);
            var newAccountDTO = _mapper.Map<AccountDTO>(accountDTO);
            return Ok(newAccountDTO);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateAccountAsync(int id, AccountDTO accountDTO)
        {
            if (id != accountDTO.Id)
            {
                return BadRequest();
            }
            var account = _mapper.Map<Account>(accountDTO);
            try
            {
                await _accountService.UpdateAccountAsync(id, accountDTO);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAccountAsync(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            await _accountService.DeleteAccountAsync(account.Id);
            return NoContent();
        }
    }
}

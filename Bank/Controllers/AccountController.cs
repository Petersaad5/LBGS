using BAL.IServices;
using BAL.Services;
using Bank.Requests;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("GetAccountById")]
        public IActionResult GetAccountById([FromQuery] GetAccountByIdRequest request)
        {
            var account = _accountService.GetAccountById(request);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpGet("GetAccountsByUserId")]
        public IActionResult GetAccountByUserId([FromQuery] GetUserByIdRequest request)
        {
            var accounts = _accountService.GetAccountByUserId(request);
            if (accounts.Count == 0)
            {
                return NotFound();
            }
            return Ok(accounts);

        }
        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount(UpdateAccountRequest request)
        {
            var accountId = new GetAccountByIdRequest { Id = request.Id };
            var account = _accountService.GetAccountById(accountId);
            int affectedRows = _accountService.UpdateAccount(request);
            if (account == null || affectedRows == 0)
            {
                return NotFound();
            }
            //Returning the new account
            var updatedAccount = GetAccountById(accountId);
            return Ok (updatedAccount);
        }
        [HttpPost("AddAccount")]
        public IActionResult AddAccount(AddAccountRequest request)
        {
            int affectedRows = _accountService.AddAccount(request);

            if (affectedRows == 0)
            {
                return BadRequest($"could not add the Account{affectedRows}");
            }

            return Ok($"Account {request.AccountNumber} added successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var getAccountRequest = new GetAccountByIdRequest { Id = id };

            if (_accountService.GetAccountById(getAccountRequest) == null)
            {
                return NotFound("Account not found .Could not delete");
            }
            int affectedRows = _accountService.DeleteAccount(id);
            if (affectedRows == 0)
            {
                return BadRequest();
            }
            return Ok("Account Deleted successefully");
        }
    }
}

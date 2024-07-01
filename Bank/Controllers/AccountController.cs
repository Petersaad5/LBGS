using BAL.IServices;
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
        public IActionResult GetAccountById([FromQuery] GetOrDeleteAccountByIdRequest request)
        {
            var account = _accountService.GetAccountById(request);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpGet("GetAccountsByUserId")]
        public IActionResult GetAccountByUserId([FromQuery] GetAccountByUserIdRequest request)
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
            var accountId = new GetOrDeleteAccountByIdRequest { Id = request.Id };
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
    }
}

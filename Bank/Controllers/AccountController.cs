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
        [HttpGet("GetAccount")]
        public IActionResult GetAccountById([FromQuery]GetOrDeleteAccountByIdRequest request)
        {
            var account = _accountService.GetAccountById(request);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
    }
}

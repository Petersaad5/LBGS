using BAL.IServices;
using Bank.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser([FromQuery] GetUserByIdRequest request)
        {
            var user = _userService.GetUser(request);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }
    }
}

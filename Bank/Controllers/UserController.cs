using BAL.IServices;
using Bank.Requests;
using Common.Requests;
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
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();

            if (users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }
        [HttpGet("AddUser")]
        public IActionResult AddUser([FromQuery] AddUserRequest request)
        {
            int affectedRows = _userService.AddUser(request);

            if (affectedRows == 0)
            {
                return BadRequest("could not add the user{affectedRows}" );
            }

            return Ok("User added successfully");
        }
    }
}

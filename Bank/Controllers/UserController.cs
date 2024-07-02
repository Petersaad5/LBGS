using BAL.IServices;
using Bank.Models;
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

        [HttpPost("AddUser")]
        public IActionResult AddUser(AddUserRequest request)
        {
            int affectedRows = _userService.AddUser(request);

            if (affectedRows == 0)
            {
                return BadRequest("could not add the user{affectedRows}");
            }

            return Ok("User added successfully");
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserRequest request)
        {
            var getUserRequest = new GetUserByIdRequest { UserId = request.UserId };

            var user = _userService.GetUser(getUserRequest);

            if (user == null)
            {
                return BadRequest("User was not found");
            }

            _userService.UpdateUser(request);

            return Ok("User was successfully updated");
        }

        [HttpPut("DeactivateUser/{id}")]
        public IActionResult DeactivateUser(int id)
        {
            var getUserRequest = new GetUserByIdRequest { UserId = id };

            User? user = _userService.GetUser(getUserRequest);

            if (user == null)
            {
                return BadRequest("User was not found");
            }

            _userService.DeactiveUser(id);

            return Ok("User was successefully Deactivated");
        }
    }
}

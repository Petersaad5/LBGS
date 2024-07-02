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
        public IActionResult GetUser([FromQuery] GetOrDeleteUserByIdRequest request)
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
        public IActionResult AddUser( AddUserRequest request)
        {
            int affectedRows = _userService.AddUser(request);

            if (affectedRows == 0)
            {
                return BadRequest("could not add the user{affectedRows}" );
            }

            return Ok("User added successfully");
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserRequest request)
        {
            var getUserRequest = new GetOrDeleteUserByIdRequest { UserId = request.UserId };
            if (_userService.GetUser(getUserRequest) == null)
            {
                return NotFound();
            }
            int affectedRows=_userService.UpdateUser(request);
            if (affectedRows == 0)
            {
                return NotFound("User not found .");
            }
            else
            {
                return Ok("User {user.name} updated successefully");
            } 
        }
        [HttpDelete ("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var getUserRequest = new GetOrDeleteUserByIdRequest { UserId = id };
            
            if (_userService.GetUser(getUserRequest) == null )
            {
                return NotFound("User not found .Could not delete");
            }
            int affectedRows = _userService.DeleteUser(id);
            if (affectedRows == 0)
            {
                return BadRequest();
            }
            return Ok("User Deleted successefully");
        }
    }
}

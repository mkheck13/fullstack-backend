using fullstack_backend.Models.DTOS;
using fullstack_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_backend.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            bool success = await _userServices.CreateUser(user);

            if(success) return Ok(new {Success = true});

            return BadRequest(new {Success = false, Message = "User already Exists"});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var success = await _userServices.Login(user);

            if(success != null) return Ok(new {Token = success});

            return Unauthorized(new {Message = "Username or Password is incorrect"}); 
        }

        [HttpGet("GetUserInfoByUsername/{username}")]
        public async Task<IActionResult> GetUserInfoByUsername(string username)
        {
            var user = await _userServices.GetUserInfoByUserName(username);

            if(user != null) return Ok(user);

            return BadRequest(new {Message = " User not found"});
        }
    }
}
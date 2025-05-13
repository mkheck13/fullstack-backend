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

            if (success) return Ok(new { Success = true });

            return BadRequest(new { Success = false, Message = "User already Exists" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            var success = await _userServices.Login(user);

            if (success != null) return Ok(new { Token = success });

            return Unauthorized(new { Message = "Username or Password is incorrect" });
        }

        [HttpGet("GetUserInfoByEmailOrUsername/{emailOrUsername}")]
        public async Task<IActionResult> GetUserInfoByEmailOrUsername(string emailOrUsername)
        {
            var user = await _userServices.GetUserInfoByEmailOrUsername(emailOrUsername);

            if (user != null) return Ok(user);

            return BadRequest(new { Message = " User not found" });
        }

        [HttpPut("UpdateUserInfo/{userId}")]
        public async Task<IActionResult> UpdateUserInfo(int userId, [FromBody] UpdateUserDTO updatedUser)
        {
            bool success = await _userServices.UpdateUserInfo(userId, updatedUser);
            if (success) return Ok(new { Success = true, Message = "User updated successfully" });

            return BadRequest(new { Success = false, Message = "User not found or update failed" });
        }

        [HttpGet("find-spotters")]
        public async Task<IActionResult> FindSpotter([FromQuery] int currentUserId, [FromQuery] bool isSpotter)
        {
            var spotters = await _userServices.FindSpotters(currentUserId, isSpotter);

            if (spotters == null || spotters.Count == 0)
            {
                return NotFound("No spotters found.");
            }

            return Ok(spotters);
        }

        [HttpGet("find-trainers")]
        public async Task<IActionResult> FindTrainers([FromQuery] int currentUserId, [FromQuery] bool isTrainer)
        {
            var trainers = await _userServices.FindTrainers(currentUserId, isTrainer);

            if (trainers == null || trainers.Count == 0)
            {
                return NotFound("No trainers found.");
            }

            return Ok(trainers);
        }


        // Test endpoint

        // [HttpGet("HelloName/{name}")]

        // public string HelloName(string name)
        // {
        //     return $"Hello {name}";

        // }
    }
}
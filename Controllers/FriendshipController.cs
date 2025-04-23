
using fullstack_backend.Models.DTOS;
using fullstack_backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace fullstack_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendshipController : Controller
    {
        private readonly FriendshipServices _friendshipService;

        public FriendshipController(FriendshipServices friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFriend([FromBody] CreateFriendshipDTO addFriendDTO)
        {
            var result = await _friendshipService.AddFriend(addFriendDTO.UserId, addFriendDTO.FriendId);

            if (result)
                return Ok(new ResponseFriendshipDTO { Message = "Friend added successfully.", Success = true });
            else
                return BadRequest(new ResponseFriendshipDTO { Message = "Error adding friend.", Success = false });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriend([FromBody] DeleteFriendshipDTO removeFriendDTO)
        {
            var result = await _friendshipService.RemoveFriend(removeFriendDTO.UserId, removeFriendDTO.FriendId);

            if (result)
                return Ok(new ResponseFriendshipDTO { Message = "Friend removed successfully.", Success = true });
            else
                return BadRequest(new ResponseFriendshipDTO { Message = "Error removing friend.", Success = false });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFriends(int userId)
        {
            var friends = await _friendshipService.GetFriends(userId);

            if (friends != null && friends.Count > 0)
                return Ok(friends);
            else
                return NotFound(new ResponseFriendshipDTO { Message = "No friends found.", Success = false });
        }

        [HttpGet("are-friends/{userId}/{friendId}")]
        public async Task<IActionResult> AreFriends(int userId, int friendId)
        {
            var result = await _friendshipService.AreFriends(userId, friendId);

            return Ok(new ResponseFriendshipDTO { Message = result ? "They are friends." : "They are not friends.", Success = result });
        }
    }
}
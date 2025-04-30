using fullstack_backend.Models;
using fullstack_backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace fullstack_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostServices _postServices;

        public PostController(PostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postServices.GetAllPosts();

            if (posts != null && posts.Count > 0)
                return Ok(posts);
            return BadRequest(new { Message = "No posts found" });
        }

        [HttpGet("GetPostsByUserId/{userId}")]
        public async Task<IActionResult> GetPostsByUserId(int userId)
        {
            var posts = await _postServices.GetPostsByUserId(userId);

            if (posts != null && posts.Count > 0)
                return Ok(posts);

            return BadRequest(new { Message = "No posts found for this user" });
        }

        [HttpGet("GetPostById/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var posts = await _postServices.GetPostById(id);

            if (posts != null)
                return Ok(posts);

            return BadRequest(new { Message = "No posts found" });
        }

        [HttpGet("GetPostsByDate/{date}")]
        public async Task<IActionResult> GetPostsByDate(string date)
        {
            var posts = await _postServices.GetPostsByDate(date);

            if (posts != null)
                return Ok(posts);

            return BadRequest(new { Message = "No posts found" });
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostModel post)
        {
            var success = await _postServices.CreatePost(post);

            if (success) return Ok(new { Success = true, Message = "Post created" });

            return BadRequest(new { Message = "Post not created" });
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] PostModel post)
        {
            var success = await _postServices.UpdatePost(post);

            if (success) return Ok(new { Success = true, Message = "Post updated" });

            return BadRequest(new { Message = "Post not updated" });
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost([FromBody] PostModel post)
        {
            var success = await _postServices.UpdatePost(post);

            if (success) return Ok(new { Success = true, Message = "Post deleted" });

            return BadRequest(new { Message = "Failed to delete post" });
        }










    }
}
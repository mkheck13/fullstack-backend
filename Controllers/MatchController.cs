using fullstack_backend.Models;
using fullstack_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly MatchServices _matchServices;

        public MatchController(MatchServices matchServices)
        {
            _matchServices = matchServices;
        }

        // Get all matches
        [HttpGet("GetAllMatches")]
        public async Task<IActionResult> GetAllMatches()
        {
            var matches = await _matchServices.GetAllMatches();

            if (matches != null && matches.Count > 0)
                return Ok(matches);

            return BadRequest(new {  });
        }

        // Get matches by userId
        [HttpGet("GetMatchesByUserId/{userId}")]
        public async Task<IActionResult> GetMatchesByUserId(int userId)
        {
            var matches = await _matchServices.GetMatchesByUserId(userId);

            if (matches != null)
                return Ok(matches);

            return BadRequest(new {  });
        }

        // Get matches by Id
        [HttpGet("GetMatchesById/{id}")]
        public async Task<IActionResult> GetMatchesById(int id)
        {
            var matches = await _matchServices.GetMatchesById(id);

            if (matches != null)
                return Ok(matches);

            return BadRequest(new {  });
        }

        // Get matches by date
        [HttpGet("GetMatchesByDate/{date}")]
        public async Task<IActionResult> GetMatchesByDate(string date)
        {
            var matches = await _matchServices.GetMatchesByDate(date);

            if (matches != null && matches.Count > 0)
                return Ok(matches);

            return BadRequest(new {  });
        }

        // Create Match
        [HttpPost("CreateMatch")]
        public async Task<IActionResult> CreateMatch([FromBody] MatchModel match)
        {
            var success = await _matchServices.CreateMatch(match);

            if (success) return Ok(new { success = true,  });
            return BadRequest(new {  });
        }

        // Update Match
        [HttpPut("UpdateMatch")]
        public async Task<IActionResult> UpdateMatch([FromBody] MatchModel match)
        {
            var success = await _matchServices.UpdateMatch(match);

            if (success) return Ok(new { success = true  });
            return BadRequest(new {  });
        }

        // Delete Match
        [HttpDelete("DeleteMatch")]
        public async Task<IActionResult> DeleteMatch([FromBody] MatchModel match)
        {
            var success = await _matchServices.UpdateMatch(match);

            if (success) return Ok(new { success = true });
            return BadRequest(new {  });
        }


        














    }
}
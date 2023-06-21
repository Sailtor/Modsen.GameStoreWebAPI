using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("users/{userid}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewForResponceDto>>> GetUserReviews(int userid)
        {
            return Ok(await _reviewService.GetUserReviewsByIdAsync(userid));
        }

        [HttpGet("games/{gameid}/reviews")]
        public async Task<ActionResult<ReviewForResponceDto>> GetGameReviews(int gameid)
        {
            return Ok(await _reviewService.GetGameReviewsByIdAsync(gameid));
        }

        [HttpGet("users/{userid}/reviews/{gameid}")]
        public async Task<ActionResult<ReviewForResponceDto>> GetUserReviewForGame(int userid, int gameid)
        {

            return Ok(await _reviewService.GetGameReviewByIdAsync(gameid, userid));
        }

        [HttpPost("users/{userid}/reviews/{gameid}")]
        public async Task<ActionResult<ReviewForResponceDto>> PostUserReviewForGame(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _reviewService.AddUserReviewAsync(userid, gameid, reviewForCreation);
            return NoContent();
        }

        [HttpPut("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> PutUserReviewForGame(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _reviewService.UpdateUserReviewAsync(userid, gameid, reviewForCreation);
            return NoContent();
        }

        [HttpDelete("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> DeleteUserReviewForGame(int userid, int gameid)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _reviewService.DeleteUserReviewAsync(userid, gameid);
            return Ok();
        }
    }
}

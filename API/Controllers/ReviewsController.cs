using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IAuthService _authService;

        public ReviewsController(IReviewService reviewService, IAuthService authService)
        {
            _reviewService = reviewService;
            _authService = authService;
        }

        [HttpGet("users/{userid}/reviews")]
        public async Task<ActionResult<PagedList<ReviewForResponceDto>>> GetUserReviews(int userid, [FromQuery] ReviewParameters reviewParameters)
        {
            var reviews = await _reviewService.GetUserReviewsByIdAsync(userid, reviewParameters);
            reviews.WritePaginationData(Response.Headers);
            return Ok(reviews);
        }

        [HttpGet("games/{gameid}/reviews")]
        public async Task<ActionResult<PagedList<ReviewForResponceDto>>> GetGameReviews(int gameid, [FromQuery] ReviewParameters reviewParameters)
        {
            var reviews = await _reviewService.GetGameReviewsByIdAsync(gameid, reviewParameters);
            reviews.WritePaginationData(Response.Headers);
            return Ok(reviews);
        }

        [HttpGet("users/{userid}/reviews/{gameid}")]
        public async Task<ActionResult<ReviewForResponceDto>> GetUserReviewForGame(int userid, int gameid)
        {
            return Ok(await _reviewService.GetGameReviewByIdAsync(gameid, userid));
        }

        [HttpPost("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> PostUserReviewForGame(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            _authService.CheckAuthorization(userid, User);
            await _reviewService.AddUserReviewAsync(userid, gameid, reviewForCreation);
            return NoContent();
        }

        [HttpPut("reviews")]
        public async Task<IActionResult> PutUserReviewForGame(ReviewForUpdateDto reviewForUpdate)
        {
            _authService.CheckAuthorization(reviewForUpdate.UserId, User);
            await _reviewService.UpdateUserReviewAsync(reviewForUpdate);
            return NoContent();
        }

        [HttpDelete("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> DeleteUserReviewForGame(int userid, int gameid)
        {
            _authService.CheckAuthorization(userid, User);
            await _reviewService.DeleteUserReviewAsync(userid, gameid);
            return Ok();
        }
    }
}

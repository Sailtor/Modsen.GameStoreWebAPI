using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreWebAPI.Models;
using AutoMapper;
using GameStoreWebAPI.Models.Dtos.Out;
using GameStoreWebAPI.Models.Dtos.In;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DAL.Data;

namespace GameStoreWebAPI.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GameStoreDBContext _context;

        public ReviewsController(IMapper mapper, GameStoreDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("users/{userid}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewForResponceDto>>> GetUserReviews(int userid)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            if (await _context.Users.FindAsync(userid) is null)
            {
                return NotFound();
            }
            var reviews = _context.Reviews.Where(r => r.UserId == userid);
            if (reviews is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<Review>, List<ReviewForResponceDto>>(await reviews.ToListAsync()));
        }

        [HttpGet("games/{gameid}/reviews")]
        public async Task<ActionResult<Review>> GetGameReviews(int gameid)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            if (await _context.Users.FindAsync(gameid) is null)
            {
                return NotFound();
            }
            var reviews = _context.Reviews.Where(r => r.GameId == gameid);
            if (reviews is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<Review>, List<ReviewForResponceDto>>(await reviews.ToListAsync()));
        }

        [HttpGet("users/{userid}/reviews/{gameid}")]
        public async Task<ActionResult<ReviewForResponceDto>> GetUserReviewForGame(int userid, int gameid)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            if (await _context.Users.FindAsync(userid) is null)
            {
                return NotFound();
            }
            if (await _context.Games.FindAsync(gameid) is null)
            {
                return NotFound();
            }

            var review = _context.Reviews.Where(r => r.GameId == gameid).Where(r => r.UserId == userid).FirstOrDefault();
            if (review is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Review, ReviewForResponceDto>(review));
        }

        [HttpPost("users/{userid}/reviews/{gameid}")]
        public async Task<ActionResult<ReviewForResponceDto>> PostUserReviewForGame(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            if (reviewForCreation is null)
            {
                return BadRequest("Review object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            if (!_context.Games.Any(g => g.Id == gameid))
            {
                return NotFound();
            }

            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }

            var mappedReview = _mapper.Map<Review>(reviewForCreation);
            mappedReview.GameId = gameid;
            mappedReview.UserId = userid;

            _context.Reviews.Add(mappedReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserReviewForGame", new { userid, gameid }, _mapper.Map<Review, ReviewForResponceDto>(mappedReview));
        }

        [HttpPut("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> ChangeUserReviewForGame(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }

            if (reviewForCreation is null)
            {
                return BadRequest("Review object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }

            var userReview = _context.Reviews.Find(userid, gameid);

            if (userReview is null)
            {
                return NotFound();
            }

            _mapper.Map(reviewForCreation, userReview);

            _context.Reviews.Update(userReview);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("users/{userid}/reviews/{gameid}")]
        public async Task<IActionResult> DeleteUserReviewForGame(int userid, int gameid)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (HttpContext.User.FindFirstValue(ClaimTypes.Role)!= "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            var userReview = _context.Reviews.Find(userid, gameid);

            if (userReview is null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(userReview);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

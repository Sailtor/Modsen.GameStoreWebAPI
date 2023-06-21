using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("{userid}/games")]
        public async Task<ActionResult<IEnumerable<PurchaseForResponceDto>>> GetUserPurchases(int userid)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            return Ok(await _purchaseService.GetUserPurchasesAsync(userid));
        }

        [HttpGet("{userid}/games/{gameid}")]
        public async Task<ActionResult<PurchaseForResponceDto>> GetUserPurchase(int userid, int gameid)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            return Ok(await _purchaseService.GetUserPurchaseByIdAsync(gameid, userid));
        }

        [HttpPost("{userid}/games/{gameid}")]
        public async Task<IActionResult> PostPurchase(int userid, int gameid, PurchaseForCreationDto purchaseForCreation)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _purchaseService.AddUserPurchaseAsync(purchaseForCreation, gameid, userid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{userid}/games/{gameid}")]
        public async Task<IActionResult> DeletePurchase(int userid, int gameid)
        {
            await _purchaseService.DeleteUserPurchaseAsync(userid, gameid);
            return NoContent();
        }
    }
}

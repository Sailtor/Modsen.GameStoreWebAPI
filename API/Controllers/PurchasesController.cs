using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using BLL.Services.Implementation;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IAuthService _authService;

        public PurchasesController(IPurchaseService purchaseService, IAuthService authService)
        {
            _purchaseService = purchaseService;
            _authService = authService;
        }

        [HttpGet("{userid}/games")]
        public async Task<ActionResult<PagedList<PurchaseForResponceDto>>> GetUserPurchases(int userid, [FromQuery] PurchaseParameters purchaseParameters)
        {
            _authService.CheckAuthorization(userid, User);
            var purchases = await _purchaseService.GetUserPurchasesAsync(userid, purchaseParameters);
            purchases.WritePaginationData(Response.Headers);
            return Ok(purchases);
        }

        [HttpGet("{userid}/games/{gameid}")]
        public async Task<ActionResult<PurchaseForResponceDto>> GetUserPurchase(int userid, int gameid)
        {
            _authService.CheckAuthorization(userid, User);
            return Ok(await _purchaseService.GetUserPurchaseByIdAsync(gameid, userid));
        }

        [HttpPost("{userid}/games/{gameid}")]
        public async Task<IActionResult> PostPurchase(int userid, int gameid, PurchaseForCreationDto purchaseForCreation)
        {
            _authService.CheckAuthorization(userid, User);
            await _purchaseService.AddUserPurchaseAsync(purchaseForCreation, gameid, userid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{userid}/games/{gameid}")]
        public async Task<IActionResult> DeletePurchase(int userid, int gameid)
        {
            await _purchaseService.DeleteUserPurchaseAsync(gameid, userid);
            return NoContent();
        }
    }
}

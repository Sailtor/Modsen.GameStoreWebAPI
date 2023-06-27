using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IPurchaseService
    {
        Task<PagedList<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid, PurchaseParameters parameters);
        Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int gameid, int userid);
        Task AddUserPurchaseAsync(PurchaseForCreationDto purchase, int gameid, int userid);
        Task DeleteUserPurchaseAsync(int gameid, int userid);
    }
}
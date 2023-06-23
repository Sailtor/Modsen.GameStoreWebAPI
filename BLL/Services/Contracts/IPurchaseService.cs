using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid);
        Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int gameid, int userid);
        Task AddUserPurchaseAsync(PurchaseForCreationDto purchase, int gameid, int userid);
        Task DeleteUserPurchaseAsync(int gameid, int userid);
    }
}
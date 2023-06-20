using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IPurchaseService
    {
        public Task<IEnumerable<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid);
        public Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int gameid, int userid);
        public Task AddUserPurchaseAsync(PurchaseForCreationDto purchase, int gameid, int userid);
        public Task DeleteUserPurchaseAsync(int gameid, int userid);
    }
}
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Contracts
{
    public interface IPurchaseService
    {
        public Task<IEnumerable<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid);
        public Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int userid , int gameid);
        public Task AddUserPurchaseAsync(PurchaseForCreationDto purchase, int userid, int gameid);
        public Task DeleteUserPurchaseAsync(int usergameid);
    }
}
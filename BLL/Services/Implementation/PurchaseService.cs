using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid)
        {
            return _mapper.Map<IEnumerable<PurchaseForResponceDto>>(await _unitOfWork.Purchase.FindAsync(p => p.UserId == userid));
        }

        public async Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int gameid, int userid)
        {
            CompoundKeyUserGame key = new()
            {
                UserId = userid,
                GameId = gameid
            };
            return _mapper.Map<PurchaseForResponceDto>(await _unitOfWork.Purchase.GetByIdAsync(key));
        }

        public async Task AddUserPurchaseAsync(PurchaseForCreationDto purchaseForCreation, int gameid, int userid)
        {
            var purchase = _mapper.Map<Purchase>(purchaseForCreation);
            purchase.GameId = gameid;
            purchase.UserId = userid;
            await _unitOfWork.Purchase.AddAsync(purchase);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserPurchaseAsync(int gameid, int userid)
        {
            CompoundKeyUserGame key = new()
            {
                UserId = userid,
                GameId = gameid
            };
            _ = await _unitOfWork.Purchase.GetByIdAsync(key);
            await _unitOfWork.Purchase.Delete(key);
            await _unitOfWork.SaveAsync();
        }
    }
}

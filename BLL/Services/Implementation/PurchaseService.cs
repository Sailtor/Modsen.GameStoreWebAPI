using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<PurchaseForCreationDto> _creationValidator;
        public PurchaseService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<PurchaseForCreationDto> creationValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
        }

        public async Task<PagedList<PurchaseForResponceDto>> GetUserPurchasesAsync(int userid, PurchaseParameters parameters)
        {
            return _mapper.Map<PagedList<PurchaseForResponceDto>>(await _unitOfWork.Purchase.FindAsync(p => p.UserId == userid, parameters));
        }

        public async Task<PurchaseForResponceDto> GetUserPurchaseByIdAsync(int gameid, int userid)
        {
            return _mapper.Map<PurchaseForResponceDto>(await _unitOfWork.Purchase.GetByIdAsync(userid, gameid));
        }

        public async Task AddUserPurchaseAsync(PurchaseForCreationDto purchaseForCreation, int gameid, int userid)
        {
            _creationValidator.ValidateAndThrowCustom(purchaseForCreation);
            var purchase = _mapper.Map<Purchase>(purchaseForCreation);
            purchase.GameId = gameid;
            purchase.UserId = userid;
            await _unitOfWork.Purchase.AddAsync(purchase);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserPurchaseAsync(int gameid, int userid)
        {
            _ = await _unitOfWork.Purchase.GetByIdAsync(userid, gameid);
            await _unitOfWork.Purchase.Delete(userid, gameid);
            await _unitOfWork.SaveAsync();
        }
    }
}

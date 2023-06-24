using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReviewForResponceDto>> GetUserReviewsByIdAsync(int userid)
        {
            return _mapper.Map<IEnumerable<ReviewForResponceDto>>(await _unitOfWork.Review.FindAsync(p => p.UserId == userid));
        }

        public async Task<IEnumerable<ReviewForResponceDto>> GetGameReviewsByIdAsync(int gameid)
        {
            return _mapper.Map<IEnumerable<ReviewForResponceDto>>(await _unitOfWork.Review.FindAsync(p => p.GameId == gameid));
        }

        public async Task<ReviewForResponceDto> GetGameReviewByIdAsync(int gameid, int userid)
        {
            return _mapper.Map<ReviewForResponceDto>(await _unitOfWork.Review.GetByIdAsync(userid, gameid));
        }

        public async Task AddUserReviewAsync(int userid, int gameid, ReviewForCreationDto reviewForCreation)
        {
            Review review = _mapper.Map<Review>(reviewForCreation);
            review.UserId = userid;
            review.GameId = gameid;
            await _unitOfWork.Review.AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserReviewAsync(ReviewForUpdateDto reviewForUpdate)
        {
            Review review = await _unitOfWork.Review.GetByIdAsync(reviewForUpdate.UserId, reviewForUpdate.GameId);
            _mapper.Map(reviewForUpdate, review);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserReviewAsync(int userid, int gameid)
        {
            _ = await _unitOfWork.Review.GetByIdAsync(userid, gameid);
            await _unitOfWork.Review.Delete(userid, gameid);
            await _unitOfWork.SaveAsync();
        }
    }
}

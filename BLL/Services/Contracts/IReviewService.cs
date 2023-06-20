using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewForResponceDto>> GetAllUserReviewByIdAsync(int userid);
        public Task<ReviewForResponceDto> GetAllGameReviewByIdAsync(int gameid);
        public Task<ReviewForResponceDto> GetGameUserReviewByIdAsync(int usergameid);
        public Task AddUserReviewAsync(ReviewForCreationDto review, int usergameid);
        public Task UpdateUserReviewAsync(int usergameid, ReviewForCreationDto review);
        public Task DeleteUserReviewAsync(int usergameid);
    }
}
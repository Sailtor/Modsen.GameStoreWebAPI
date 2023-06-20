using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewForResponceDto>> GetUserReviewsByIdAsync(int userid);
        public Task<IEnumerable<ReviewForResponceDto>> GetGameReviewsByIdAsync(int gameid);
        public Task<ReviewForResponceDto> GetGameReviewByIdAsync(int gameid, int userid);
        public Task AddUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        public Task UpdateUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        public Task DeleteUserReviewAsync(int userid, int gameid);
    }
}
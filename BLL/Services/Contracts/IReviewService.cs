using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewForResponceDto>> GetUsersReviewByIdAsync(int userid);
        public Task<ReviewForResponceDto> GetGameReviewByIdAsync(int IEnumerable, int gameid );
        public Task<ReviewForResponceDto> GetGameUserReviewByIdAsync(int userid, int gameid);
        public Task AddUserReviewAsync(ReviewForCreationDto review, int userid, int gameid);
        public Task UpdateUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        public Task DeleteUserReviewAsync(int userid,int gameid);
    }
}
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewForResponceDto>> GetUserReviewsByIdAsync(int userid);
        Task<IEnumerable<ReviewForResponceDto>> GetGameReviewsByIdAsync(int gameid);
        Task<ReviewForResponceDto> GetGameReviewByIdAsync(int gameid, int userid);
        Task AddUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        Task UpdateUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        Task DeleteUserReviewAsync(int userid, int gameid);
    }
}
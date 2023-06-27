using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IReviewService
    {
        Task<PagedList<ReviewForResponceDto>> GetUserReviewsByIdAsync(int userid, ReviewParameters parameters);
        Task<PagedList<ReviewForResponceDto>> GetGameReviewsByIdAsync(int gameid, ReviewParameters parameters);
        Task<ReviewForResponceDto> GetGameReviewByIdAsync(int gameid, int userid);
        Task AddUserReviewAsync(int userid, int gameid, ReviewForCreationDto review);
        Task UpdateUserReviewAsync(ReviewForUpdateDto reviewForUpdate);
        Task DeleteUserReviewAsync(int userid, int gameid);
    }
}
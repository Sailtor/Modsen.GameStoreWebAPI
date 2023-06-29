using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IReviewRepository : IRepository<Review, int>
    {
        Task<PagedList<Review>> GetGameReviewsAsync(int gameid, ReviewParameters parameters);
        Task<PagedList<Review>> GetUserReviewsAsync(int userid, ReviewParameters parameters);
    }
}
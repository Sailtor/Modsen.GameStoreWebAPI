using DAL.Models;

namespace DAL.Repository.Contracts
{
    public interface IReviewRepository : IRepository<Review, int>
    {
        Task<IEnumerable<Review>> GetGameReviewsAsync(int gameid);
        Task<IEnumerable<Review>> GetUserReviewsAsync(int userid);
    }
}

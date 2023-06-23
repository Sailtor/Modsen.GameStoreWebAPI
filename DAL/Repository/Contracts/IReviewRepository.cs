using DAL.Models;

namespace DAL.Repository.Contracts
{
    public interface IReviewRepository : IRepository<Review, CompoundKeyUserGame>
    {
        Task<IEnumerable<Review>> GetGameReviewsAsync(int gameid);
        Task<IEnumerable<Review>> GetUserReviewsAsync(int userid);
    }
}

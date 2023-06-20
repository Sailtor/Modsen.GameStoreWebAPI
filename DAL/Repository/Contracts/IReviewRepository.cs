using DAL.Models;

namespace DAL.Repository.Contracts
{
    public interface IReviewRepository : IRepository<Review, CompoundKeyUserGame>
    {
        public Task<IEnumerable<Review>> GetGameReviewsAsync(int gameid);
        public Task<IEnumerable<Review>> GetUserReviewsAsync(int userid);
    }
}

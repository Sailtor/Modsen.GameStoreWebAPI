using DAL.Models;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class ReviewRepository : RepositoryBase<Review, CompoundKeyUserGame>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetGameReviewsAsync(int gameid)
        {
            return await _context.Set<Review>().Where(r => r.GameId == gameid).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetUserReviewsAsync(int userid)
        {
            return await _context.Set<Review>().Where(r => r.UserId == userid).ToListAsync();
        }
    }
}
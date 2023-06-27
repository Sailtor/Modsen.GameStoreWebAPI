using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class ReviewRepository : RepositoryBase<Review, int>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {
        }

        public async Task<PagedList<Review>> GetGameReviewsAsync(int gameid, ReviewParameters parameters)
        {
            return PagedList<Review>.ToPagedList(_context.Set<Review>()
                .Where(r => r.GameId == gameid), parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Review>> GetUserReviewsAsync(int userid, ReviewParameters parameters)
        {
            return PagedList<Review>.ToPagedList(_context.Set<Review>()
                .Where(r => r.UserId == userid), parameters.PageNumber, parameters.PageSize);
        }
    }
}
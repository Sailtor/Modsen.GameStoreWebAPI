using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repository.Impementation
{
    public class ReviewRepository : RepositoryBase<Review, int>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {
        }

        public async Task<PagedList<Review>> GetGameReviewsAsync(int gameid, ReviewParameters parameters)
        {
            var list = PagedList<Review>.ToPagedList(_context.Set<Review>()
                .Where(r => r.GameId == gameid), parameters.PageNumber, parameters.PageSize);

            if ((list is null) || (!list.Any()))
            {
                throw new DatabaseNotFoundException();
            }

            return list;
        }

        public async Task<PagedList<Review>> GetUserReviewsAsync(int userid, ReviewParameters parameters)
        {
            var list = PagedList<Review>.ToPagedList(_context.Set<Review>()
                .Where(r => r.UserId == userid), parameters.PageNumber, parameters.PageSize);

            if ((list is null) || (!list.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return list;
        }
    }
}
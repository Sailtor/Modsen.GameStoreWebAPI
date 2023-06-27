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
            var list = _context.Set<Review>()
                .Where(r => r.GameId == gameid)
                .AsQueryable();

            if (parameters.MinReviewDate is not null)
            {
                list = list.Where(g => g.ReviewDate >= parameters.MinReviewDate && g.ReviewDate <= parameters.MaxReviewDate);
            }
            if (parameters.MinScore is not null && parameters.MaxScore is not null)
            {
                list = list.Where(g => g.Score >= parameters.MinScore && g.Score <= parameters.MaxScore);
            }

            var pagedList = PagedList<Review>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }

        public async Task<PagedList<Review>> GetUserReviewsAsync(int userid, ReviewParameters parameters)
        {
            var list = _context.Set<Review>()
                .Where(r => r.UserId == userid)
                .AsQueryable();

            if (parameters.MinReviewDate is not null)
            {
                list = list.Where(g => g.ReviewDate >= parameters.MinReviewDate && g.ReviewDate <= parameters.MaxReviewDate);
            }
            if (parameters.MinScore is not null)
            {
                list = list.Where(g => g.Score >= parameters.MinScore && g.Score <= parameters.MaxScore);
            }

            var pagedList = PagedList<Review>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }
    }
}
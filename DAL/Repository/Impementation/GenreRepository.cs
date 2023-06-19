using DAL.Models;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }
    }
}

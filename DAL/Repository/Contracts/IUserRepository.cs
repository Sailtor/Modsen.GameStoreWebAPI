using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
        public Task<PagedList<User>> GetAllFilteredAsync(UserParameters parameters);
    }
}

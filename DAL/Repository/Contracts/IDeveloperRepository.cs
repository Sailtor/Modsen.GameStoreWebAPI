using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IDeveloperRepository : IRepository<Developer, int>
    {
        Task<PagedList<Developer>> GetAllDevelopersAsync(DeveloperParameters parameters);
    }
}
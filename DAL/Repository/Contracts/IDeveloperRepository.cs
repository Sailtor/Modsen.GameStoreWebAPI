using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IDeveloperRepository : IRepository<Developer,int>
    {
        public PagedList<Developer> GetAllDevelopers(DeveloperParameters parameters);
    }
}

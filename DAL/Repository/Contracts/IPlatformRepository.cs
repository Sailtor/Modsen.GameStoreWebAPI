using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IPlatformRepository : IRepository<Platform, int>
    {
        public PagedList<Platform> GetAllPlatforms(PlatformParameters parameters);
    }
}
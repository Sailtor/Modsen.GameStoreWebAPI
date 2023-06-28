using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        PagedList<Role> GetAllRoles(RoleParameters parameters);
    }
}

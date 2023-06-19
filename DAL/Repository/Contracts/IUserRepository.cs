using GameStoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}

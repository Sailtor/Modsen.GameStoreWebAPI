using DAL.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDeveloperRepository Developer { get; }
        IGameRepository Game { get; }
        IGenreRepository Genre { get; }
        IPlatformRepository Platform { get; }
        IPurchaseRepository Purchase { get; }
        IReviewRepository Review { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}

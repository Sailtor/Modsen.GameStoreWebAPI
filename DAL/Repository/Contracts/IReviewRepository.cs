using DAL.Models;
using GameStoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Contracts
{
    public interface IReviewRepository : IRepository<Review, CompoundKeyUserGame>
    {

    }
}

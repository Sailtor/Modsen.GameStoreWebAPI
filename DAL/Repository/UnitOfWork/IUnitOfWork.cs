using DAL.Repository.Contracts;

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
using DAL.Data;
using DAL.Exceptions;
using DAL.Repository.Contracts;
using DAL.Repository.Impementation;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDBContext _context;

        private IDeveloperRepository _developer;
        private IGameRepository _game;
        private IGenreRepository _genre;
        private IPlatformRepository _platform;
        private IPurchaseRepository _purchase;
        private IReviewRepository _review;
        private IRoleRepository _role;
        private IUserRepository _user;

        public IDeveloperRepository Developer
        {
            get
            {
                _developer ??= new DeveloperRepository(_context);

                return _developer;
            }
        }
        public IGameRepository Game
        {
            get
            {
                _game ??= new GameRepository(_context);

                return _game;
            }
        }
        public IGenreRepository Genre
        {
            get
            {
                _genre ??= new GenreRepository(_context);

                return _genre;
            }
        }
        public IPlatformRepository Platform
        {
            get
            {
                _platform ??= new PlatformRepository(_context);

                return _platform;
            }
        }
        public IPurchaseRepository Purchase
        {
            get
            {
                _purchase ??= new PurchaseRepository(_context);

                return _purchase;
            }
        }
        public IReviewRepository Review
        {
            get
            {
                _review ??= new ReviewRepository(_context);

                return _review;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                _role ??= new RoleRepository(_context);

                return _role;
            }
        }

        public IUserRepository User
        {
            get
            {
                _user ??= new UserRepository(_context);

                return _user;
            }
        }

        public UnitOfWork(GameStoreDBContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new DatabaseSaveFailedException();
            }
        }
    }
}

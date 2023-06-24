using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Data
{
    public partial class GameStoreDBContext : DbContext
    {
        public GameStoreDBContext()
        {
        }

        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Developer> Developers { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(2048);

                entity.Property(e => e.DeveloperId).HasColumnName("DeveloperID");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__Developer__31EC6D26");

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Games)
                    .UsingEntity<Dictionary<string, object>>(
                        "GamesGenre",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GamesGenr__Genre__3A81B327"),
                        r => r.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GamesGenr__GameI__398D8EEE"),
                        j =>
                        {
                            j.HasKey("GameId", "GenreId").HasName("PK__GamesGen__DA80C78846F82486");

                            j.ToTable("GamesGenres");

                            j.IndexerProperty<int>("GameId").HasColumnName("GameID");

                            j.IndexerProperty<int>("GenreId").HasColumnName("GenreID");
                        });

                entity.HasMany(d => d.Platforms)
                    .WithMany(p => p.Games)
                    .UsingEntity<Dictionary<string, object>>(
                        "GamesPlatform",
                        l => l.HasOne<Platform>().WithMany().HasForeignKey("PlatformId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GamesPlat__Platf__36B12243"),
                        r => r.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__GamesPlat__GameI__35BCFE0A"),
                        j =>
                        {
                            j.HasKey("GameId", "PlatformId").HasName("PK__GamesPla__95ED08B0F9E5EDE7");

                            j.ToTable("GamesPlatforms");

                            j.IndexerProperty<int>("GameId").HasColumnName("GameID");

                            j.IndexerProperty<int>("PlatformId").HasColumnName("PlatformID");
                        });
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GameId })
                    .HasName("PK__Purchase__D52345D10AF4019E");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__GameI__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__UserI__3D5E1FD2");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GameId })
                    .HasName("PK__Review__D52345D18161759A");

                entity.ToTable("Reviews");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewText).HasMaxLength(2048);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__GameID__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__UserID__412EB0B6");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login, "UQ__Users__5E55825B25F41094")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F2941804")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Login).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.RefreshToken).HasMaxLength(256);

                entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleID__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

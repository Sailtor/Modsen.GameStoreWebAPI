using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Game
    {
        public Game()
        {
            Purchases = new HashSet<Purchase>();
            Reviews = new HashSet<Review>();
            Genres = new HashSet<Genre>();
            Platforms = new HashSet<Platform>();
        }

        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public virtual Developer Developer { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Platform> Platforms { get; set; }
    }
}

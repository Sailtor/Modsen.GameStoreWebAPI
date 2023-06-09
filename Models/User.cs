using System;
using System.Collections.Generic;

namespace GameStoreWebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Purchases = new HashSet<Purchase>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

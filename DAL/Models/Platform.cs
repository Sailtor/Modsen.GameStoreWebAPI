using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Platform
    {
        public Platform()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}

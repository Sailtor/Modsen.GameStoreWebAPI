using System;
using System.Collections.Generic;

namespace GameStoreWebAPI.Models
{
    public partial class Purchase
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public virtual Game Game { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

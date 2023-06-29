namespace DAL.Models
{
    public partial class Review
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Game Game { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
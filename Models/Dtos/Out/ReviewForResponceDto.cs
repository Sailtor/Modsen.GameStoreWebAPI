namespace GameStoreWebAPI.Models.Dtos.Out
{
    public class ReviewForResponceDto
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public byte Score { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
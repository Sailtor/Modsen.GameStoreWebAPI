namespace BLL.Dtos.OutDto
{
    public class ReviewForResponceDto
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
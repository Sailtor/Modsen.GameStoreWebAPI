namespace BLL.Dtos.InDto
{
    public class ReviewForUpdateDto
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
        public string? ReviewText { get; set; }
    }
}
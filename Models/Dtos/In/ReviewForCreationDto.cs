namespace GameStoreWebAPI.Models.Dtos.In
{
    public class ReviewForCreationDto
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public byte Score { get; set; }
        public string? ReviewText { get; set; }
    }
}
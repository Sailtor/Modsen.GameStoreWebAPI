namespace GameStoreWebAPI.Models.Dtos.In
{
    public class ReviewForCreationDto
    {
        public byte Score { get; set; }
        public string? ReviewText { get; set; }
    }
}
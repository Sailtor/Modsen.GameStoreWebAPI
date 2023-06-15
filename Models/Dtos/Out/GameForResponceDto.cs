namespace GameStoreWebAPI.Models.Dtos.Out
{
    public class GameForResponceDto
    {

        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public byte? Score { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}

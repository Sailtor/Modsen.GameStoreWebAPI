namespace BLL.Dtos.InDto
{
    public class GameForCreationDto
    {
        public int DeveloperId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
namespace BLL.Dtos.InDto
{
    public class GameForUpdateDto
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
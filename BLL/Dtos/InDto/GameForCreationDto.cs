using System.ComponentModel.DataAnnotations;
namespace BLL.Dtos.InDto
{
    public class GameForCreationDto
    {

        public int DeveloperId { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [StringLength(2048)]
        public string? Description { get; set; }
        [Required]
        [Range(1, 200000)]
        public decimal Price { get; set; }
    }
}
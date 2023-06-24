using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class GenreForCreationDto
    {
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
using System.ComponentModel.DataAnnotations;
namespace BLL.Dtos.InDto
{
    public class DeveloperForCreationDto
    {
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
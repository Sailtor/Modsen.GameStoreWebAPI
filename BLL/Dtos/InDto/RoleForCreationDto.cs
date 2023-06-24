using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class RoleForCreationDto
    {
        [Required]
        [StringLength(256, MinimumLength = 3)]
        public string Name { get; set; } = null!;
    }
}
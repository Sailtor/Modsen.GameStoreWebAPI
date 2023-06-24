using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class RoleForUpdateDto
    {
        [Required]
        [Range(1, 100)]
        public int Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 3)]
        public string Name { get; set; } = null!;
    }
}
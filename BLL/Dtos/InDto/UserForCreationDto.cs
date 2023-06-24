using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class UserForCreationDto
    {
        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Login { get; set; } = null!;
        [Required]
        [StringLength(256, MinimumLength = 8)]
        public string Password { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
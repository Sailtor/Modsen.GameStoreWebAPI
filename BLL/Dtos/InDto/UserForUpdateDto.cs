using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class UserForUpdateDto
    {
        [Required]
        public int Id { get; set; }
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
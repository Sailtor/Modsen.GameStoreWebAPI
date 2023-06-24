using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class ReviewForCreationDto
    {
        [Required]
        [Range(0, 5)]
        public byte Score { get; set; }
        [StringLength(2048, MinimumLength = 3)]
        public string? ReviewText { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.InDto
{
    public class ReviewForUpdateDto
    {
        [Required]
        [Range(1, 100)]
        public int UserId { get; set; }
        [Required]
        [Range(1, 100)]
        public int GameId { get; set; }
        [Required]
        [Range(0, 5)]
        public byte Score { get; set; }
        [StringLength(2048, MinimumLength = 3)]
        public string? ReviewText { get; set; }
    }
}
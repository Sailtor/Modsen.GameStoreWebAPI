namespace GameStoreWebAPI.Models.Dtos.In
{
    public class UserForCreationDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
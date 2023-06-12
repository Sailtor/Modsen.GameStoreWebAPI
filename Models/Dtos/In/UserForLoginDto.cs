namespace GameStoreWebAPI.Models.Dtos.In
{
    public class UserForLoginDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
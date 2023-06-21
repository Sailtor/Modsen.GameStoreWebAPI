namespace BLL.Dtos.OutDto
{
    public class UserForResponceDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
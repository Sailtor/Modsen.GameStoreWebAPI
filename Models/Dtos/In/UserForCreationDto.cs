﻿namespace GameStoreWebAPI.Models.Dtos.In
{
    public class UserForCreationDto
    {
        public string RoleName { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
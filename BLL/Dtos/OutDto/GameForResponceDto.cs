﻿namespace BLL.Dtos.OutDto
{
    public class GameForResponceDto
    {

        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public double? Score { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}

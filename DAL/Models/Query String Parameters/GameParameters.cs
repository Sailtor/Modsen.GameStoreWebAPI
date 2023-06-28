namespace DAL.Models.Query_String_Parameters
{
    public class GameParameters : QueryStringParameters
    {
        public int? DeveloperId { get; set; }
        public DateTime? MinReleaseDate { get; set; }
        public DateTime? MaxReleaseDate { get; set; } = DateTime.Now;
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        /*public double? MaxScore { get; set; }
        public double? MinScore { get; set; }*/
        public ICollection<int>? GenresIds { get; set; }
        public ICollection<int>? PlatformsIds { get; set; }
        public string? SearchName { get; set; }
        public string? SearchDesc { get; set; }
    }
}

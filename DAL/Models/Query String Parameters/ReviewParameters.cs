namespace DAL.Models.Query_String_Parameters
{
    public class ReviewParameters : QueryStringParameters
    {
        public int? MaxScore { get; set; } = 5;
        public int? MinScore { get; set; } = 0;
        public DateTime? MaxReviewDate { get; set; } = DateTime.Now;
        public DateTime? MinReviewDate { get; set; }
        public string? SearchText { get; set; }
    }
}

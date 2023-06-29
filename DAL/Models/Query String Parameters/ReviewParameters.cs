namespace DAL.Models.Query_String_Parameters
{
    public class ReviewParameters : QueryStringParameters
    {
        public int? MaxScore { get; set; }
        public int? MinScore { get; set; }
        public DateTime? MinReviewDate { get; set; }
        public DateTime? MaxReviewDate { get; set; }
        public string? SearchText { get; set; }
    }
}
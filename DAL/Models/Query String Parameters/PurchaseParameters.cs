namespace DAL.Models.Query_String_Parameters
{
    public class PurchaseParameters : QueryStringParameters
    {
        public DateTime? MaxPurchaseDate { get; set; } = DateTime.Now;
        public DateTime? MinPurchaseDate { get; set; }
    }
}

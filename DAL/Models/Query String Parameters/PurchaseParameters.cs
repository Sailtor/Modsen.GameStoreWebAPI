namespace DAL.Models.Query_String_Parameters
{
    public class PurchaseParameters : QueryStringParameters
    {
        public DateTime? MaxPurchaseDate { get; set; }
        public DateTime? MinPurchaseDate { get; set; }
    }
}
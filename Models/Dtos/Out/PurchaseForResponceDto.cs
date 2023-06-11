namespace GameStoreWebAPI.Models.Dtos.Out
{
    public class PurchaseForResponceDto
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
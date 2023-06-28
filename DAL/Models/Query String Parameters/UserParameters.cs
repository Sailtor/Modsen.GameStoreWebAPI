namespace DAL.Models.Query_String_Parameters
{
    public class UserParameters : QueryStringParameters
    {
        public int? RoleId { get; set; }
        public string? SearchLogin { get; set; }
        public string? SearchEmail { get; set; }
    }
}

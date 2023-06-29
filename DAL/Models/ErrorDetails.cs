using System.Text.Json;

namespace DAL.Models
{
    public class ErrorDetails
    {
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
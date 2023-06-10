namespace GameStoreWebAPI.Models
{
    public class PlatformForCreationDto
    {
        public PlatformForCreationDto()
        {
            Games = new HashSet<Game>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}

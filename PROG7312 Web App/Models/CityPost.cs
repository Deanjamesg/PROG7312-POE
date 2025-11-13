namespace PROG7312_Web_App.Models
{
    public class CityPost
    {
        public Guid Id { get; private set; }

        public CityPostType CityPostType { get; set; } // Event or Announcement

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DatePublished { get; set; } = DateTime.Now;

        // Only for Local Events
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventCategory? EventCategory { get; set; }
        public string? Location { get; set; }

        public CityPost()
        {
            Id = Guid.NewGuid();
        }


    }

}

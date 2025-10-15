namespace PROG7312_Web_App.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "City of Cape Town Post / Notice";
        public string Description { get; set; } = "Contact us to find out more about this post!";
        public PostType Type { get; set; }
        public DateTime DatePublished { get; set; }

        // Events Only
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
        public EventCategory? Category { get; set; }

        public override string ToString()
        {
            string post = $"ID: {Id} \nTitle: {Title} \nDescription: {Description} \nType: {Type.GetDisplayName()} \nDate Published: {DatePublished.ToString()}";
            return post;
        }
    }
}

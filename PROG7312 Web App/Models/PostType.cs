namespace PROG7312_Web_App.Models
{
    public enum PostType
    {
        LocalEvent,
        Announcement
    }

    public static class PostTypeExtensions
    {
        public static string GetDisplayName(this PostType type)
        {
            return type switch
            {
                PostType.LocalEvent => "Local Events",
                PostType.Announcement => "Announcements",
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown Post Type")
            };
        }
    }
}

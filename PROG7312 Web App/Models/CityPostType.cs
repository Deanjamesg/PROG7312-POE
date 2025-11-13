namespace PROG7312_Web_App.Models
{
    public enum CityPostType
    {
        LocalEvent,
        Announcement
    }

    public static class CityPostTypeExtensions
    {
        public static string DisplayName(this PostType type)
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

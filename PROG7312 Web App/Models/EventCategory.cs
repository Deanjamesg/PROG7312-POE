namespace PROG7312_Web_App.Models
{
    public enum EventCategory
    {
        CultureAndCreativity,
        Sports,
        BusinessAndInnovation,
        Environment,
        Community
    }

    public static class EventCategoryExtensions
    {
        public static string GetDisplayName(this EventCategory category)
        {
            return category switch
            {
                EventCategory.CultureAndCreativity => "Culture and Creativity",
                EventCategory.Sports => "Sports",
                EventCategory.BusinessAndInnovation => "Business and Innovation",
                EventCategory.Environment => "Environment",
                EventCategory.Community => "Community",
                _ => throw new ArgumentOutOfRangeException(nameof(category), "Unknown Category")
            };
        }
    }
}

using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.ViewModels
{
    public class HomeViewModel
    {
        public List<CityPost> RecentPosts { get; set; } = new List<CityPost>();
        public List<CityPost> RecentlyViewedPosts { get; set; } = new List<CityPost>();

        public List<CityPost> RecommendedPosts { get; set; } = new List<CityPost>();
        public string? RecommendationReason { get; set; }
    }
}

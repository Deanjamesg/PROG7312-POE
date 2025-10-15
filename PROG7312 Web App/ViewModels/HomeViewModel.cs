using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> RecentPosts { get; set; } = new List<Post>();
        public List<Post> RecentlyViewedPosts { get; set; } = new List<Post>();

        public List<Post> RecommendedPosts { get; set; } = new List<Post>();
        public string? RecommendationReason { get; set; }
    }
}

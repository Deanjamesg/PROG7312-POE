using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.Models;
using PROG7312_Web_App.ViewModels;
using System.Diagnostics;

namespace PROG7312_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            viewModel.RecentPosts = AppData.Instance.Posts.Values
                                           .OrderByDescending(p => p.DatePublished)
                                           .Take(4)
                                           .ToList();

            viewModel.RecentlyViewedPosts = AppData.Instance.RecentlyViewed
                                                   .Take(8)
                                                   .ToList();

            var searchAnalytics = AppData.Instance.SearchAnalytics;
            string? favoriteCategory = null;

            if (searchAnalytics.Any())
            {
                favoriteCategory = searchAnalytics.OrderByDescending(kvp => kvp.Value).First().Key;
            }

            if (favoriteCategory != null && Enum.TryParse<EventCategory>(favoriteCategory, out var favCatEnum))
            {
                viewModel.RecommendationReason = $"Because you like {favCatEnum.GetDisplayName()}";
                viewModel.RecommendedPosts = AppData.Instance.Posts.Values
                    .Where(p => p.Type == PostType.LocalEvent &&
                                 p.Category.HasValue && p.Category.Value == favCatEnum &&
                                 p.StartTime.HasValue && p.StartTime > DateTime.Now)
                    .OrderBy(p => p.StartTime)
                    .Take(3)
                    .ToList();
            }
            else
            {
                viewModel.RecommendationReason = "Upcoming in Your City";
                viewModel.RecommendedPosts = AppData.Instance.Posts.Values
                    .Where(p => p.Type == PostType.LocalEvent &&
                                 p.StartTime.HasValue && p.StartTime > DateTime.Now)
                    .OrderBy(p => p.StartTime)
                    .Take(3)
                    .ToList();
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

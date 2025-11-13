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

            viewModel.RecentPosts = CityPostData.RecentNews.ToList();

            viewModel.RecentlyViewedPosts = CityPostData.RecentlyViewed.Take(8).ToList();

            var searchAnalytics = CityPostData.SearchAnalytics;

            string? favoriteCategory = null;

            if (searchAnalytics.Any())
            {
                favoriteCategory = searchAnalytics.OrderByDescending(kvp => kvp.Value).First().Key;
            }

            var allPosts = CityPostData.CityPostsById.Values;

            if (favoriteCategory != null && Enum.TryParse<EventCategory>(favoriteCategory, out var favCatEnum))
            {
                viewModel.RecommendationReason = $"Because you like {favCatEnum.GetDisplayName()}";

                viewModel.RecommendedPosts = allPosts
                    .Where(p => p.CityPostType == CityPostType.LocalEvent &&
                                p.EventCategory.HasValue && p.EventCategory.Value == favCatEnum &&
                                p.StartDate.HasValue && p.StartDate > DateTime.Now)
                    .OrderBy(p => p.StartDate)
                    .Take(3)
                    .ToList();
            }
            else
            {
                viewModel.RecommendationReason = "Upcoming in Your City";

                viewModel.RecommendedPosts = allPosts
                    .Where(p => p.CityPostType == CityPostType.LocalEvent &&
                                p.StartDate.HasValue && p.StartDate > DateTime.Now)
                    .OrderBy(p => p.StartDate)
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

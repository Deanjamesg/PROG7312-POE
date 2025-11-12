using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Controllers
{
    public class PostController : Controller
    {
        public IActionResult ViewPosts()
        {
            var allPosts = AppData.Instance.Posts.Values.ToList();

            ViewData["ActivePage"] = "ViewPosts";

            return View(allPosts);
        }

        public IActionResult GetPostDetails(int Id)
        {
            if (AppData.Instance.Posts.TryGetValue(Id, out Post post))
            {
                // Add the Clicked Post to the RecentlyViewed Stack
                AppData.Instance.RecentlyViewed.Push(post);

                // Return a New Partial View Designed for the Modal
                return PartialView("_PostDetails", post);
            }

            return NotFound();
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable?view=net-8.0
        // https://learn.microsoft.com/en-us/aspnet/web-forms/overview/older-versions-getting-started/aspnet-ajax/understanding-asp-net-ajax-web-services
        public IActionResult FilterPosts(string searchTerm, string postType, string category, DateTime? startDate, DateTime? endDate,
            string sortOrder)
        {
            // Start with the Full List of Posts
            var query = AppData.Instance.Posts.Values.AsQueryable();

            // Filter by Search Term (Title or Description)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                         p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by Post Type
            if (Enum.TryParse<PostType>(postType, out var pType))
            {
                query = query.Where(p => p.Type == pType);
            }

            // Filter by Event Category
            if (Enum.TryParse<EventCategory>(category, out var eCat))
            {
                query = query.Where(p => p.Category.HasValue && p.Category.Value == eCat);
                AppData.Instance.LogSearchCategory(category); // Track this Search
            }

            // Filter by Event Date Range
            if (startDate.HasValue)
            {
                query = query.Where(p => p.Type == PostType.LocalEvent && p.StartTime.HasValue && p.StartTime.Value.Date >= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                query = query.Where(p => p.Type == PostType.LocalEvent && p.StartTime.HasValue && p.StartTime.Value.Date <= endDate.Value.Date);
            }

            // Apply Sorting
            query = sortOrder switch
            {
                "DatePublishedAsc" => query.OrderBy(p => p.DatePublished),
                "TitleAsc" => query.OrderBy(p => p.Title),
                "CategoryAsc" => query.OrderBy(p => p.Category),
                _ => query.OrderByDescending(p => p.DatePublished),
            };

            var filteredPosts = query.ToList();

            // Return the Partial View with the Filtered and Sorted List
            return PartialView("_PostList", filteredPosts);
        }
    }
}

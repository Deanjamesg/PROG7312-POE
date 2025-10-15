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

            return View(allPosts);
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable?view=net-8.0
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

using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Controllers
{
    public class CityPostController : Controller
    {

        public IActionResult CityPost()
        {
            var cityPosts = GetInitialPosts();
            return View(cityPosts);
        }

        [HttpGet]
        public IActionResult FilterPosts(
            string searchTerm = "",
            string postType = "",
            string eventCategory = "",
            DateTime? startDate = null,
            DateTime? endDate = null,
            string sortBy = "DatePublishedDesc")
        {
            var allPosts = CityPostData.CityPostsByDate.Values
                .SelectMany(list => list)
                .ToList();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                allPosts = allPosts.Where(p =>
                    p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(postType) && Enum.TryParse<CityPostType>(postType, out var typeFilter))
            {
                allPosts = allPosts.Where(p => p.CityPostType == typeFilter).ToList();
            }

            if (!string.IsNullOrWhiteSpace(eventCategory) && Enum.TryParse<EventCategory>(eventCategory, out var categoryFilter))
            {
                allPosts = allPosts.Where(p =>
                    p.CityPostType == CityPostType.LocalEvent &&
                    p.EventCategory.HasValue &&
                    p.EventCategory.Value == categoryFilter
                ).ToList();
            }

            if (startDate.HasValue)
            {
                allPosts = allPosts.Where(p =>
                {
                    var postDate = p.CityPostType == CityPostType.LocalEvent
                        ? p.StartDate ?? p.DatePublished
                        : p.DatePublished;
                    return postDate.Date >= startDate.Value.Date;
                }).ToList();
            }

            if (endDate.HasValue)
            {
                allPosts = allPosts.Where(p =>
                {
                    var postDate = p.CityPostType == CityPostType.LocalEvent
                        ? p.StartDate ?? p.DatePublished
                        : p.DatePublished;
                    return postDate.Date <= endDate.Value.Date;
                }).ToList();
            }

            allPosts = sortBy switch
            {
                "DatePublishedAsc" => allPosts.OrderBy(p => p.DatePublished).ToList(),
                "DatePublishedDesc" => allPosts.OrderByDescending(p => p.DatePublished).ToList(),
                "TitleAsc" => allPosts.OrderBy(p => p.Title).ToList(),
                "CategoryAsc" => allPosts.OrderBy(p =>
                    p.CityPostType == CityPostType.LocalEvent && p.EventCategory.HasValue
                        ? p.EventCategory.Value.GetDisplayName()
                        : "ZZZ"
                ).ToList(),
                _ => allPosts.OrderByDescending(p => p.DatePublished).ToList()
            };

            return PartialView("_CityPostList", allPosts);
        }

        public IActionResult GetPostDetails(Guid Id)
        {
            if (CityPostData.CityPostsById.TryGetValue(Id, out CityPost post))
            {
                CityPostData.AddToRecentlyViewed(post);

                return PartialView("_CityPostDetails", post);
            }

            return NotFound();
        }

        private List<CityPost> GetInitialPosts()
        {
            var cityPosts = CityPostData.CityPostsByDate.Values.ToList();
            List<CityPost> cityPostList = [];
            foreach (var listOfPostsOnDay in cityPosts)
            {
                foreach (var post in listOfPostsOnDay)
                {
                    cityPostList.Add(post);

                    if (cityPostList.Count >= 8)
                    {
                        break;
                    }
                }
                if (cityPostList.Count >= 8)
                {
                    break;
                }
            }
            return cityPostList;
        }

    }
}

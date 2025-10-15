using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Controllers
{
    public class PostController : Controller
    {
        public IActionResult ViewPosts()
        {
            List<Post> posts = [];

            foreach (var item in AppData.Instance.Posts)
            {
                var post = item.Value;
                posts.Add(post);
                Console.WriteLine(item.Value.Category.ToString());
                Console.WriteLine(item.ToString());
            }

            return View(posts);
        }

        public IActionResult CreatePost()
        {
            return View();
        }
    }
}

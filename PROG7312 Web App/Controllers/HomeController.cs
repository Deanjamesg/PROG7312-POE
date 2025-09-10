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

        public static ReportList reportList = new ReportList();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewReports()
        {
            List<ReportNode> reports = new List<ReportNode>();

            var currentNode = reportList.Head;

            while (currentNode != null)
            {
                reports.Add(currentNode);
                currentNode = currentNode.Next;
            }

            return View(reports);
        }

        public IActionResult Report()
        {
            return View(new ReportViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReport(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                ReportNode report = new ReportNode();
                if (model.Attachment != null && model.Attachment.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Attachment.CopyToAsync(memoryStream);
                        byte[] attachmentBytes = memoryStream.ToArray();

                        string base64String = Convert.ToBase64String(attachmentBytes);
                        report.Attachment = base64String;

                    }
                }
                report.FirstName = model.FirstName;
                report.LastName = model.LastName;
                report.Title = model.Title;
                report.Description = model.Description;
                report.Location = model.Location;
                report.Category = model.Category;
                report.Status = "On Going";
                reportList.Add(report);

                TempData["SuccessMessage"] = "Successfully submitted your report!";

                return RedirectToAction("Report");
            }
            return View("Report", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

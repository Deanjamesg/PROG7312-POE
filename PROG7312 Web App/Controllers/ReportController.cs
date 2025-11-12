using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.ViewModels;

namespace PROG7312_Web_App.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult ViewReports()
        {
            List<ReportNode> reports = new List<ReportNode>();

            var currentNode = AppData.Instance.reportList.Head;

            while (currentNode != null)
            {
                reports.Add(currentNode);
                currentNode = currentNode.Next;
            }
            ViewData["ActivePage"] = "ViewReports";

            return View(reports);
        }

        public IActionResult CreateReport()
        {
            ViewData["ActivePage"] = "RequestService";
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
                AppData.Instance.reportList.Add(report);

                TempData["SuccessMessage"] = "Successfully submitted your report!";

                return RedirectToAction("CreateReport");
            }
            return View("CreateReport", model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace PROG7312_Web_App.Controllers
{
    public class ServiceRequestController : Controller
    {

        public IActionResult ServiceRequest()
        {
            ViewData["ActivePage"] = "ServiceRequest";

            return View();
        }

        public IActionResult RequestServicePartial()
        {
            return PartialView("_RequestService");
        }
    }
}

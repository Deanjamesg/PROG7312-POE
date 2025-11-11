using Microsoft.AspNetCore.Mvc;

namespace PROG7312_Web_App.Controllers
{
    public class ServiceController : Controller
    {

        public IActionResult ServicePartial()
        {
            return PartialView("_Service");
        }

        public IActionResult RequestServicePartial()
        {
            return PartialView("_RequestService");
        }
    }
}

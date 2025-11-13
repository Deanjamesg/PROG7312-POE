using Microsoft.AspNetCore.Mvc;
using PROG7312_Web_App.Data;
using PROG7312_Web_App.Models;
using PROG7312_Web_App.ViewModels;

namespace PROG7312_Web_App.Controllers
{
    public class ServiceRequestController : Controller
    {
        public IActionResult AllServiceRequests()
        {
            List<ServiceRequest> allRequests = ServiceRequestData.AllRequests.GetAllSorted();

            return View(allRequests);
        }

        [HttpGet]
        public IActionResult NewServiceRequest()
        {
            return View(new ServiceRequestViewModel());
        }

        [HttpGet]
        public IActionResult ViewServiceRequestStatus(Guid? id)
        {
            ServiceRequest? request = null;
            if (id.HasValue)
            {
                request = ServiceRequestData.AllRequests.Find(id.Value);
            }
            return PartialView("_ServiceRequestDetails", request);
        }

        [HttpGet]
        public IActionResult SearchRequests(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                var allRequests = ServiceRequestData.AllRequests.GetAllSorted();
                return PartialView("_ServiceRequestTable", allRequests);
            }

            List<ServiceRequest> foundRequests = new List<ServiceRequest>();

            if (Guid.TryParse(id, out Guid searchGuid))
            {
                var foundRequest = ServiceRequestData.AllRequests.Find(searchGuid);

                if (foundRequest != null)
                {
                    foundRequests.Add(foundRequest);
                }
            }
            return PartialView("_ServiceRequestTable", foundRequests);
        }

        [HttpPost]
        public IActionResult CreateServiceRequest(ServiceRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceRequest newRequest = new ServiceRequest()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ContactNumber = model.ContactNumber,
                    Description = model.Description,
                    Location = model.Location,
                    Category = model.Category,
                    Status = ServiceRequestStatus.PendingApproval
                };



                ServiceRequestData.PendingSubmissions.Enqueue(newRequest);

                ServiceRequestData.AllRequests.Insert(newRequest);

                string referenceNumber = newRequest.Id.ToString();

                TempData["SuccessMessage"] = "Successfully submitted your report! Please use the reference number provided below to view the status of your service request.";
                TempData["ReferenceNumber"] = "Reference Number: " + referenceNumber;

                return RedirectToAction("NewServiceRequest", new ServiceRequestViewModel());
            }
            return View("NewServiceRequest", model);
        }

    }
}

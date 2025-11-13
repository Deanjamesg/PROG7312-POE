using System.ComponentModel.DataAnnotations;

namespace PROG7312_Web_App.ViewModels
{
    public class ServiceRequestViewModel
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your contact number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a location / address.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string Category { get; set; }

        public IFormFile? Attachment { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PROG7312_Web_App.ViewModels
{
    public class ReportViewModel
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a location.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string Category { get; set; }

        public IFormFile Attachment { get; set; }
    }
}

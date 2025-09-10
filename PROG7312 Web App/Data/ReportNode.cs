namespace PROG7312_Web_App.Data
{
    public class ReportNode
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public string? Attachment { get; set; }
        public string? Status { get; set; }

        public ReportNode Next { get; set; }
        public ReportNode Previous { get; set; }

        public ReportNode()
        {
            Next = null;
            Previous = null;
        }
    }
}

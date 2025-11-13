namespace PROG7312_Web_App.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public Guid Id { get; private set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string? Attachment { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public DateTime DateOfSubmission { get; set; }


        public ServiceRequest()
        {
            Id = Guid.NewGuid();
            DateOfSubmission = DateTime.Now;
            Status = ServiceRequestStatus.PendingApproval;
        }

        public int CompareTo(ServiceRequest? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
    }

    public enum ServiceRequestStatus
    {
        PendingApproval,
        Approved,
        InProgress,
        Complete,
        Declined
    }

    public enum ServiceRequestCategory
    {
        TransportTrafficAndRoads = 1, // Priority 1 (Highest)
        Electricity = 2, // Priority 2
        StormwaterAndFlooding = 3, // Priority 3
        SafetyAndSecurity = 4, // Priority 4
        WaterAndSanitation = 5 // Priority 5 (Lowest)
    }


}

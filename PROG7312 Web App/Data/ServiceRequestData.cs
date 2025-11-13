using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Data
{
    public static class ServiceRequestData
    {
        public static CustomBinarySearchTree AllRequests { get; private set; } = new CustomBinarySearchTree();
        public static Queue<ServiceRequest> PendingSubmissions { get; private set; } = new Queue<ServiceRequest>();

        public static CustomGraph<ServiceRequestStatus> StatusWorkflowGraph { get; private set; } = new CustomGraph<ServiceRequestStatus>();

        private static Random _random = new Random();

        private static List<string> _firstNames = new List<string> { "Sipho", "John", "Maria", "Lerato", "Chris", "Zanele", "Peter", "Thabo", "Emily", "David", "Nomsa", "Michael", "Sarah", "James", "Linda" };
        private static List<string> _lastNames = new List<string> { "Ndlovu", "Smith", "Gomes", "Mokoena", "Van der Merwe", "Dlamini", "Jones", "Molefe", "Williams", "Brown", "Khumalo", "Johnson", "Lee", "Davis", "Peters" };



        public static void SeedServiceRequestData()
        {
            if (!StatusWorkflowGraph.GetNeighbors(ServiceRequestStatus.PendingApproval).Any())
            {
                SeedWorkflowGraph();
            }

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0821112221",
                Description = "Major water pipe burst on corner of Main Rd and 1st Ave. Water is flooding the street.",
                Category = ServiceRequestCategory.WaterAndSanitation.ToString(),
                Location = "Corner of Main Rd and 1st Ave, Rondebosch",
                Status = ServiceRequestStatus.InProgress
            }, new DateTime(2025, 11, 13, 8, 15, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0732223332",
                Description = "Sewer drain is blocked and overflowing. Has been smelling for 2 days.",
                Category = ServiceRequestCategory.WaterAndSanitation.ToString(),
                Location = "123 Long Street, Cape Town CBD",
                Status = ServiceRequestStatus.Complete
            }, new DateTime(2025, 11, 11, 14, 30, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0713334443",
                Description = "No water in my taps. Whole street seems to be affected.",
                Category = ServiceRequestCategory.WaterAndSanitation.ToString(),
                Location = "45 Buitenkant St, Gardens",
                Status = ServiceRequestStatus.Approved
            }, new DateTime(2025, 11, 13, 9, 0, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0844445554",
                Description = "Power lines are down after the storm. Wires are sparking on the pavement.",
                Category = ServiceRequestCategory.Electricity.ToString(),
                Location = "Victoria Road, Camps Bay",
                Status = ServiceRequestStatus.InProgress
            }, new DateTime(2025, 11, 13, 6, 5, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0825556665",
                Description = "Street light has been out for over a week. It's very dark at night.",
                Category = ServiceRequestCategory.Electricity.ToString(),
                Location = "Opposite 22 Rose St, Bo-Kaap",
                Status = ServiceRequestStatus.PendingApproval
            }, new DateTime(2025, 11, 12, 11, 20, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0766667776",
                Description = "My prepaid meter is not accepting tokens. It keeps saying 'Error'.",
                Category = ServiceRequestCategory.Electricity.ToString(),
                Location = "Unit 15, Sunset Villas, Milnerton",
                Status = ServiceRequestStatus.Approved
            }, new DateTime(2025, 11, 12, 16, 45, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0737778887",
                Description = "Transformer box is making a loud buzzing noise and smoking.",
                Category = ServiceRequestCategory.Electricity.ToString(),
                Location = "Greenbelt Park, Constantia",
                Status = ServiceRequestStatus.Complete
            }, new DateTime(2025, 11, 10, 10, 0, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0798889998",
                Description = "Massive pothole in the middle of the lane. Very dangerous.",
                Category = ServiceRequestCategory.TransportTrafficAndRoads.ToString(),
                Location = "Klipfontein Road, near Athlone Stadium",
                Status = ServiceRequestStatus.PendingApproval
            }, new DateTime(2025, 11, 13, 11, 10, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0839990009",
                Description = "Traffic lights at intersection are out. Causing chaos.",
                Category = ServiceRequestCategory.TransportTrafficAndRoads.ToString(),
                Location = "Intersection of Buitengracht and Wale St",
                Status = ServiceRequestStatus.InProgress
            }, new DateTime(2025, 11, 13, 10, 30, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0721112221",
                Description = "Stop sign has been knocked over and is lying on the pavement.",
                Category = ServiceRequestCategory.TransportTrafficAndRoads.ToString(),
                Location = "20 Cavendish St, Claremont",
                Status = ServiceRequestStatus.Declined
            }, new DateTime(2025, 11, 9, 13, 0, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0812223332",
                Description = "Storm drain is completely blocked with leaves and rubbish. Road will flood if it rains.",
                Category = ServiceRequestCategory.StormwaterAndFlooding.ToString(),
                Location = "1 Main Road, Hout Bay",
                Status = ServiceRequestStatus.Approved
            }, new DateTime(2025, 11, 11, 9, 5, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0743334443",
                Description = "Manhole cover is missing. It's a huge open hole.",
                Category = ServiceRequestCategory.StormwaterAndFlooding.ToString(),
                Location = "Side street next to 44 Dorp St, Stellenbosch",
                Status = ServiceRequestStatus.Complete
            }, new DateTime(2025, 11, 8, 17, 10, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0824445554",
                Description = "Vagrants have started a fire under the bridge. It's getting large.",
                Category = ServiceRequestCategory.SafetyAndSecurity.ToString(),
                Location = "N2 Bridge, near Langa",
                Status = ServiceRequestStatus.InProgress
            }, new DateTime(2025, 11, 13, 11, 45, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0715556665",
                Description = "Street racing happening every night. Very loud and dangerous.",
                Category = ServiceRequestCategory.SafetyAndSecurity.ToString(),
                Location = "Summerley Road, Kenilworth",
                Status = ServiceRequestStatus.PendingApproval
            }, new DateTime(2025, 11, 12, 23, 30, 0));

            AddSeededRequest(new ServiceRequest
            {
                FirstName = _firstNames[_random.Next(_firstNames.Count)],
                LastName = _lastNames[_random.Next(_lastNames.Count)],
                ContactNumber = "0786667776",
                Description = "Illegal dumping of builder's rubble on the field.",
                Category = ServiceRequestCategory.SafetyAndSecurity.ToString(),
                Location = "Field behind Philippi Shopping Centre",
                Status = ServiceRequestStatus.Approved
            }, new DateTime(2025, 11, 10, 15, 20, 0));
        }

        private static void AddSeededRequest(ServiceRequest request, DateTime submissionDate)
        {
            request.DateOfSubmission = submissionDate;
            request.Status = request.Status;

            AllRequests.Insert(request);
        }

        private static void SeedWorkflowGraph()
        {
            foreach (ServiceRequestStatus status in Enum.GetValues(typeof(ServiceRequestStatus)))
            {
                StatusWorkflowGraph.AddNode(status);
            }

            StatusWorkflowGraph.AddEdge(ServiceRequestStatus.PendingApproval, ServiceRequestStatus.Approved);
            StatusWorkflowGraph.AddEdge(ServiceRequestStatus.PendingApproval, ServiceRequestStatus.Declined);
            StatusWorkflowGraph.AddEdge(ServiceRequestStatus.Approved, ServiceRequestStatus.InProgress);
            StatusWorkflowGraph.AddEdge(ServiceRequestStatus.Approved, ServiceRequestStatus.Declined);
            StatusWorkflowGraph.AddEdge(ServiceRequestStatus.InProgress, ServiceRequestStatus.Complete);
        }
    }
}
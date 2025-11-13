using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Data
{
    public sealed class AppDataOld
    {
        // Application State and Singleton Logic

        // https://learn.microsoft.com/en-us/dotnet/api/system.lazy-1?view=net-8.0
        private static readonly Lazy<AppDataOld> instance = new Lazy<AppDataOld>(() => new AppDataOld());
        public static AppDataOld Instance { get { return instance.Value; } }

        // ----------------------------------------------------------------------------------------------------------------

        // Application Data Stored during Run-Time

        // Primary Storage for Posts (Local Events and Announcements)
        public Dictionary<int, Post> Posts { get; private set; } = [];

        // Unique Categories Filter
        public HashSet<string> EventCategories { get; private set; } = [];

        // Recently Viewed Feature (LiFo - Last in First out)
        public Stack<Post> RecentlyViewed { get; private set; } = [];

        // User's Search and Recommendation Algorithm's Tracking
        public Dictionary<string, int> SearchAnalytics { get; private set; } = [];

        // Community Reports
        public ReportList reportList { get; set; } = new ReportList();

        // ----------------------------------------------------------------------------------------------------------------

        // This is to Ensure Thread Safety, and Produce Unique IDs (No-Duplicates)

        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock

        // Unique ID for each Post
        private int _nextPostId = 1;

        // Lock to to ensure Thread Safety
        private readonly object _lock = new object();

        // ----------------------------------------------------------------------------------------------------------------

        // Constructor
        private AppDataOld()
        {
            CreateSamplePostData();
            CreateSampleReportData();
        }

        // Get the Next Unique ID
        public int GetNextId()
        {
            lock (_lock)
            {
                return _nextPostId++;
            }
        }

        public void LogSearchCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) return;

            lock (_lock)
            {
                if (SearchAnalytics.ContainsKey(category))
                {
                    SearchAnalytics[category]++;
                }
                else
                {
                    SearchAnalytics.Add(category, 1);
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------------

        private void CreateSampleReportData()
        {
            ReportNode node = new ReportNode();

            node.FirstName = "John";
            node.LastName = "Doe";
            node.Title = "Fallen Tree on Main road";
            node.Description = "Four trees were uprooted within 200m on Frans Conradie Drive in Bellville.\r\n\r\n";
            node.Category = "Storms and Flooding";
            node.Location = "Frans Conradie Drive, Bellville, Cape Town";
            node.Attachment = "https://cdn.24.co.za/files/Cms/General/d/8480/0dc0670884c1440e84a792379a957796.jpg";
            node.Status = "In Progress";

            reportList.Add(node);

            node = new ReportNode();

            node.FirstName = "James";
            node.LastName = "Carl";
            node.Title = "High School Destroyed by Strong Winds";
            node.Description = "School structures at Nomzamo High School in Strand were severely damaged during a heavy Cape storm.\r\n\r\n";
            node.Category = "Storms and Flooding";
            node.Location = "Nomzamo High School, Strand, Cape Town";
            node.Attachment = "https://cdn.24.co.za/files/Cms/General/d/8477/99054e139d1642f6937f05c3cce6233a.jpg";
            node.Status = "Complete";

            reportList.Add(node);

            node = new ReportNode();

            node.FirstName = "Daniel";
            node.LastName = "McCarthy";
            node.Title = "N2 closed in Cape Town due to ongoing protest action";
            node.Description = "Traffic congestion in Cape Town has intensified following ongoing protest actions that have led to significant disruptions on the N2 highway.";
            node.Category = "Storms and Flooding";
            node.Location = "N2 Highway, Cape Town";
            node.Attachment = "https://www.capetownetc.com/wp-content/uploads/2025/05/image-1280x720-2025-05-22T160916.113-1024x576.png";
            node.Status = "On Going";

            reportList.Add(node);
        }


        // https://www.capetown.gov.za/Local%20and%20communities/Events-and-your-City/Find-an-event-in-the-City/Signature-Cape-Town-events#Heading1

        // Method to Initialize and Create Sample Data
        private void CreateSamplePostData()
        {
            // ANNOUNCEMENTS (5)

            Posts.Add(GetNextId(), new Post
            {
                Id = 1,
                Type = PostType.Announcement,
                Title = "Public Notice: Load Shedding Schedule Update",
                Description = "Please be advised of the updated load shedding schedule for the upcoming week. Check the City's official channels for your area's schedule.",
                DatePublished = new DateTime(2025, 10, 14)
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 2,
                Type = PostType.Announcement,
                Title = "MyCiTi Fare Increase Effective 1 November 2025",
                Description = "Commuters are advised that MyCiTi bus fares will undergo a small annual increase starting from the 1st of November 2025.",
                DatePublished = new DateTime(2025, 10, 10)
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 3,
                Type = PostType.Announcement,
                Title = "Call for Public Comment on New Waste Management By-Law",
                Description = "The City invites residents to submit comments on the proposed Waste Management By-Law. The deadline for submissions is 30 November 2025.",
                DatePublished = new DateTime(2025, 10, 5)
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 4,
                Type = PostType.Announcement,
                Title = "Holiday Season Refuse Collection Schedule",
                Description = "Refuse collection will continue as normal on public holidays, except for Christmas Day. Please put your bins out as per your regular schedule.",
                DatePublished = new DateTime(2025, 9, 28)
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 5,
                Type = PostType.Announcement,
                Title = "Temporary Road Closure: Green Point for Cycle Tour Prep",
                Description = "Motorists are advised of temporary road closures around the DHL Stadium from 5-9 March 2026 for Cape Town Cycle Tour preparations.",
                DatePublished = new DateTime(2025, 10, 15)
            });

            // LOCAL EVENTS (10) 

            Posts.Add(GetNextId(), new Post
            {
                Id = 6,
                Type = PostType.LocalEvent,
                Title = "Kirstenbosch Summer Sunset Concerts",
                Description = "Enjoy live music from top South African artists on the beautiful lawns of Kirstenbosch National Botanical Garden. ",
                DatePublished = new DateTime(2025, 10, 12),
                StartTime = new DateTime(2025, 12, 7, 17, 30, 0),
                EndTime = new DateTime(2025, 12, 7, 19, 0, 0),
                Location = "Kirstenbosch Gardens",
                Category = EventCategory.CultureAndCreativity
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 7,
                Type = PostType.LocalEvent,
                Title = "Cape Town Cycle Tour 2026",
                Description = "Join thousands of cyclists for the world's largest timed cycle race through the beautiful Cape Peninsula. ",
                DatePublished = new DateTime(2025, 9, 15),
                StartTime = new DateTime(2026, 3, 8, 6, 0, 0),
                EndTime = new DateTime(2026, 3, 8, 17, 0, 0),
                Location = "Grand Parade, Cape Town",
                Category = EventCategory.Sports
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 8,
                Type = PostType.LocalEvent,
                Title = "First Thursdays Cape Town",
                Description = "Explore art galleries and cultural attractions in the city centre, which stay open late on the first Thursday of every month.",
                DatePublished = new DateTime(2025, 10, 15),
                StartTime = new DateTime(2025, 11, 6, 17, 0, 0),
                EndTime = new DateTime(2025, 11, 6, 21, 0, 0),
                Location = "Cape Town CBD",
                Category = EventCategory.CultureAndCreativity
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 9,
                Type = PostType.LocalEvent,
                Title = "Table Mountain National Park Clean-up",
                Description = "Volunteer with SANParks to help maintain the beauty of our iconic mountain. Bags and gloves will be provided.",
                DatePublished = new DateTime(2025, 10, 11),
                StartTime = new DateTime(2025, 11, 22, 9, 0, 0),
                EndTime = new DateTime(2025, 11, 22, 12, 0, 0),
                Location = "Platteklip Gorge Trailhead",
                Category = EventCategory.Environment
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 10,
                Type = PostType.LocalEvent,
                Title = "Small Business Support Workshop",
                Description = "The City's Business Hub invites entrepreneurs to a free workshop on digital marketing and financial planning.",
                DatePublished = new DateTime(2025, 10, 9),
                StartTime = new DateTime(2025, 11, 19, 10, 0, 0),
                EndTime = new DateTime(2025, 11, 19, 15, 0, 0),
                Location = "Cape Town Civic Centre",
                Category = EventCategory.BusinessAndInnovation
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 11,
                Type = PostType.LocalEvent,
                Title = "Oranjezicht City Farm Market Day",
                Description = "A vibrant farmers' market offering fresh produce, artisanal foods, and local crafts. ",
                DatePublished = new DateTime(2025, 10, 15),
                StartTime = new DateTime(2025, 10, 18, 9, 0, 0),
                EndTime = new DateTime(2025, 10, 18, 14, 0, 0),
                Location = "V&A Waterfront",
                Category = EventCategory.Community
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 12,
                Type = PostType.LocalEvent,
                Title = "Cape Town Marathon",
                Description = "Africa's only Abbott World Marathon Majors candidate race. Come run or support the thousands of athletes.",
                DatePublished = new DateTime(2025, 8, 20),
                StartTime = new DateTime(2026, 4, 18, 6, 30, 0),
                EndTime = new DateTime(2026, 4, 18, 13, 0, 0),
                Location = "Green Point, Cape Town",
                Category = EventCategory.Sports
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 13,
                Type = PostType.LocalEvent,
                Title = "Khayelitsha Canoe Club Community Day",
                Description = "Join the Khayelitsha Canoe Club for a day of fun on the water, with introductory lessons for kids and a braai for all.",
                DatePublished = new DateTime(2025, 10, 1),
                StartTime = new DateTime(2025, 11, 29, 11, 0, 0),
                EndTime = new DateTime(2025, 11, 29, 16, 0, 0),
                Location = "Khayelitsha Wetlands Park",
                Category = EventCategory.Community
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 14,
                Type = PostType.LocalEvent,
                Title = "Cape Town International Animation Festival",
                Description = "A showcase of African and international animation talent, featuring screenings, workshops, and networking events.",
                DatePublished = new DateTime(2025, 9, 5),
                StartTime = new DateTime(2026, 4, 24, 10, 0, 0),
                EndTime = new DateTime(2026, 4, 26, 18, 0, 0),
                Location = "The Labia Theatre",
                Category = EventCategory.CultureAndCreativity
            });

            Posts.Add(GetNextId(), new Post
            {
                Id = 15,
                Type = PostType.LocalEvent,
                Title = "Future of Tech Summit",
                Description = "A premier conference for tech startups, investors, and innovators to connect and explore emerging technologies.",
                DatePublished = new DateTime(2025, 10, 13),
                StartTime = new DateTime(2026, 2, 20, 9, 0, 0),
                EndTime = new DateTime(2026, 2, 21, 17, 0, 0),
                Location = "Cape Town International Convention Centre (CTICC)",
                Category = EventCategory.BusinessAndInnovation
            });

        }
    }

}
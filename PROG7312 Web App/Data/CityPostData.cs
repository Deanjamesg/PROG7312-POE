using PROG7312_Web_App.Models;

namespace PROG7312_Web_App.Data
{
    public static class CityPostData
    {
        // Primary storage for all the City Posts, sorted by Date
        // Key -> Published Date for Announcements
        // Key -> Start Date for Events
        // Value -> List of CityPost objects for that date
        public static SortedDictionary<DateTime, List<CityPost>> CityPostsByDate = [];

        // Fast lookup by ID for retrieving full CityPost objects
        public static Dictionary<Guid, CityPost> CityPostsById = [];

        // Category index for fast filtering by event category
        // Key -> Category name 
        // Value -> Set of CityPost IDs (Events only)
        public static Dictionary<string, HashSet<Guid>> CityPostEventCategory { get; private set; } = [];

        // Title index for fast searching by title
        // Key -> Lowercase title words 
        // Value -> Set of CityPost IDs
        public static Dictionary<string, HashSet<Guid>> CityPostTitleIndex { get; private set; } = [];

        // Queue of recent news posts (limited to 4 items for home page display)
        public static Queue<CityPost> RecentNews { get; private set; } = [];

        // Stack of recently viewed posts (for "Recently Viewed" feature)
        public static Stack<CityPost> RecentlyViewed { get; private set; } = [];

        // User's Search and Recommendation Algorithm's Tracking
        public static Dictionary<string, int> SearchAnalytics { get; private set; } = [];


        public static void AddCityPost(CityPost post)
        {
            DateTime key = AddCityPostHelper(post);

            if (!CityPostsByDate.ContainsKey(key))
            {
                CityPostsByDate[key] = new List<CityPost>();
            }
            CityPostsByDate[key].Add(post);
            CityPostsById[post.Id] = post;
        }


        private static DateTime AddCityPostHelper(CityPost post)
        {
            switch (post.CityPostType)
            {
                case CityPostType.LocalEvent:
                    return post.StartDate!.Value.Date;

                case CityPostType.Announcement:
                    AddToRecentNews(post);
                    return post.DatePublished.Date;
            }
            return post.DatePublished.Date;
        }


        private static void AddToRecentNews(CityPost post)
        {
            RecentNews.Enqueue(post);
            if (RecentNews.Count > 4)
            {
                RecentNews.Dequeue();
            }
        }


        public static void AddToRecentlyViewed(CityPost post)
        {
            if (post == null) return;

            // Remove if already in stack to avoid duplicates
            List<CityPost> tempList = [];

            while (RecentlyViewed.Count > 0)
            {
                var item = RecentlyViewed.Pop();
                if (item.Id != post.Id)
                {
                    tempList.Add(item);
                }
            }

            // Push back in reverse order
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                RecentlyViewed.Push(tempList[i]);
            }

            // If it is an Event, get its category and increment counter for that category
            if (post.CityPostType == CityPostType.LocalEvent && post != null)
            {
                string category = post.EventCategory.Value.GetDisplayName();

                if (SearchAnalytics.ContainsKey(category))
                {
                    SearchAnalytics[category]++;
                }
                else
                {
                    SearchAnalytics.Add(category, 1);
                }
            }

            // Add the new viewed item
            RecentlyViewed.Push(post);
        }


        public static void SeedCityPostData()
        {
            // ANNOUNCEMENTS (5)

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.Announcement,
                Title = "Public Notice: Load Shedding Schedule Update",
                Description = "Please be advised of the updated load shedding schedule for the upcoming week. Check the City's official channels for your area's schedule.",
                DatePublished = new DateTime(2025, 10, 14)
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.Announcement,
                Title = "MyCiTi Fare Increase Effective 1 November 2025",
                Description = "Commuters are advised that MyCiTi bus fares will undergo a small annual increase starting from the 1st of November 2025.",
                DatePublished = new DateTime(2025, 10, 10)
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.Announcement,
                Title = "Call for Public Comment on New Waste Management By-Law",
                Description = "The City invites residents to submit comments on the proposed Waste Management By-Law. The deadline for submissions is 30 November 2025.",
                DatePublished = new DateTime(2025, 10, 5)
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.Announcement,
                Title = "Holiday Season Refuse Collection Schedule",
                Description = "Refuse collection will continue as normal on public holidays, except for Christmas Day. Please put your bins out as per your regular schedule.",
                DatePublished = new DateTime(2025, 9, 28)
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.Announcement,
                Title = "Temporary Road Closure: Green Point for Cycle Tour Prep",
                Description = "Motorists are advised of temporary road closures around the DHL Stadium from 5-9 March 2026 for Cape Town Cycle Tour preparations.",
                DatePublished = new DateTime(2025, 10, 15)
            });

            // LOCAL EVENTS (10) 

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Kirstenbosch Summer Sunset Concerts",
                Description = "Enjoy live music from top South African artists on the beautiful lawns of Kirstenbosch National Botanical Garden. ",
                DatePublished = new DateTime(2025, 10, 12),
                StartDate = new DateTime(2025, 12, 7, 17, 30, 0),
                EndDate = new DateTime(2025, 12, 7, 19, 0, 0),
                Location = "Kirstenbosch Gardens",
                EventCategory = EventCategory.CultureAndCreativity
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Cape Town Cycle Tour 2026",
                Description = "Join thousands of cyclists for the world's largest timed cycle race through the beautiful Cape Peninsula. ",
                DatePublished = new DateTime(2025, 9, 15),
                StartDate = new DateTime(2026, 3, 8, 6, 0, 0),
                EndDate = new DateTime(2026, 3, 8, 17, 0, 0),
                Location = "Grand Parade, Cape Town",
                EventCategory = EventCategory.Sports
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "First Thursdays Cape Town",
                Description = "Explore art galleries and cultural attractions in the city centre, which stay open late on the first Thursday of every month.",
                DatePublished = new DateTime(2025, 10, 15),
                StartDate = new DateTime(2025, 11, 6, 17, 0, 0),
                EndDate = new DateTime(2025, 11, 6, 21, 0, 0),
                Location = "Cape Town CBD",
                EventCategory = EventCategory.CultureAndCreativity
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Table Mountain National Park Clean-up",
                Description = "Volunteer with SANParks to help maintain the beauty of our iconic mountain. Bags and gloves will be provided.",
                DatePublished = new DateTime(2025, 10, 11),
                StartDate = new DateTime(2025, 11, 22, 9, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 12, 0, 0),
                Location = "Platteklip Gorge Trailhead",
                EventCategory = EventCategory.Environment
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Small Business Support Workshop",
                Description = "The City's Business Hub invites entrepreneurs to a free workshop on digital marketing and financial planning.",
                DatePublished = new DateTime(2025, 10, 9),
                StartDate = new DateTime(2025, 11, 19, 10, 0, 0),
                EndDate = new DateTime(2025, 11, 19, 15, 0, 0),
                Location = "Cape Town Civic Centre",
                EventCategory = EventCategory.BusinessAndInnovation
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Oranjezicht City Farm Market Day",
                Description = "A vibrant farmers' market offering fresh produce, artisanal foods, and local crafts. ",
                DatePublished = new DateTime(2025, 10, 15),
                StartDate = new DateTime(2025, 10, 18, 9, 0, 0),
                EndDate = new DateTime(2025, 10, 18, 14, 0, 0),
                Location = "V&A Waterfront",
                EventCategory = EventCategory.Community
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Cape Town Marathon",
                Description = "Africa's only Abbott World Marathon Majors candidate race. Come run or support the thousands of athletes.",
                DatePublished = new DateTime(2025, 8, 20),
                StartDate = new DateTime(2026, 4, 18, 6, 30, 0),
                EndDate = new DateTime(2026, 4, 18, 13, 0, 0),
                Location = "Green Point, Cape Town",
                EventCategory = EventCategory.Sports
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Khayelitsha Canoe Club Community Day",
                Description = "Join the Khayelitsha Canoe Club for a day of fun on the water, with introductory lessons for kids and a braai for all.",
                DatePublished = new DateTime(2025, 10, 1),
                StartDate = new DateTime(2025, 11, 29, 11, 0, 0),
                EndDate = new DateTime(2025, 11, 29, 16, 0, 0),
                Location = "Khayelitsha Wetlands Park",
                EventCategory = EventCategory.Community
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Cape Town International Animation Festival",
                Description = "A showcase of African and international animation talent, featuring screenings, workshops, and networking events.",
                DatePublished = new DateTime(2025, 9, 5),
                StartDate = new DateTime(2026, 4, 24, 10, 0, 0),
                EndDate = new DateTime(2026, 4, 26, 18, 0, 0),
                Location = "The Labia Theatre",
                EventCategory = EventCategory.CultureAndCreativity
            });

            AddCityPost(new CityPost
            {
                CityPostType = CityPostType.LocalEvent,
                Title = "Future of Tech Summit",
                Description = "A premier conference for tech startups, investors, and innovators to connect and explore emerging technologies.",
                DatePublished = new DateTime(2025, 10, 13),
                StartDate = new DateTime(2026, 2, 20, 9, 0, 0),
                EndDate = new DateTime(2026, 2, 21, 17, 0, 0),
                Location = "Cape Town International Convention Centre (CTICC)",
                EventCategory = EventCategory.BusinessAndInnovation
            });

        }

    }
}

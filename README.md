# 🏙️ Cape Town Municipal Services Web Application

### PROG7312 - POE
### ST10378305
### Dean James Greeff

### [YouTube Link](https://youtu.be/8rbXeQMoR4M)

## 📋 Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Data Structures](#data-structures)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Usage Guide](#usage-guide)
- [Project Structure](#project-structure)

## ✨ Features

### 🎫 City Posts & Events
- Browse local events and city announcements
- Filter by category, date range, and post type
- Search functionality with live filtering
- Personalized event recommendations based on viewing history
- Recently viewed posts tracking

### 🛠️ Service Request Management
- Submit service requests for municipal issues (electricity, water, roads, etc.)
- Track request status with unique reference numbers
- Search requests by ID
- View all service requests with detailed information
- Status workflow validation using graph structure

### 🏠 Home Dashboard
- Recent news queue (displays 4 most recent announcements)
- Recently viewed posts (up to 8 items)
- Personalized event recommendations
- Analytics-driven suggestions based on user preferences

## 🚀 Technology Stack

- **Framework:** ASP.NET Core MVC (.NET 8+)
- **Language:** C#
- **Frontend:** Bootstrap, CSS, RazorViews, HTMX, JavaScript
- **Architecture:** MVC Pattern
- **Data Storage:** In-memory with custom data structures

## 🧠 Data Structures

This application showcases the practical implementation of advanced data structures:

| Data Structure | Purpose | Key Operations |
|---------------|---------|----------------|
| **Custom Binary Search Tree** | Service request storage | O(log n) search, sorted retrieval |
| **Directed Graph** | Status workflow validation | State transition management |
| **SortedDictionary** | Chronologically ordered posts | O(log n) insert, range queries |
| **Dictionary + HashSet** | Category & title indexing | O(1) filtering and search |
| **Queue** | Recent news display | FIFO, fixed size (4 items) |
| **Stack** | Recently viewed tracking | LIFO, duplicate prevention |
| **Dictionary** | Analytics tracking | O(1) counter increments |

For detailed implementation analysis, see the source files in `/Models/` and `/Data/`.

## 📥 Installation

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or higher
- A code editor (Visual Studio 2022, Visual Studio Code, or Rider)
- Git (for cloning the repository)

### Step 1: Clone the Repository

```bash
git clone https://github.com/Deanjamesg/PROG7312-POE.git
cd PROG7312-POE
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

### Step 3: Build the Project

```bash
dotnet build
```

## ▶️ Running the Application

### Using the .NET CLI

```bash
dotnet run
```

The application will start and be available at:
- HTTPS: `https://localhost:7234`

### Using Visual Studio

1. Open the solution file (`.sln`) in Visual Studio
2. Press `F5` or click the "Run" button
3. The application will launch in your default browser

### Using Visual Studio Code

1. Open the project folder in VS Code
2. Press `F5` or use the "Run and Debug" panel
3. Select ".NET Core Launch (web)" configuration

## 📖 Usage Guide

### Viewing City Posts and Events

1. Navigate to **City Posts & Events** from the main menu
2. Browse through announcements and local events
3. Use the filter panel to:
   - Search by keywords
   - Filter by post type (Event/Announcement)
   - Filter by event category (Sports, Culture, Environment, etc.)
   - Set date ranges
   - Sort results

### Submitting a Service Request

1. Go to **Report an Issue**
2. Fill in the required information:
   - First Name and Last Name
   - Contact Number
   - Description of the issue
   - Category (select from dropdown)
   - Location
3. Click **Submit**
4. Save your reference number for tracking

### Tracking Service Requests

1. Navigate to **Track Service Request**
2. Enter your reference number (GUID) in the search box
3. View the status of your request:
   - Pending Approval
   - Approved
   - In Progress
   - Complete
   - Declined

### Viewing All Service Requests

1. Go to **Track Service Request**
2. Click **View All Service Requests**
3. Browse the complete list of requests
4. Click on any request to view full details

### Home Dashboard Features

- **Recent News:** View the 4 most recent city announcements
- **Recently Viewed:** See your last 8 viewed posts
- **Recommendations:** Get personalized event suggestions based on your viewing history

## 📁 Project Structure

```
PROG7312-Web-App/
├── Controllers/
│   ├── HomeController.cs           # Home page logic
│   ├── CityPostController.cs       # City posts and events
│   └── ServiceRequestController.cs # Service request handling
├── Models/
│   ├── CityPost.cs                 # City post model
│   ├── ServiceRequest.cs           # Service request model
│   ├── CustomBinarySearchTree.cs   # BST implementation
│   └── CustomGraph.cs              # Graph implementation
├── Data/
│   ├── CityPostData.cs             # City post data layer
│   └── ServiceRequestData.cs       # Service request data layer
├── Views/
│   ├── Home/
│   ├── CityPost/
│   ├── ServiceRequest/
│   └── Shared/
│       └── _Layout.cshtml          # Main layout template
└── wwwroot/                        # Static files (CSS, JS, images)
```

## 🎯 Key Features Explained

### Personalized Recommendations

The application tracks which event categories you view most frequently and suggests similar upcoming events. The recommendation algorithm:

1. Monitors event views via the `RecentlyViewed` stack
2. Updates category counters in `SearchAnalytics` dictionary
3. Identifies your favorite category
4. Filters upcoming events by that category
5. Falls back to generic "Upcoming Events" if no preference is detected

### Status Workflow Validation

Service requests follow a strict state machine enforced by a directed graph:

```
PendingApproval → Approved → InProgress → Complete
       ↓             ↓
   Declined     Declined
```

Invalid transitions (e.g., Complete → PendingApproval) are prevented by the graph structure.

### Efficient Searching and Filtering

Multiple indexing strategies enable fast queries:
- **By Date:** SortedDictionary provides O(log n) date range queries
- **By ID:** Dictionary provides O(1) direct lookup
- **By Category:** HashSet index provides O(1) category filtering
- **By Keywords:** Inverted index enables multi-term search


## 👨‍💻 Author

**Dean James Greeff**
- GitHub: [@deanjamesg](https://github.com/deanjamesg)

## 🙏 Acknowledgments

- Cape Town municipality for inspiration
- Seed data based on actual Cape Town events and services
- ASP.NET Core documentation and community

---

**Note:** This application uses in-memory data storage. All data will be reset when the application restarts. For production use, integrate a persistent database system like SQL Server or PostgreSQL.

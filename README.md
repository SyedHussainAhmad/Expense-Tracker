# 💰 Expense Tracker

A comprehensive web-based expense tracking application built with ASP.NET Core MVC, featuring user authentication, role-based authorization, and interactive data visualization.

## 🚀 Features

### 🔐 Authentication & Authorization
- **User Registration & Login** - Secure authentication system using ASP.NET Core Identity
- **Role-Based Access Control** - Admin and User roles with different permission levels
- **User-Specific Data** - Each user can only access their own expenses and categories
- **Password Security** - Encrypted password storage and validation

### 📊 Dashboard & Analytics
- **Interactive Home Dashboard** - Visual representation of expense data
- **Dynamic Charts & Graphs** - Real-time expense visualization using charts
- **Expense Analytics** - Track spending patterns and trends
- **Category-wise Breakdown** - Analyze expenses by different categories

### 💸 Expense Management
- **Add Expenses** - Quick and easy expense entry with AJAX functionality
- **Update Expenses** - Edit existing expense records seamlessly
- **Delete Expenses** - Remove unwanted expense entries
- **Search & Filter** - Advanced search functionality using AJAX for instant results
- **Real-time Updates** - All operations performed without page refresh

### 🏷️ Category Management
- **Custom Categories** - Create and manage personalized expense categories
- **Category CRUD Operations** - Add, update, delete categories with AJAX
- **Category Search** - Quick category lookup and filtering
- **User-Specific Categories** - Each user maintains their own category list

### 📱 User Interface
- **Modern Gradient Design** - Beautiful UI with gradient backgrounds and modern styling
- **Responsive Layout** - Mobile-friendly design that works on all devices
- **Intuitive Navigation** - Easy-to-use interface with smooth user experience
- **AJAX-Powered** - Fast, responsive interactions without page reloads

### 📄 Reporting & Export
- **Downloadable Reports** - Generate and download expense reports
- **Multiple Export Formats** - Export data in various formats (PDF, Excel, etc.)
- **Custom Date Ranges** - Generate reports for specific time periods
- **Detailed Analytics** - Comprehensive expense breakdowns

### 📞 Communication
- **Contact Page** - Professional contact form for user inquiries
- **Email Integration** - Automatic email responses after form submission
- **About Page** - Information about the application and its features

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core MVC** - Main framework
- **ASP.NET Core Identity** - Authentication and authorization
- **Entity Framework Core** - Object-relational mapping
- **Microsoft SQL Server** - Database management system

### Frontend
- **HTML5 & CSS3** - Modern web standards
- **Bootstrap** - Responsive CSS framework
- **JavaScript & jQuery** - Client-side interactivity
- **AJAX** - Asynchronous data operations
- **Chart.js/D3.js** - Data visualization libraries

### Database
- **Microsoft SQL Server** - Relational database
- **Code-First Approach** - Database schema management through migrations

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional)

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/SyedHussainAhmad/Expense-Tracker.git
cd Expense-Tracker
```

### 2. Configure Database Connection
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ExpenseTrackerDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Apply Database Migrations
```bash
dotnet ef database update
```

### 5. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## 📊 Database Schema

The application uses the following main entities:

### Users (Identity)
- User management through ASP.NET Core Identity
- Role-based authorization (Admin, User)

### Expenses
- Id (Primary Key)
- Amount
- Description
- Date
- CategoryId (Foreign Key)
- UserId (Foreign Key)

### Categories
- Id (Primary Key)
- Name
- Description
- UserId (Foreign Key)

## 🎯 Usage

### For Regular Users:
1. **Register/Login** - Create an account or sign in
2. **Dashboard** - View your expense overview and charts
3. **Add Expenses** - Record new expenses with categories
4. **Manage Categories** - Create custom expense categories
5. **Search & Filter** - Find specific expenses quickly
6. **Generate Reports** - Download expense reports
7. **Contact Support** - Use the contact form for inquiries

### For Administrators:
- Access to all user data and system management
- User management capabilities
- System-wide reporting and analytics

## 🔧 Configuration

### Email Settings
Configure SMTP settings in `appsettings.json` for contact form functionality:
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com"
  }
}
```

### Identity Configuration
Identity settings are configured in `Program.cs` with:
- Password requirements
- Lockout settings
- Cookie configuration

## 🚀 Deployment

### IIS Deployment
1. Publish the application: `dotnet publish -c Release`
2. Copy published files to IIS directory
3. Configure IIS application pool for .NET Core
4. Update connection string for production database

### Azure Deployment
1. Create an Azure App Service
2. Configure SQL Database connection
3. Deploy using Visual Studio or Azure DevOps
4. Configure environment variables

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📞 Support

If you encounter any issues or have questions, please:
1. Check the [Issues](https://github.com/SyedHussainAhmad/Expense-Tracker/issues) page
2. Use the contact form in the application
3. Create a new issue with detailed information

---

## 👤 Author

**Syed Hussain Ahmad**
- GitHub: [@SyedHussainAhmad](https://github.com/SyedHussainAhmad)
- LinkedIn: [Syed Hussain Ahmad](https://www.linkedin.com/in/syedhussainahmad/)

---
⭐ **Star this repository if you find it helpful!**

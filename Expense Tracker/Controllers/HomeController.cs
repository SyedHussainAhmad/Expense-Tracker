using Expense_Tracker.Models;
using ExpenseTracker.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Expense_Tracker.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.Identity.Name;

            // Fetch expenses by category
            var categoryData = _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.Category.Title)
                .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();

            // Fetch monthly trends
            var monthlyData = _context.Expenses
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.Date.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();

            // Fetch total expenses
            var totalExpenses = _context.Expenses
                .Where(e => e.UserId == userId)
                .Sum(e => e.Amount);

            // Fetch this month's expenses
            var thisMonthExpenses = _context.Expenses
                .Where(e => e.UserId == userId && e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                .Sum(e => e.Amount);

            // Fetch the largest expense
            var largestExpense = _context.Expenses
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Amount)
                .FirstOrDefault()?.Amount ?? 0;

            // Serialize data to JSON for the view
            ViewBag.CategoryLabels = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData.Select(c => c.Category));
            ViewBag.CategoryValues = Newtonsoft.Json.JsonConvert.SerializeObject(categoryData.Select(c => c.Total));
            ViewBag.MonthlyLabels = Newtonsoft.Json.JsonConvert.SerializeObject(monthlyData.Select(m => new DateTime(1, m.Month, 1).ToString("MMM")));
            ViewBag.MonthlyValues = Newtonsoft.Json.JsonConvert.SerializeObject(monthlyData.Select(m => m.Total));

            // Pass additional data to the view
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.ThisMonthExpenses = thisMonthExpenses;
            ViewBag.LargestExpense = largestExpense;

            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                SendEmailNotification(contact.Name, contact.Email, contact.Message);
                TempData["Success"] = "Thank you for your feedback!";
                return RedirectToAction("Contact");
            }
            else
            {
                TempData["Failed"] = "Feedback not sent! Please try again...";

            }
            return View("Contact");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void SendEmailNotification(string name, string email, string message)
        {
            string toEmail = "hsyed9877@gmail.com";
            string subject = "New Contact Form Submission of Expense Tracker App";
            string body = $@"
                <h2>New Contact Form Submission</h2>
                <p><strong>Name:</strong> {name}</p>
                <p><strong>Email:</strong> {email}</p>
                <p><strong>Message:</strong> {message}</p>";

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("hsyed9877@gmail.com", "znrq bvwr lnnb xkca");
                smtp.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("hsyed9877@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                smtp.Send(mailMessage);
            }
        }
    }
}
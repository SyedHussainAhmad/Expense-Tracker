using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Data;
using jsreport.Local;
using jsreport.Types;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Expense_Tracker.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public ReportController(ApplicationDbContext context, IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider)
        {
            _context = context;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
        }

        // Action to generate and display the report in view
        public IActionResult GenerateReport()
        {
            var userId = User.Identity.Name;

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

            // Pass data to the view
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.ThisMonthExpenses = thisMonthExpenses;
            ViewBag.LargestExpense = largestExpense;

            return View();
        }
    }
}

using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        private object GetSearchResults(string keyword, string searchType)
        {
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(searchType))
                return null;

            if (searchType == "Category")
            {
                return _context.Categories
                    .Where(c => c.Title.Contains(keyword))
                    .Select(c => new
                    {
                        c.CategoryId,
                        c.Title,
                        c.Type
                    })
                    .ToList();
            }
            else if (searchType == "Expense")
            {
                return _context.Expenses
                    .Include(e => e.Category)
                    .Where(e => e.Note.Contains(keyword))
                    .Select(e => new
                    {
                        e.ExpenseId,
                        e.Note,
                        e.Amount,
                        e.Date,
                        Category = e.Category.Title // Include the Category's Title
                    })
                    .ToList();
            }

            return null;
        }

        [HttpPost]
        public IActionResult Search(string keyword, string searchType)
        {
            var results = GetSearchResults(keyword, searchType);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest") // Check if it's an AJAX request
            {
                if (searchType == "Expense")
                {
                    return PartialView("_SearchResults_Expense", results);
                }
                else if (searchType == "Category")
                {
                    return PartialView("_SearchResults_Category", results);
                }
            }

            ViewBag.Type = searchType;
            return View(results);
        }


    }
}

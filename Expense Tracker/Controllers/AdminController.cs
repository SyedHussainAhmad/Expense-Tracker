using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await _context.Users.ToListAsync();

            var totalUsers = users.Count;
            var totalExpenses = _context.Expenses.Sum(e => e.Amount);
            var totalCategories = _context.Categories.Count();

            var expenseStats = _context.Expenses
                .GroupBy(e => e.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    TotalExpenses = g.Sum(e => e.Amount)
                }).ToList();

            ViewBag.Users = users;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.TotalCategories = totalCategories;

            ViewBag.ExpenseStats = Newtonsoft.Json.JsonConvert.SerializeObject(expenseStats);

            return View();
        }

        public IActionResult UserDetails(string userId)
        {

            if (userId == null) return NotFound();

            var expenses = _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .ToList();

            return View(expenses);
        }
    }
}

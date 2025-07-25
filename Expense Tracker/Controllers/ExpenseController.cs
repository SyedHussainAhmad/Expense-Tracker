using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Expense_Tracker.Data;

namespace ExpenseTracker.Controllers
{
    [Authorize] // Ensures only logged-in users can access these actions
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var expenses = _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .ToList();
            return View(expenses);
        }

        public IActionResult Expense(int id)
        {
            var expenses = _context.Expenses
                .Where(e => e.ExpenseId == id)
                .Include(e => e.Category)
                .ToList();
            return View(expenses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddExpense()
        {
            // Manually retrieve form data
            var categoryId = HttpContext.Request.Form["CategoryId"].ToString();
            var amount = HttpContext.Request.Form["Amount"].ToString();
            var date = HttpContext.Request.Form["Date"].ToString();
            var note = HttpContext.Request.Form["Note"].ToString();

            // Manually create a new Expense object and set its properties
            var expense = new Expense
            {
                CategoryId = int.Parse(categoryId),
                Amount = decimal.Parse(amount),
                Date = DateTime.Parse(date),
                Note = note,
                UserId = User.Identity.Name
            };

            if (ModelState.IsValid)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                TempData["Success"] = "Expense added successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to add the expense. Please check the input.";
            ViewBag.Categories = _context.Categories.ToList();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null || expense.UserId != User.Identity.Name)
            {
                TempData["Error"] = "Expense not found or you do not have permission to edit it.";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList(); // Pass categories to the view
            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(expense);
                _context.SaveChanges();
                TempData["Success"] = "Expense updated successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to update the expense. Please check the input.";
            ViewBag.Categories = _context.Categories.ToList(); 
            return View(expense);
        }

        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null && expense.UserId == User.Identity.Name)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
                TempData["Success"] = "Expense deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete the expense. It may not exist or you do not have permission to delete it.";
            }
            return RedirectToAction("Index");
        }
    }
}

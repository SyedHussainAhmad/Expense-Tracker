using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using Expense_Tracker.Data;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Category(int id)
        {
            var categories = _context.Categories.Where(c=> c.CategoryId == id).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category added successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to add the category. Please check the input.";
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to update the category. Please check the input.";
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["Success"] = "Category deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete the category. It may not exist.";
            }
            return RedirectToAction("Index");
        }
    }
}

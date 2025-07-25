using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View(); // Login page
        }

        public IActionResult Register()
        {
            return View(); // Registration page
        }

        public IActionResult Profile()
        {
            return View(); // User profile page
        }

        public IActionResult Logout()
        {
            // Logic to log out the user will be added here
            return RedirectToAction("Index", "Home");
        }
    }
}

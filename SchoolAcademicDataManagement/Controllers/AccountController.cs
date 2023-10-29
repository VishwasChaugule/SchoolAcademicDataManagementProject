using System.Text;
using Microsoft.AspNetCore.Mvc;
using SchoolAcademicDataManagement.Models.User;
using SchoolAcademicDataManagement.Services;

namespace SchoolAcademicDataManagement.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model.Email, model.Password);

                if (user != null)
                {
                    // Authenticate successful, redirect to home page
                    var userRole = Encoding.UTF8.GetBytes(user.Role);
                    HttpContext.Session.Set("UserRole", userRole);
                    HttpContext.Session.SetString("IsAuthenticated", "True");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    Role = model.Role ?? "User"
                };

                _userService.Register(user);

                // Registration successful, redirect to login page
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Clear session data (if used)
            HttpContext.Session.Clear();

            // Redirect to the Login page after logout
            return RedirectToAction("Login", "Account");
        }
    }

}


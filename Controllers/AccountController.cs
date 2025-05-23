using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PureScanApp.Models;
using PureScanApp.Data;

namespace PureScanApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(User user, string password)
        {
            if (!ModelState.IsValid)
                return View(user);

            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email bulunamadı.");
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Şifre yanlış.");
                return View();
            }

            // Giriş başarılı → Session’a kullanıcıyı yaz
            HttpContext.Session.SetString("username", user.Username);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}

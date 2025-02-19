using Microsoft.AspNetCore.Mvc;

namespace Task_19_2_2025.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string email, string pass, string re_pass)
        {
            // تخزين البيانات في الجلسة
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("password", pass);
            HttpContext.Session.SetString("repeatPassword", re_pass);

            // التحقق من تطابق كلمات المرور
            if (pass == re_pass)
            {
                TempData["SuccessfulMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["ErrorMessage"] = "Your password and repeat password don't match!";
                return RedirectToAction("Register");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult HandleLogin(string email, string pass)
        {
            string storedEmail = HttpContext.Session.GetString("email");
            string storedPassword = HttpContext.Session.GetString("password");

            // التحقق من تطابق البريد الإلكتروني وكلمة المرور
            if (storedEmail == email && storedPassword == pass)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email or password!";
                return RedirectToAction("Login", "User");
            }
        }






        public IActionResult Profile()
        {
            // استرجاع القيم من الجلسة
            string name = HttpContext.Session.GetString("name");
            string email = HttpContext.Session.GetString("email");

            // التحقق من وجود القيم في الجلسة
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Name = name;
            ViewBag.Email = email;

            return View();
        }

    }
}

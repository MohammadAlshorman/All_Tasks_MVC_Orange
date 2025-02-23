using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("password", pass);
            HttpContext.Session.SetString("repeatPassword", re_pass);

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
            // استرجاع بيانات تسجيل الدخول من الكوكيز
            string savedEmail = Request.Cookies["UserEmail"];
            string savedPassword = Request.Cookies["UserPassword"];

            ViewData["SavedEmail"] = savedEmail;
            ViewData["SavedPassword"] = savedPassword;

            return View();
        }

        [HttpPost]
        public IActionResult HandleLogin(string email, string pass, string rememberMe)
        {
            string storedEmail = HttpContext.Session.GetString("email");
            string storedPassword = HttpContext.Session.GetString("password");

            if (storedEmail == email && storedPassword == pass)
            {
                // حفظ بيانات الجلسة بعد تسجيل الدخول
                HttpContext.Session.SetString("isLoggedIn", "true");
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("role", "user"); // غيّرها إلى "admin" إذا كان المستخدم أدمن

                // حفظ بيانات تسجيل الدخول في الكوكيز إذا تم تحديد Remember Me
                if (!string.IsNullOrEmpty(rememberMe))
                {
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7) // الكوكيز تبقى لمدة 7 أيام
                    };
                    Response.Cookies.Append("UserEmail", email, options);
                    Response.Cookies.Append("UserPassword", pass, options);
                }
                else
                {
                    Response.Cookies.Delete("UserEmail");
                    Response.Cookies.Delete("UserPassword");
                }

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
            string name = HttpContext.Session.GetString("name");
            string email = HttpContext.Session.GetString("email");
            string phone = HttpContext.Session.GetString("phone");
            string address = HttpContext.Session.GetString("address");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Phone = phone;
            ViewBag.Address = address;

            return View();
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // مسح جميع بيانات الجلسة
            return RedirectToAction("Login", "User"); // إعادة التوجيه إلى صفحة تسجيل الدخول
        }


        [HttpPost]
        public IActionResult UpdateProfile(string name, string phone, string address)
        {
            // تحديث البيانات في الجلسة
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("phone", phone ?? "");
            HttpContext.Session.SetString("address", address ?? "");

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }


        public IActionResult EditProfile()
        {
            // التحقق مما إذا كان المستخدم مسجل الدخول
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("email")))
            {
                return RedirectToAction("Login", "User");
            }

            // تمرير بيانات المستخدم إلى الصفحة
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            ViewBag.Address = HttpContext.Session.GetString("address");

            return View();
        }



    }
}

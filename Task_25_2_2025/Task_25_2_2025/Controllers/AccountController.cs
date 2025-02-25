using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_25_2_2025.Models;

namespace Task_25_2_2025.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }

        // ✅ عرض صفحة تسجيل الدخول
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ معالجة تسجيل الدخول
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                // تشفير كلمة المرور المدخلة قبل التحقق
                string hashedPassword = HashPassword(model.PasswordHash);

                // البحث عن المستخدم بناءً على البريد وكلمة المرور المشفرة
                var user = _context.Users
                    .Where(u => u.Email == model.Email && u.PasswordHash == hashedPassword)
                    .FirstOrDefault();

                if (user != null)
                {
                    // ✅ حفظ اسم المستخدم في الـ Session
                    HttpContext.Session.SetString("UserName", user.FullName);
                    return RedirectToAction("Index", "Home"); // 🔹 التوجيه للصفحة الرئيسية
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View(model);
        }

        // ✅ تسجيل الخروج
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // 🔹 مسح بيانات الجلسة
            return RedirectToAction("Login"); // 🔹 العودة لصفحة تسجيل الدخول
        }

        // ✅ دالة تشفير كلمة المرور باستخدام SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

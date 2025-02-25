using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Revision.Models;

namespace Revision.Controllers
{
    public class UserController : Controller
    {

        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Products()
        {
            var products = _context.Products.ToList();
            return View(products);
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(CurrentUser user)
        {
            if (ModelState.IsValid)
            {
                _context.CurrentUsers.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View();
                //return BadRequest(ModelState);

            }

        }

        public IActionResult Login()
        {

            return View();
        }



        [HttpPost]
        public IActionResult Login(CurrentUser user)
        {
            if (ModelState.IsValid)
            {
                var loggingUser = _context.CurrentUsers.FirstOrDefault(X => X.Email == user.Email && X.Password == user.Password);
                if (loggingUser != null)
                {
                    HttpContext.Session.SetString("UserName", loggingUser.Name);
                    HttpContext.Session.SetString("Email", loggingUser.Email);
                    HttpContext.Session.SetString("Role", loggingUser.Role);

                    TempData["name"] = loggingUser.Name;

                    if (loggingUser.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }




        public IActionResult Details(int id)
        {
            var products = _context.Products.Find(id);

            return View(products);
        }



        public IActionResult Profile()
        {
           

            return View();
        }


        public IActionResult EditProfile()
        {
            // Retrieve the current user's email from the session
            var userEmail = HttpContext.Session.GetString("Email");

            // Fetch the user's details from the database

            var user = _context.CurrentUsers.FirstOrDefault(X => X.Email == userEmail);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(CurrentUser updatedUser)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing user from the database
                var userEmail = HttpContext.Session.GetString("Email");
                var existingUser = _context.CurrentUsers.FirstOrDefault(X => X.Email == userEmail);



                // Update the user's details
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;


                // Save changes to the database
                _context.SaveChanges();

                // Update session data with the new values
                HttpContext.Session.SetString("UserName", updatedUser.Name);
                HttpContext.Session.SetString("Email", updatedUser.Email);

                // Redirect to the profile page
                return RedirectToAction("Profile");
            }

            // If the model state is invalid, return the view with validation errors
            return View(updatedUser);
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }


    }
}

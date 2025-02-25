using Microsoft.AspNetCore.Mvc;
using Revision.Models;

namespace Revision.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.CurrentUsers.ToList());
        }


        public IActionResult Users()
        {

            return View(_context.CurrentUsers.ToList());
        }

        public IActionResult EditUsers(int id)
        {
            var user = _context.CurrentUsers.Find(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult EditUsers(CurrentUser updatedUser, int id)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.CurrentUsers.Find(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update user details
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password; // Consider hashing the password
                existingUser.Role = updatedUser.Role;

                _context.SaveChanges();

                return RedirectToAction("Users");


            }
            return View(updatedUser);
        }




        public IActionResult UserDetails(int id)
        {

            return View(_context.CurrentUsers.Find(id));
        }


        public IActionResult DeleteUsers(int id)
        {
            var user = _context.CurrentUsers.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _context.CurrentUsers.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("users");
            }

        }

        public IActionResult CreateUsers()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateUsers(CurrentUser user)
        {

            _context.CurrentUsers.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Users");



        }

        // =====================================



        public IActionResult Products()
        {

            return View(_context.Products.ToList());
        }








        public IActionResult EditProducts(int id)
        {
            var product = _context.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProducts(Product updatedPro, int id)
        {
            if (ModelState.IsValid)
            {
                var existingPro = _context.Products.Find(id);
                if (existingPro == null)
                {
                    return NotFound();
                }

                // Update user details
                existingPro.Name = updatedPro.Name;
                existingPro.Price = updatedPro.Price;
                existingPro.Description = updatedPro.Description; // Consider hashing the password
                existingPro.Image = updatedPro.Image;

                _context.SaveChanges();

                return RedirectToAction("Products");


            }
            return View(updatedPro);
        }




        public IActionResult ProductDetails(int id)
        {

            return View(_context.Products.Find(id));
        }


        public IActionResult DeletePro(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("users");
            }

        }

        public IActionResult CreateProduct()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Products");



        }



        public IActionResult Profile()
        {

            return View();
        }




        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");


        }




    }
}

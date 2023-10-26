using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            ViewBag.Firstname = HttpContext.Session.GetString("Firstname");
            ViewBag.Lastname = HttpContext.Session.GetString("Lastname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            ViewBag.ImagePath = HttpContext.Session.GetString("Image");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");


            ViewBag.UserAdminCount = _context.Useradmins.Count();
            ViewBag.SubscriptionsCount = _context.Subscriptions.Count();

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToShortDateString();
            ViewBag.CurrentDate = formattedDate;

            return View();
        }
    }
}

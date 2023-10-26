using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace First_Project.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AuthenticationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
    
        public IActionResult Register2()
        {
            return View();
        }
      
        public IActionResult Login2()
        {
            return View();
        }
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2([Bind("Userid,Password,Email,Firstname,Lastname,Userrole,IMAGE,ImageFile")] Useradmin useradmin)
        {
            if (ModelState.IsValid)
            {
            if (useradmin.ImageFile != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + useradmin.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await useradmin.ImageFile.CopyToAsync(fileStream);
                }
                useradmin.ImagePath = fileName;
            }
              var user = _context.Useradmins.Where(x => x.Email == useradmin.Email).FirstOrDefault();
                if (user == null)
                {
                   useradmin.Userrole = 2;
                    _context.Add(useradmin);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction("Login2", "Authentication");
                }
                else
                {
                    ViewBag.Error = "Email is already used, please try another one.";
                }
            }
            return View(useradmin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login2([Bind("Userid,Password,Email")] Useradmin useradmin)
        {
            var auth = _context.Useradmins.Where(x => x.Email == useradmin.Email && x.Password == useradmin.Password).FirstOrDefault();

            if (auth != null)
            {
                //var user = _context.Useradmins.Where(x => x.Userid == auth.Userid).FirstOrDefault();

                switch (auth.Userrole)
                {
                    case 1:
                        HttpContext.Session.SetString("Firstname", auth.Firstname);
                        HttpContext.Session.SetString("Lastname", auth.Lastname);
                        HttpContext.Session.SetString("Email", auth.Email);
                        HttpContext.Session.SetString("Password", auth.Password);
                        if (auth.ImagePath != null)
                        {
                            HttpContext.Session.SetString("Image", auth.ImagePath);
                        }
                        HttpContext.Session.SetInt32("Userid", (int)auth.Userid);
                        return RedirectToAction("Index", "Admin");
                    case 2:
                        HttpContext.Session.SetString("Firstname", auth.Firstname);
                        HttpContext.Session.SetString("Lastname", auth.Lastname);
                        HttpContext.Session.SetString("Email", auth.Email);
                        HttpContext.Session.SetString("Password", auth.Password);
                        if (auth.ImagePath != null)
                        {
                            HttpContext.Session.SetString("Image", auth.ImagePath);
                        }
                        HttpContext.Session.SetInt32("Userid", (int)auth.Userid);

                        return RedirectToAction("Index", "Homes");
                }
            }
            else
            {
                ViewBag.Error = "Wrong credentials";
            }

            return View();
        }
    }
    }


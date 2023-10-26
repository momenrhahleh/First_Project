using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;

namespace First_Project.Controllers
{
    public class UseradminsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UseradminsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        

        // GET: Useradmins
        public async Task<IActionResult> Index()
        {

            ViewBag.Firstname = HttpContext.Session.GetString("Firstname");
            ViewBag.Lastname = HttpContext.Session.GetString("Lastname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            ViewBag.ImagePath = HttpContext.Session.GetString("Image");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Userrole = HttpContext.Session.GetInt32("Userrole");

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToShortDateString();
            ViewBag.CurrentDate = formattedDate;
            var modelContext = _context.Useradmins.Include(u => u.UserroleNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Useradmins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Useradmins == null)
            {
                return NotFound();
            }

            var useradmin = await _context.Useradmins
                .Include(u => u.UserroleNavigation)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useradmin == null)
            {
                return NotFound();
            }

            return View(useradmin);
        }

        // GET: Useradmins/Create
        public IActionResult Create()
        {
            ViewData["Userrole"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Useradmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Password,Email,Firstname,Lastname,Userrole,IMAGE,ImageFile")] Useradmin useradmin)
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
            
            if (ModelState.IsValid)
            {
                _context.Add(useradmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userrole"] = new SelectList(_context.Roles, "Roleid", "Roleid", useradmin.Userrole);
            return View(useradmin);
        }

        // GET: Useradmins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {

            if (id == null || _context.Useradmins == null)
            {
                return NotFound();
            }

            var useradmin = await _context.Useradmins.FindAsync(id);
            if (useradmin == null)
            {
                return NotFound();
            }
            ViewBag.Firstname = HttpContext.Session.GetString("Firstname");
            ViewBag.Lastname = HttpContext.Session.GetString("Lastname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            ViewBag.ImagePath = HttpContext.Session.GetString("Image");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Userrole = HttpContext.Session.GetInt32("Userrole");

            ViewData["Userrole"] = new SelectList(_context.Roles, "Roleid", "Roleid", useradmin.Userrole);
            return View(useradmin);
        }
        // Change the route to accept the UserId as a parameter

        

        // POST: Useradmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Password,Email,Firstname,Lastname,Userrole,IMAGE,ImageFile")] Useradmin useradmin)
        {
            if (id != useradmin.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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

                    _context.Update(useradmin);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Image", useradmin.ImagePath);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseradminExists(useradmin.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userrole"] = new SelectList(_context.Roles, "Roleid", "Roleid", useradmin.Userrole);
            return View(useradmin);
        }

        // GET: Useradmins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Useradmins == null)
            {
                return NotFound();
            }

            var useradmin = await _context.Useradmins
                .Include(u => u.UserroleNavigation)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useradmin == null)
            {
                return NotFound();
            }

            return View(useradmin);
        }

        // POST: Useradmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Useradmins == null)
            {
                return Problem("Entity set 'ModelContext.Useradmins'  is null.");
            }
            var useradmin = await _context.Useradmins.FindAsync(id);
            if (useradmin != null)
            {
                _context.Useradmins.Remove(useradmin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseradminExists(decimal id)
        {
          return (_context.Useradmins?.Any(e => e.Userid == id)).GetValueOrDefault();
        }
    }
}

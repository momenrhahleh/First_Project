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
    public class AboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AboutusController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        

        // GET: Aboutus
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

            return _context.Aboutus != null ? 
                          View(await _context.Aboutus.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Aboutus'  is null.");
        }

        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Aboutusid == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aboutusid,IMAGE,Paragraphtext,Text,Userid,ImageFile")] Aboutu aboutu)
        {
            if (aboutu.ImageFile != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;



                string fileName = Guid.NewGuid().ToString() + aboutu.ImageFile.FileName;



                string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await aboutu.ImageFile.CopyToAsync(fileStream);
                }



                aboutu.ImagePath = fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(aboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
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
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Aboutusid,IMAGE,Paragraphtext,Text,Userid,ImageFile")] Aboutu aboutu)
        {
            if (id != aboutu.Aboutusid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + aboutu.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(fileStream);
                        }

                        aboutu.ImagePath = fileName;
                    }

                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.Aboutusid))
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
            return View(aboutu);
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Aboutusid == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Aboutus == null)
            {
                return Problem("Entity set 'ModelContext.Aboutus'  is null.");
            }
            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu != null)
            {
                _context.Aboutus.Remove(aboutu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
          return (_context.Aboutus?.Any(e => e.Aboutusid == id)).GetValueOrDefault();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;

namespace First_Project.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            ViewBag.Firstname = HttpContext.Session.GetString("Firstname");
            ViewBag.ImagePath = HttpContext.Session.GetString("Image");

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToShortDateString();
            ViewBag.CurrentDate = formattedDate;

            return _context.Homes != null ?
                          View(await _context.Homes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homes'  is null.");
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homeid,IMAGE,Text,Title,ImageFile")] Home home)
        {
            if (home.ImageFile != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;



                string fileName = Guid.NewGuid().ToString() + home.ImageFile.FileName;



                string path = Path.Combine(wwwRootPath + "/Images/" + fileName);



                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await home.ImageFile.CopyToAsync(fileStream);
                }



                home.ImagePath = fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
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
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homeid,IMAGE,Text,Title,ImageFile")] Home home)
        {
            if (id != home.Homeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImageFile != null)
                    {
                        string wwwRootPath = webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + home.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(fileStream);
                        }

                        home.ImagePath = fileName;
                    }

                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Homeid))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homes == null)
            {
                return Problem("Entity set 'ModelContext.Homes'  is null.");
            }
            var home = await _context.Homes.FindAsync(id);
            if (home != null)
            {
                _context.Homes.Remove(home);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
          return (_context.Homes?.Any(e => e.Homeid == id)).GetValueOrDefault();
        }
    }
}

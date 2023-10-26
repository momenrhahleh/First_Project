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
    public class BeneficiariesController : Controller
    {
        private readonly ModelContext _context;

        public BeneficiariesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Beneficiaries
        public async Task<IActionResult> Index()
        {
            ViewBag.Firstname = HttpContext.Session.GetString("Firstname");
            ViewBag.ImagePath = HttpContext.Session.GetString("Image");

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToShortDateString();
            ViewBag.CurrentDate = formattedDate;

            return _context.Beneficiaries != null ? 
                          View(await _context.Beneficiaries.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Beneficiaries'  is null.");
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .FirstOrDefaultAsync(m => m.Beneficiaryid == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Beneficiaryid,Userid,Subscriptionid,Name,Relationship")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Beneficiaryid,Userid,Subscriptionid,Name,Relationship")] Beneficiary beneficiary)
        {
            if (id != beneficiary.Beneficiaryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.Beneficiaryid))
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
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .FirstOrDefaultAsync(m => m.Beneficiaryid == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Beneficiaries == null)
            {
                return Problem("Entity set 'ModelContext.Beneficiaries'  is null.");
            }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiaryExists(decimal id)
        {
          return (_context.Beneficiaries?.Any(e => e.Beneficiaryid == id)).GetValueOrDefault();
        }
    }
}

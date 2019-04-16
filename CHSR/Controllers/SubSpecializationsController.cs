using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CHSR.Models;

namespace CHSR.Controllers
{
    public class SubSpecializationsController : Controller
    {
        private readonly CHSRContext _context;

        public SubSpecializationsController(CHSRContext context)
        {
            _context = context;
        }

        // GET: SubSpecializations
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubSpecializations.ToListAsync());
        }

        // GET: SubSpecializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSpecialization = await _context.SubSpecializations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subSpecialization == null)
            {
                return NotFound();
            }

            return View(subSpecialization);
        }

        // GET: SubSpecializations/Create
        public async Task<IActionResult> Create()
        {
            var specializations = await _context.Specializations.ToListAsync();

            var specilizationList = new List<SelectListItem>();

            foreach (var specilization in specializations)
            {
                specilizationList.Add(new SelectListItem { Text = specilization.Name, Value = specilization.Id.ToString() });
            }

            ViewData["Specializations"] = specilizationList;
            return View();
        }

        // POST: SubSpecializations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SubSpecialization subSpecialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subSpecialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subSpecialization);
        }

        // GET: SubSpecializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSpecialization = await _context.SubSpecializations.FindAsync(id);
            if (subSpecialization == null)
            {
                return NotFound();
            }
            return View(subSpecialization);
        }

        // POST: SubSpecializations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SubSpecialization subSpecialization)
        {
            if (id != subSpecialization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subSpecialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubSpecializationExists(subSpecialization.Id))
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
            return View(subSpecialization);
        }

        // GET: SubSpecializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSpecialization = await _context.SubSpecializations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subSpecialization == null)
            {
                return NotFound();
            }

            return View(subSpecialization);
        }

        // POST: SubSpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subSpecialization = await _context.SubSpecializations.FindAsync(id);
            _context.SubSpecializations.Remove(subSpecialization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubSpecializationExists(int id)
        {
            return _context.SubSpecializations.Any(e => e.Id == id);
        }
    }
}

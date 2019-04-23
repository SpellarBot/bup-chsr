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
    public class ResourcePersonsController : Controller
    {
        private readonly CHSRContext _context;

        public ResourcePersonsController(CHSRContext context)
        {
            _context = context;
        }

        // GET: ResourcePersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResourcePerson.ToListAsync());
        }

        // GET: ResourcePersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourcePerson = await _context.ResourcePerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourcePerson == null)
            {
                return NotFound();
            }

            return View(resourcePerson);
        }

        // GET: ResourcePersons/Create
        public async Task<IActionResult> Create()
        {
            //var institutes = await _context.Institutes.ToListAsync();
            List<Institute> institutes = _context.Institutes.ToListAsync().Result;
            ViewData["institutes"] = institutes;

            return View();
        }

        // POST: ResourcePersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Institute,Faculty,Department,Designation,Phone,Email,Specialization,SubSpecialization")] ResourcePerson resourcePerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourcePerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourcePerson);
        }

        // GET: ResourcePersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourcePerson = await _context.ResourcePerson.FindAsync(id);
            if (resourcePerson == null)
            {
                return NotFound();
            }
            return View(resourcePerson);
        }

        // POST: ResourcePersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Institute,Faculty,Department,Designation,Phone,Email,Specialization,SubSpecialization")] ResourcePerson resourcePerson)
        {
            if (id != resourcePerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourcePerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourcePersonExists(resourcePerson.Id))
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
            return View(resourcePerson);
        }

        // GET: ResourcePersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourcePerson = await _context.ResourcePerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourcePerson == null)
            {
                return NotFound();
            }

            return View(resourcePerson);
        }

        // POST: ResourcePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resourcePerson = await _context.ResourcePerson.FindAsync(id);
            _context.ResourcePerson.Remove(resourcePerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourcePersonExists(int id)
        {
            return _context.ResourcePerson.Any(e => e.Id == id);
        }
    }
}

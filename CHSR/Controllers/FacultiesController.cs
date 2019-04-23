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
    public class FacultiesController : Controller
    {
        private readonly CHSRContext _context;

        public FacultiesController(CHSRContext context)
        {
            _context = context;
        }




        public async Task<IActionResult> GetFaculties(int id)
        {
          
            return Json(await _context.Faculties.Where(x => x.Institute.ID == id).ToListAsync());
        }

        // GET: Faculties
        public async Task<IActionResult> Index(int id)
        {
             
             return View(await _context.Faculties.Where(x=>x.Institute.ID==id).ToListAsync());
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.ID == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create(int? id)
        {
            var institute = _context.Institutes.FindAsync(id).Result;
            //List<Institute> institutes =  _context.Institutes.ToListAsync().Result;
            ViewData["ins"] = institute;
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Faculty faculty, int instituteID)
        {
            // int instituteID =(int) TempData["Data1"];

            if (ModelState.IsValid)
            {
                var institute = _context.Institutes.FindAsync(instituteID).Result;
                faculty.Institute = institute;

                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = instituteID });
            }
            return View(faculty);
        }



        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.Where(c => c.ID == id).Include(c => c.Institute).FirstOrDefaultAsync();
            ViewData["faculty"] = faculty;
                
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Faculty faculty,int instituteID)
        {
            if (id != faculty.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                

                return RedirectToAction(nameof(Index), new { id = instituteID });
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.Where(c => c.ID == id).Include(c => c.Institute).FirstOrDefaultAsync();
            ViewData["faculty"] = faculty;
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int instituteID)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new {id=instituteID });
        }

        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.ID == id);
        }
    }
}

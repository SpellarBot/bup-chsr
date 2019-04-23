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
    public class DepartmentsController : Controller
    {
        private readonly CHSRContext _context;

        public DepartmentsController(CHSRContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetDepartments(int id)
        {
            return Json(await _context.Departments.Where(x => x.Faculty.ID == id).ToListAsync());
        }


        // GET: Departments
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Departments.Where(x => x.Faculty.ID == id).ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create(int?id)
        {
            var faculty = _context.Faculties.FindAsync(id).Result;
            ViewData["faculty"] = faculty;
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Department department,int facultyID)
        {
            if (ModelState.IsValid)
            {
                var faculty = _context.Faculties.FindAsync(facultyID).Result;
                department.Faculty = faculty;

                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = facultyID });
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Where(c => c.id == id).Include(c => c.Faculty).FirstOrDefaultAsync();
            ViewData["department"] = department;

            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name")] Department department, int facultyID)
        {
            if (id != department.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new {id=facultyID });
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Where(c => c.id == id).Include(c => c.Faculty).FirstOrDefaultAsync();
            ViewData["department"] = department;

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int facultyID)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new {id=facultyID });
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.id == id);
        }
    }
}

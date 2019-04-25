using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CHSR.Models;
using System.IO;

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
            List<Task> tasks = new List<Task>();
            Task<List<ResourcePerson>> resourcePerson = _context.ResourcePerson.ToListAsync();
            Task<List<Institute>> institutes= _context.Institutes.ToListAsync();
            Task<List<Faculty>> faculties = _context.Faculties.ToListAsync();
            Task<List<Department>> departments = _context.Departments.ToListAsync();
            Task<List<Specialization>> specializations = _context.Specializations.ToListAsync();
            Task<List<SubSpecialization>> subSpecializations = _context.SubSpecializations.ToListAsync();

            tasks.Add(resourcePerson);
            tasks.Add(institutes);
            tasks.Add(faculties);
            tasks.Add(departments);
            tasks.Add(specializations);
            tasks.Add(subSpecializations);

            Task.WaitAll(tasks.ToArray());

            foreach (var rp in resourcePerson.Result)
            {
                var institute = institutes.Result.Find(x => x.ID.ToString() == rp.Institute);
                if (institute != null)
                    rp.Institute = institute.Name;

                var faculty = faculties.Result.Find(x => x.ID.ToString() == rp.Faculty);
                if (faculty != null)
                    rp.Faculty = faculty.Name;

                var department = departments.Result.Find(x => x.id.ToString() == rp.Department);
                if (department != null)
                    rp.Department = department.Name;

                var specialization = specializations.Result.Find(x => x.Id.ToString() == rp.Specialization);
                if (specialization != null)
                    rp.Specialization = specialization.Name;

                var subSpecialization = subSpecializations.Result.Find(x => x.Id.ToString() == rp.SubSpecialization);
                if (subSpecialization != null)
                    rp.SubSpecialization = subSpecialization.Name;

            }


            return View(resourcePerson.Result);
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

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.ID == id);

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.ID == id);

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.id == id);
            var specialization = await _context.Specializations
                .FirstOrDefaultAsync(m => m.Id == id);

            var subSpecialization = await _context.SubSpecializations
               .FirstOrDefaultAsync(m => m.Id == id);

            resourcePerson.Institute = institute.Name;
            resourcePerson.Faculty = faculty.Name;
            resourcePerson.Department = department.Name;
            resourcePerson.Specialization = specialization.Name;
            resourcePerson.SubSpecialization = subSpecialization.Name;

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
            List<Specialization> specializations = _context.Specializations.ToListAsync().Result;

            ViewData["institutes"] = institutes;
            ViewData["specializations"] = specializations;
            return View();
        }

        // POST: ResourcePersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Institute,Faculty,Department,Designation,Phone,Email,Specialization,SubSpecialization,Photo,PhotoId")] ResourcePerson resourcePerson)
        {
            if (ModelState.IsValid)
            {

                var traceId = Guid.NewGuid().ToString();
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\ResourcePerson", traceId);

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                if (resourcePerson.Photo != null || resourcePerson.Photo.Length > 0)
                {
                    var profilePicturePath = Path.Combine(rootPath, resourcePerson.Photo.FileName);

                    using (var stream = new FileStream(profilePicturePath, FileMode.Create))
                    {
                        await resourcePerson.Photo.CopyToAsync(stream);
                        resourcePerson.PhotoId = resourcePerson.Photo.FileName;
                    }
                }





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

﻿using CHSR.Models;
using CHSR.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Controllers
{
    public class ResourcePersonsController : Controller
    {
        private readonly CHSRContext _context;
        private readonly FileAddRemoveService _fileService;

        public ResourcePersonsController(CHSRContext context, FileAddRemoveService fileUpService)
        {
            _context = context;
            _fileService = fileUpService;
        }

        // GET: ResourcePersons
        public async Task<IActionResult> Index()
        {
            List<Task> tasks = new List<Task>();
            Task<List<ResourcePerson>> resourcePerson = _context.ResourcePerson.ToListAsync();
            Task<List<Institute>> institutes = _context.Institutes.ToListAsync();
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


            List<ResearchInterest> researchInterests = _context.ResearchAreas.Where(m => m.ResourcePersonId == resourcePerson.Id).Select(x => x.ResearchInterest).ToList();

            ViewData["researchInterests"] = researchInterests;



            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.ID.ToString() == resourcePerson.Institute);

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.ID.ToString() == resourcePerson.Faculty);

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.id.ToString() == resourcePerson.Department);
            var specialization = await _context.Specializations
                .FirstOrDefaultAsync(m => m.Id.ToString() == resourcePerson.Specialization);

            var subSpecialization = await _context.SubSpecializations
               .FirstOrDefaultAsync(m => m.Id.ToString() == resourcePerson.SubSpecialization);

            resourcePerson.Institute = institute == null ? "" : institute.Name;
            resourcePerson.Faculty = faculty == null ? "" : faculty.Name;
            resourcePerson.Department = department == null ? "" : department.Name;
            resourcePerson.Specialization = specialization == null ? "" : specialization.Name;
            resourcePerson.SubSpecialization = subSpecialization == null ? "" : subSpecialization.Name;

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
            List<ResearchInterest> researchInterests = _context.ResearchInterests.ToListAsync().Result;

            ViewData["institutes"] = institutes;
            ViewData["specializations"] = specializations;
            ViewData["researchInterests"] = researchInterests;
            return View();
        }

        // POST: ResourcePersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Institute,Faculty,Department,Designation,Phone,Email,Specialization,SubSpecialization,Photo,ResearchAreas")] ResourcePerson resourcePerson, ICollection<string> ResearchAreas)
        {
            if (ModelState.IsValid)
            {

                var traceId = Guid.NewGuid().ToString();
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\ResourcePerson", traceId);

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                if (resourcePerson.Photo != null && resourcePerson.Photo.Length > 0)
                {
                    _fileService.UploadFile(resourcePerson.Photo, rootPath);
                    resourcePerson.PicFolderId = traceId;
                    resourcePerson.PhotoFileName = resourcePerson.Photo.FileName;
                }

                foreach (string item in ResearchAreas)
                {
                    int value = 0;
                    if (int.TryParse(item, out value))
                    {
                        resourcePerson.ResearchAreas.Add(new ResearchArea { ResearchInterestId = value });
                    }
                    else
                    {
                        resourcePerson.ResearchAreas.Add(new ResearchArea { ResearchInterest = new ResearchInterest { AreaName = item } });
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

            List<Institute> institutes = await _context.Institutes.ToListAsync();
            List<Faculty> faculties = await _context.Faculties.Where(x => x.Institute.ID.ToString() == resourcePerson.Institute).ToListAsync();
            List<Department> departments = await _context.Departments.Where(x => x.Faculty.ID.ToString() == resourcePerson.Faculty).ToListAsync();

            List<Specialization> specializations = await _context.Specializations.ToListAsync();
            List<SubSpecialization> subSpecializations = await _context.SubSpecializations.Where(x => x.Specialization.Id.ToString() == resourcePerson.Specialization).ToListAsync();

            ViewData["institutes"] = institutes;
            ViewData["faculties"] = faculties;
            ViewData["departments"] = departments;
            ViewData["specializations"] = specializations;
            ViewData["subSpecializations"] = subSpecializations;


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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Institute,Faculty,Department,Designation,Phone,Email,Specialization,SubSpecialization,Photo")] ResourcePerson resourcePerson, string pid,string FileName)
        {
            if (id != resourcePerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //var traceId = Guid.NewGuid().ToString();

               // var temp = await _context.ResourcePerson.FindAsync(id);

                //if(resourcePerson.Institute==null)
                //{
                //    resourcePerson.Faculty = null;
                //    resourcePerson.Department = null;
                //}
                //if (resourcePerson.Faculty == null)
                //{
                //    resourcePerson.Department = null;
                //}
                //if(resourcePerson.Specialization==null)
                //{
                //    resourcePerson.SubSpecialization = null;
                //}

                if (pid==null) pid= Guid.NewGuid().ToString();

                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\ResourcePerson", pid);

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }


                if (resourcePerson.Photo != null && resourcePerson.Photo.Length > 0)
                {
                    _fileService.UploadFile(resourcePerson.Photo, rootPath);

                    string temp = rootPath+"\\"+FileName;

                    _fileService.RemoveFile(temp);

                   
                    resourcePerson.PhotoFileName = resourcePerson.Photo.FileName;
                    resourcePerson.PicFolderId = pid;
                }
                else
                {
                    resourcePerson.PhotoFileName = FileName;
                    resourcePerson.PicFolderId = pid;
                }
                

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
        public async Task<IActionResult> Delete(int id,string pid,string FileName)
        {
            if (pid != null)
            {
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\ResourcePerson", pid);

               if(FileName!=null) _fileService.RemoveFile(rootPath + "\\" + FileName);

                Directory.Delete(rootPath);
            }


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

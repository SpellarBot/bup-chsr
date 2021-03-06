﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CHSR.Models;

namespace CHSR.Controllers
{
    public class InstitutesController : Controller
    {
        private readonly CHSRContext _context;

        public InstitutesController(CHSRContext context)
        {
            _context = context;
        }

        // GET: Institutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutes.ToListAsync());
        }

        // GET: Institutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // GET: Institutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Location")] Institute institute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institute);
        }

        // GET: Institutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes.FindAsync(id);
            if (institute == null)
            {
                return NotFound();
            }
            return View(institute);
        }

        // POST: Institutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location")] Institute institute)
        {
            if (id != institute.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(institute.ID))
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
            return View(institute);
        }

        // GET: Institutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // POST: Institutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institute = await _context.Institutes.FindAsync(id);
            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(int id)
        {
            return _context.Institutes.Any(e => e.ID == id);
        }
    }
}

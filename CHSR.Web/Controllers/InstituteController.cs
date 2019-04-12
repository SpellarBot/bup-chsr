
using Microsoft.AspNetCore.Mvc;
using CHSR.DataCrudService;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;
using CHSR.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CHSR.Domain.Setup;
using System;

namespace CHSR.Web.Controllers
{
    public class InstituteController : BaseController<Institute>
    {
        private ApplicationDbContext _context;
        public InstituteController(InstituteDataCrudService instituteDataCrudService, ApplicationDbContext context) : base(instituteDataCrudService)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var qry = dataCrudService.Get().AsNoTracking().OrderBy(x => x.InstituteName);
            var model = await PagingList.CreateAsync(qry, 2, page);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteName")] Institute supplier)
        {
            if (ModelState.IsValid)
            {
                dataCrudService.Insert(supplier);
                await dataCrudService.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Suppliers/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction("Index", "StatusCode", new { statusCode = 404 });
            }

            var institute = await dataCrudService.GetByIdAsync(id);
            if (institute == null)
            {
                return NotFound();
            }

            ViewData["Institute"] = institute;

            return View();
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await dataCrudService.GetByIdAsync(id);
            if (institute == null)
            {
                return NotFound();
            }

            ViewData["Institute"] = institute;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, InstituteName")] Institute institute)
        {
            if (id != institute.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(suppliers);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!SuppliersExists(suppliers.SupplierId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                return RedirectToAction("Index");
            }

            dataCrudService.Update(institute);
            await dataCrudService.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await dataCrudService.GetByIdAsync(id);
            if (institute == null)
            {
                return NotFound();
            }

            ViewData["Institute"] = institute;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            dataCrudService.Delete(id);
            await dataCrudService.SaveChangesAsync();
            //_context.Suppliers.Remove(suppliers);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
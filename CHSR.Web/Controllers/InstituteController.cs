
using Microsoft.AspNetCore.Mvc;
using CHSR.DataCrudService;
using System.Threading.Tasks;
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
            var data = dataCrudService.Get().AsNoTracking().OrderBy(x => x.InstituteName).ToList();          
            return View(data);
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

            return View(institute);
        }
    }
}
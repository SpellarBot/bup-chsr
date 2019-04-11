
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

        public InstituteController(InstituteDataCrudService instituteDataCrudService, ApplicationDbContext context) : base(instituteDataCrudService)
        {
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

            //var suppliers = await _context.Suppliers.SingleOrDefaultAsync(m => m.SupplierId == id);
            var institute = await dataCrudService.GetByIdAsync(id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }
    }
}
using CHSR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using CHSR.Web.Models;

namespace CHSR.Web.Controllers
{
    public class UAMController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UAMController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }


        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            var role = new RoleViewModel();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Name")]RoleViewModel role)
        {
            var _role = new IdentityRole { Name = role.Name };
            var result = await _roleManager.CreateAsync(_role);
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public IActionResult Roles()
        {
            return View(_context.Roles.ToList());
        }
    }
}
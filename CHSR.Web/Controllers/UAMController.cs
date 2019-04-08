using CHSR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CHSR.Web.Models;
using CHSR.Domain.UAM;
using System.Collections.Generic;
using CHSR.Web.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CHSR.Web.Controllers
{
    public class UAMController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UAMController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Users()
        {
            var userListViewModel = new List<UserViewModel>();

            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    FullName = user.FirstName + " " + user.LastName,
                    Email = user.Email
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                userViewModel.RoleName = userRoles.FirstOrDefault();

                userListViewModel.Add(userViewModel);
            }

            return View(userListViewModel);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.ToList();

            List<SelectListItem> mySkills = new List<SelectListItem>();

            foreach (var role in roles)
            {
                mySkills.Add(new SelectListItem { Text = role.Name, Value = role.Id });
            }

            ViewData["Roles"] = mySkills;

            return View();
        }

        [Authorize(Roles ="Manager")]
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

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(IdentityRole role)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(role.Id);
            await _roleManager.DeleteAsync(roleToDelete);
            return RedirectToAction("Roles", "UAM");
        }
    }
}
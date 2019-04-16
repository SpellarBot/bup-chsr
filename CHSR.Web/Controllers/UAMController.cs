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
using CHSR.ViewModel.Account;

namespace CHSR.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
                    UserId = user.Id,
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
                mySkills.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }

            ViewData["Roles"] = mySkills;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel)
        {
            var user = new ApplicationUser
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };

            var userCreateResponse = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (userCreateResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerViewModel.RoleId);
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var roles = _roleManager.Roles.ToList();

            List<SelectListItem> mySkills = new List<SelectListItem>();

            foreach (var role in roles)
            {
                mySkills.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }

            ViewData["Roles"] = mySkills;


            var user = await _userManager.FindByIdAsync(id);
            var userRoleNames = await _userManager.GetRolesAsync(user);

            var userModel = new RegisterViewModel
            { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                RoleId = userRoleNames.FirstOrDefault()
            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(RegisterViewModel registerViewModel)
        {


            var userResponse = _userManager.FindByNameAsync(registerViewModel.UserName);

            var user = userResponse.Result;

            var roleNames = await _userManager.GetRolesAsync(user);

            if (!roleNames.Contains(registerViewModel.RoleId))
            {
                await _userManager.RemoveFromRoleAsync(user, roleNames.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, registerViewModel.RoleId);
            }


            user.FirstName = registerViewModel.FirstName;
            user.LastName = registerViewModel.LastName;
            user.UserName = registerViewModel.Email;
            user.Email = registerViewModel.Email;


            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(ApplicationUser user)
        {
            var userInfo = await _userManager.FindByIdAsync(user.Id);

            var userRoles = await _userManager.GetRolesAsync(userInfo);

            await _userManager.DeleteAsync(userInfo);
            await _userManager.RemoveFromRolesAsync(userInfo, userRoles);

            return RedirectToAction("Users");
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
            ViewData["RoleList"] = _context.Roles.ToList();
            return View();
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

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var registerViewModel = new RegisterViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByNameAsync(registerViewModel.UserName);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, registerViewModel.Password);
            return RedirectToAction("Users");
        }
    }
}
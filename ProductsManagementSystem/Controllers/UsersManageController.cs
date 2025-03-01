using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Account;
using ProductsManagementSystem.Models.VM.UsersManage;
using System.Threading.Tasks;

namespace ProductsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersManageController : Controller
    {
        private readonly IUsersManageService _usersManageService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersManageController(IUsersManageService usersManageService, RoleManager<IdentityRole> roleManager)
        {
            _usersManageService = usersManageService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _usersManageService.GetUsers();
            return View(users);
        }

        public IActionResult CreateUser()
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(x =>
                new SelectListItem
                {
                    Text = x,
                    Value = x
                })
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usersManageService.CreateUser(model);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string Id)
        {
            await _usersManageService.ResetPassword(Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _usersManageService.DeleteUser(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await _usersManageService.GetUser(Id);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _usersManageService.EditUser(model);
                return RedirectToAction("Index");
            }

            var result = new IdentityResult();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}
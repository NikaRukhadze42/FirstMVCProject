using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models.VM.UserRole;

namespace ProductsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public UserRoleController(IUserRoleService userRoleService, IRoleService roleService)
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.users = await _userRoleService.GetAllUsers();
            return View();
        }

        public async Task<IActionResult> AddRole(string userId)
        {
            ViewBag.user = await _userRoleService.GetUser(userId);
            var roles = await _roleService.GetAllRoles();
            var roleNames = new List<string>();
            roles.ForEach(x =>
            {
                roleNames.Add(x.Name);
            });
            ViewBag.roles = roleNames;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserRoleUpdateViewModel userRoleUpdateViewModel)
        {
            await _userRoleService.AddRoles(userRoleUpdateViewModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateRoles(string userId)
        {
            ViewBag.user = await _userRoleService.GetUser(userId);
            var roles = await _roleService.GetAllRoles();
            var roleNames = new List<string>();
            roles.ForEach(x =>
            {
                roleNames.Add(x.Name);
            });
            ViewBag.roles = roleNames;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoles(UserRoleUpdateViewModel userRoleUpdateViewModel)
        {
            await _userRoleService.UpdateRoles(userRoleUpdateViewModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoles(string id)
        {
            await _userRoleService.RemoveRoles(id);
            return RedirectToAction("Index");
        }
    }
}

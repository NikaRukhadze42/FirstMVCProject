using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models.VM.Account;

namespace ProductsManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountService _service;
        public readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IAccountService service, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Register()
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = new IdentityResult();
            if (ModelState.IsValid)
            {
                result = await _service.Register(model);
                if (result.Succeeded)
                    return RedirectToAction("Index","Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(model);
                if (result.Succeeded)
                    return RedirectToAction("Index","Home");
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

    }
}
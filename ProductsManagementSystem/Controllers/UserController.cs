using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models.VM.User;
using System.Threading.Tasks;

namespace ProductsManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UpdatePersonalInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo(UserInfoUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdatePersonalInfo(model);
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        public IActionResult ChangeUserName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserName(UserUserNameChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.ChangeUserName(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.PasswordChange(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public IActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(UserDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.DeleteUser(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
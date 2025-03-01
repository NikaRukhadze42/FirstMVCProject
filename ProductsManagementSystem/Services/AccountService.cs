using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Account;

namespace ProductsManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(ApplicationDbContext applicationDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = applicationDbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }



        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginViewModel.Email);
            if (user == null)
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, loginViewModel.RememberMe);
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> Register(RegisterViewModel registerViewModel)
        {
            var newUser = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if( result.Succeeded)
            {
                if (registerViewModel.SelectedRole != null)
                    await _userManager.AddToRoleAsync(newUser, registerViewModel.SelectedRole);
                else
                    await _userManager.AddToRoleAsync(newUser, "User");
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }
            return result;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using ProductsManagementSystem.Models.VM.Account;

namespace ProductsManagementSystem.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegisterViewModel registerViewModel);
        public Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginViewModel loginViewModel);
        public Task Logout();
    }
}

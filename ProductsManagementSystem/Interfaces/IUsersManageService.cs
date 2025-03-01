using Microsoft.AspNetCore.Identity;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Account;
using ProductsManagementSystem.Models.VM.UsersManage;

namespace ProductsManagementSystem.Interfaces
{
    public interface IUsersManageService
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetUser(string Id);
        public Task<IdentityResult> CreateUser(RegisterViewModel registerViewModel);
        public Task EditUser(EditUserViewModel editUserViewModel);
        public Task ResetPassword(string Id);
        public Task DeleteUser(string Id);
    }
}
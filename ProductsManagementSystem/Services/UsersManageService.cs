using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Account;
using ProductsManagementSystem.Models.VM.UsersManage;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Services
{

    public class UsersManageService : IUsersManageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersManageService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<IdentityResult> CreateUser(RegisterViewModel registerViewModel)
        {
            var newUser = new User
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName
            };

            var registrationResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (registrationResult.Succeeded)
            {
                if (registerViewModel.SelectedRole is null)
                    await _userManager.AddToRoleAsync(newUser, "User");

                await _userManager.AddToRoleAsync(newUser, registerViewModel.SelectedRole);
            }
            return registrationResult;
        }

        public async Task ResetPassword(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetResult = await _userManager.ResetPasswordAsync(user, resetToken, "Paroli1!");
            }
        }

        public async Task EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(editUserViewModel.Id);

            if (user != null)
            {
                user.Email = editUserViewModel.Email;
                user.UserName = user.Email;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var userDeleteResult = await _userManager.DeleteAsync(user);
        }
    }
}
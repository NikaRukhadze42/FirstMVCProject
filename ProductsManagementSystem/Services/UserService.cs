using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.User;
using System.Security.Claims;

namespace ProductsManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ChangeUserName(UserUserNameChangeViewModel userUserNameChangeViewModel)
        {
            var usernameIsTaken = await _context.Users
                .AnyAsync(x => x.UserName == userUserNameChangeViewModel.NewUserName);

            if (!usernameIsTaken)
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                {
                    var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, userUserNameChangeViewModel.Password);

                    if (passwordIsCorrect)
                    {
                        user.UserName = userUserNameChangeViewModel.NewUserName;
                        user.Email = userUserNameChangeViewModel.NewUserName;
                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }


        public async Task DeleteUser(UserDeleteViewModel userDeleteViewModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, userDeleteViewModel.Password);

                if (passwordIsCorrect)
                {
                    await _signInManager.SignOutAsync();
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task PasswordChange(UserPasswordChangeViewModel userPasswordChangeViewModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, userPasswordChangeViewModel.OldPassword);

                if (passwordIsCorrect)
                {
                    await _userManager.ChangePasswordAsync(user, userPasswordChangeViewModel.OldPassword, 
                        userPasswordChangeViewModel.NewPassword);
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task UpdatePersonalInfo(UserInfoUpdateViewModel userInfoUpdateViewModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                user.FirstName = userInfoUpdateViewModel.FirstName;
                user.LastName = userInfoUpdateViewModel.LastName;
                user.PhoneNumber = userInfoUpdateViewModel.PhoneNumber;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
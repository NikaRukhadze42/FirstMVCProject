using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.UserRole;

namespace ProductsManagementSystem.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRoleService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddRoles(UserRoleUpdateViewModel userRoleUpdateViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userRoleUpdateViewModel.UserId);
            if (user != null)
                await _userManager.AddToRolesAsync(user, userRoleUpdateViewModel.RoleList);
        }

        public async Task<List<UserRoleVisualViewModel>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (users is null)
                return new List<UserRoleVisualViewModel>();

            var usersListWithRoles = new List<UserRoleVisualViewModel>();

            for(int i = 0; i < users.Count; i++)
            {
                var userRoles = await _userManager.GetRolesAsync(users[i]);
                var viewModel = new UserRoleVisualViewModel
                {
                    UserId = users[i].Id,
                    UserName = users[i].UserName,
                    RoleList = userRoles.ToList()
                };
                usersListWithRoles.Add(viewModel);
            }
            return usersListWithRoles;
        }

        //public async Task<User> GetUser(string userId) => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        public async Task<User> GetUser(string userId) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task RemoveRoles(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
        }

        public async Task UpdateRoles(UserRoleUpdateViewModel userRoleUpdateViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userRoleUpdateViewModel.UserId);
            var userOldRoles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                await _userManager.RemoveFromRolesAsync(user, userOldRoles);
                await _userManager.AddToRolesAsync(user, userRoleUpdateViewModel.RoleList);
            }
        }
    }
}

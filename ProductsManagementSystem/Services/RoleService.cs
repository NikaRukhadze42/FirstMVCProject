using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Role;
using ProductsManagementSystem.Models.VM.UserRole;


namespace ProductsManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Add(string roleName)
        {
            var newRole = new Role()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(string Id)
        {
            var roleInDb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (roleInDb != null)
            {
                _context.Roles.Remove(roleInDb);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task UpdateRole(UpdateRoleViewModel updateRoleViewModel)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == updateRoleViewModel.Id);

            if (role != null)
            {
                role.Name = updateRoleViewModel.Name;
                role.NormalizedName = updateRoleViewModel.Name.ToUpper();

                await _roleManager.UpdateAsync(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IdentityRole> GetRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            return role;
        }
    }
}

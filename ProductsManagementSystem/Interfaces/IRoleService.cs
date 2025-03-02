using Microsoft.AspNetCore.Identity;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Role;

namespace ProductsManagementSystem.Interfaces
{
    public interface IRoleService
    {
        public Task<List<IdentityRole>> GetAllRoles();

        public Task<IdentityRole> GetRole(string Id);

        public Task Add(string roleName);

        public Task UpdateRole(UpdateRoleViewModel updateRoleViewModel);

        public Task Remove(string Id);
    }
}

using Microsoft.AspNetCore.Identity;
using ProductsManagementSystem.Models.Entities;

namespace ProductsManagementSystem.Interfaces
{
    public interface IRoleService
    {
        public Task<List<IdentityRole>> GetAllRoles();

        public Task Add(string roleName);

        public Task Remove(string Id);
    }
}

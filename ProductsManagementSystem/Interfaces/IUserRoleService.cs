using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.UserRole;

namespace ProductsManagementSystem.Interfaces
{
    public interface IUserRoleService
    {
        public Task<List<UserRoleVisualViewModel>> GetAllUsers();

        public Task UpdateRoles(UserRoleUpdateViewModel userRoleUpdateViewModel);

        public Task RemoveRoles(string userId);

        public Task AddRoles(UserRoleUpdateViewModel userRoleUpdateViewModel);

        public Task<User> GetUser(string userId);
    }
}
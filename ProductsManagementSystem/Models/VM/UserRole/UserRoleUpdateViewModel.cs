using ProductsManagementSystem.Models.Entities;

namespace ProductsManagementSystem.Models.VM.UserRole
{
    public class UserRoleUpdateViewModel
    {
        public string UserId { get; set; }
        public List<string> RoleList { get; set; }
    }
}

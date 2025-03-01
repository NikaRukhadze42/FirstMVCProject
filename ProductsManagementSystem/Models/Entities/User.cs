using Microsoft.AspNetCore.Identity;

namespace ProductsManagementSystem.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

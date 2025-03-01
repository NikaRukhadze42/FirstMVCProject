using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models.VM.UsersManage
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EmailAddress]
        public string Email { get; set; }

        public string ?PhoneNumber { get; set; }
    }
}
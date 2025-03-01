using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models.VM.User
{
    public class UserInfoUpdateViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string LastName { get; set; }

        public string ?PhoneNumber { get; set; }
    }
}

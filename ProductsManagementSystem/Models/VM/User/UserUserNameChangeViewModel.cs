using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models.VM.User
{
    public class UserUserNameChangeViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EmailAddress]
        public string NewUserName { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models.VM.User
{
    public class UserDeleteViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models.VM.User
{
    public class UserPasswordChangeViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
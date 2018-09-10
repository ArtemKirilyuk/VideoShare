using System.ComponentModel.DataAnnotations;

namespace VideoShare.PL.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [RegularExpression(@"(\w+)[@](\w+)[.](\w+)", ErrorMessage = ("Your email has wrong format"))]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords doesn't equal.")]
        [Display(Name="Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        public string Name { get; set; }
    }
}
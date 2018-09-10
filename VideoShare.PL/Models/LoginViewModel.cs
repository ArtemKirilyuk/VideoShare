using System.ComponentModel.DataAnnotations;

namespace VideoShare.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter your email.")]
        [RegularExpression(@"(\w+)[@](\w+)[.](\w+)",ErrorMessage =("Your email has wrong format"))]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
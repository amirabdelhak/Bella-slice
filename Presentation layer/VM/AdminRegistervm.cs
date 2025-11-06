using System.ComponentModel.DataAnnotations;

namespace Presentation_layer.VM
{
    public class AdminRegistervm
    {
        public string username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Phone number must be at least 3 characters long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Phone number must be at least 3 characters long.")]
        public string LastName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

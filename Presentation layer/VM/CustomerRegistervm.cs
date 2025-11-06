using System.ComponentModel.DataAnnotations;

namespace Presentation_layer.VM
{
    public class CustomerRegistervm
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = " FirstName must be at least 3 characters long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LastName must be at least 3 characters long.")]
        public string LastName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "phonenumber must be 11 number ")]
        public string phoneNumber { get; set; }
    }
}

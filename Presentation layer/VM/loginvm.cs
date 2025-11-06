using System.ComponentModel.DataAnnotations;

namespace Presentation_layer.VM
{
    public class loginvm
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}

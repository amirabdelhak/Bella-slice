using System.ComponentModel.DataAnnotations;

namespace Presentation_layer.VM
{
    public class Rolevm
    {
        [Required]
        [StringLength(20)]
        public string name { get; set; }
    }
}

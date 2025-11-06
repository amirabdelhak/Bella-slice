using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "this field is required")]
        [StringLength(50)]
        public string? ShippingAddress { get; set; }
        [Required(ErrorMessage = "this field is required")]

        public decimal TotalPrice { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }


        public virtual ICollection<productorder>? productorder { get; set; } = new List<productorder>();
    }
}

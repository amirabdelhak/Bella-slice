using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Product
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        public string name { get; set; }
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string description { get; set; }

        public decimal price { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "please enter your image")]
        public IFormFile? image { get; set; }
        public string? imagename { get; set; }
        public decimal discount { get; set; }



        [ForeignKey("Category")]
        [Required(ErrorMessage = "please enter the category if this product")]

        public int Categoryid { get; set; }
        public virtual Category? Category { get; set; }


        public virtual ICollection<CustmerProduct>? CustmerProduct { get; set; } = new List<CustmerProduct>();

        public virtual ICollection<productorder>? productorder { get; set; } = new List<productorder>();



    }
}

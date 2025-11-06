using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("Customers")]

    public class Customer:IdentityUser
    {
        public string fname { get; set; } 
        public string lname { get; set; }

        public string address { get; set; }


        public virtual ICollection<Order>? Orders { get; set; }=new List<Order>();
        public virtual ICollection<CustmerProduct>? CustmerProduct { get; set; }=new List<CustmerProduct>();






    }
}

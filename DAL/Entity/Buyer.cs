using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("Buyers")]
    public class Buyer:IdentityUser
    {
        public string fname { get; set; }
        public string lname { get; set; }

        public int age { get; set; }

        public string address { get; set; }
    }
}

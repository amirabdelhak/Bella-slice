using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Admin: IdentityUser
    {
        public string fname { get; set; }
        public string lname { get; set; }

        public string address { get; set; }
    }
}

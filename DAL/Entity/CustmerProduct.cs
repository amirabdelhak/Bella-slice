using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [PrimaryKey(nameof(customerid), nameof(Productid))]

    public class CustmerProduct
    {
        [ForeignKey("customer")]
        public string customerid { get; set; }
        public virtual Customer customer { get; set; }

        [ForeignKey("Product")]
        public int Productid { get; set; }
        public virtual Product Product { get; set; }


        public int? Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
    }
}

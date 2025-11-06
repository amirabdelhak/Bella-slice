using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OrderDTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductDTO> Products { get; set; } = new();
    }
    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageName { get; set; }
    }
}

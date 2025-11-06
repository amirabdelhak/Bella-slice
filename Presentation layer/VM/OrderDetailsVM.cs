namespace Presentation_layer.VM
{
    public class OrderDetailsVM
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }

        public List<ProductVM> Products { get; set; } = new List<ProductVM>();
    }
    public class ProductVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageName { get; set; }
    }

}

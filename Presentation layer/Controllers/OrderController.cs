using BL.manager;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Presentation_layer.VM;
using Stripe;
using System.Text.Json;

namespace Presentation_layer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdermanager ordermanager;
        private readonly iCustomermanager customermanager;
        private readonly IProductmanager productmanager;
        private readonly iProductOrdermanager productOrdermanager;

        public OrderController(IOrdermanager ordermanager,iCustomermanager customermanager,IProductmanager productmanager,iProductOrdermanager productOrdermanager)
        {
            this.ordermanager = ordermanager;
            this.customermanager = customermanager;
            this.productmanager = productmanager;
            this.productOrdermanager = productOrdermanager;
        }

        public IActionResult GetAll()
        {
            var orders = ordermanager.GetAll();
            return View(orders);
        }
        public IActionResult Getbyid(int id)
        {
            var dto = ordermanager.GetByid(id);

            if (dto == null)
                return NotFound();

            var vm = new OrderDetailsVM
            {
                OrderId = dto.OrderId,
                TotalPrice = dto.TotalPrice,
                Products = dto.Products.Select(p => new ProductVM
                {
                    Name = p.Name,
                    Price = p.Price,
                    ImageName = p.ImageName
                }).ToList()
            };

            return View(vm);
        }




        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add()
        {
            var cartCookie = Request.Cookies["cart"];
            if (string.IsNullOrEmpty(cartCookie))
            {
                return RedirectToAction("ViewCart", "Cart");
            }

            var cartProductIds = JsonSerializer.Deserialize<List<int>>(cartCookie);
            var products = productmanager.cartproducts(cartProductIds);

            if (products == null || !products.Any())
            {
                return RedirectToAction("ViewCart", "Cart");
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = customermanager.GetById(userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                ShippingAddress = user.address,
                TotalPrice = products.Sum(p => (p.price - (p.price * p.discount / 100))),
                CustomerId = userId
            };
            ordermanager.Add(order);


            foreach (var product in products)
            {
                var productOrder = new productorder
                {
                    ProductId = product.id,
                    OrderId = order.Id
                };
                productOrdermanager.Addproductorder(productOrder);
            }
            productOrdermanager.save();

            Response.Cookies.Delete("cart");

            return RedirectToAction("Getbyid",new {id = order.Id });
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var order = ordermanager.GetByid(id);
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {
                ordermanager.Update(order);
                return RedirectToAction(nameof(GetAll));
            }
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            ordermanager.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }








    }
}

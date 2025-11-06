using BL.manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation_layer.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductmanager productmanager;

        public CartController(IProductmanager productmanager)
        {
            this.productmanager = productmanager;
        }





        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cartCookie = Request.Cookies["cart"];
            List<int> cart;

            if (string.IsNullOrEmpty(cartCookie))
            {
                cart = new List<int>();
            }
            else
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartCookie);
            }
            //الجزء ده عشان ميكررش اضافه منتج هو ضايفه بالفعل 
            if (!cart.Contains(productId))
            {
                cart.Add(productId);

                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("cart", JsonSerializer.Serialize(cart), options);// الكارت هنا عباره عن جيسون يعنى شويه استرينج فلما تطلبه فى اى حته على مستوى البرنامج لازم تفكه بالجيسون بردو
            }

            return RedirectToAction("getall", "Product");
        }


        public IActionResult ViewCart()
        {
            var cartCookie = Request.Cookies["cart"];
            List<int> cart;

            if (string.IsNullOrEmpty(cartCookie))
            {
                cart = new List<int>();
            }
            else
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartCookie);
            }

            var products = productmanager.cartproducts(cart);

            return View(products);
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartCookie = Request.Cookies["cart"];
            List<int> cart;

            if (string.IsNullOrEmpty(cartCookie))
            {
                cart = new List<int>();
            }
            else
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartCookie);
            }

            if (cart.Contains(productId))
            {
                cart.Remove(productId);

                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("cart", JsonSerializer.Serialize(cart), options);
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult EmptyCart()
        {
            Response.Cookies.Delete("cart");
            return RedirectToAction("ViewCart");
        }



    }
}

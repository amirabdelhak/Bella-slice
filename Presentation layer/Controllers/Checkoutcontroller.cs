using BL.manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Text.Json;

namespace Presentation_layer.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IProductmanager productmanager;

        public CheckoutController(IConfiguration configuration,IProductmanager productmanager)
        {
            this.configuration = configuration;
            this.productmanager = productmanager;
        }
        public async Task<IActionResult> Checkout()
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

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

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var product in products)
            {
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)((product.price - (product.price * product.discount / 100)) * 100),
                        Currency = "EGP",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.name,
                        },
                    },
                    Quantity = 1,
                };

                lineItems.Add(lineItem);
            }

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                   "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://bellaslice.runasp.net/Order/Add/",
                CancelUrl = "https://bellaslice.runasp.net/Cart/ViewCart/",
            };

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);

            return Redirect(session.Url);
        }
    }
}

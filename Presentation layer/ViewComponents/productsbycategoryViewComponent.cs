using BL.manager;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_layer
{
    public class productsbycategoryViewComponent : ViewComponent
    {
        private readonly IProductmanager productmanager;
        public productsbycategoryViewComponent(IProductmanager productmanager)
        {
            this.productmanager = productmanager;
        }
        public IViewComponentResult Invoke(int categoryid)
        {
            var products = productmanager.GetProductsByCategory(categoryid);
            return View(products);
        }
    }
}

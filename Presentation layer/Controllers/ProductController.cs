using BL.manager;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation_layer.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductmanager productmanager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductmanager productmanager, IWebHostEnvironment webHostEnvironment)
        {
            this.productmanager = productmanager;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult getall()
        {
            ViewBag.Categories = productmanager.getallcategories() ?? new List<Category>();
            ViewBag.SelectedCategoryId = 0;
            var products = productmanager.getallproduct() ?? new List<Product>();
            return View(products);
        }
        public IActionResult getproductbycategory(int categoryid)
        {
            ViewBag.Categories = productmanager.getallcategories() ?? new List<Category>();
            ViewBag.SelectedCategoryId = categoryid;
            var products = productmanager.GetProductsByCategory(categoryid) ?? new List<Product>();
            return View("getall", products);

        }
        public IActionResult getbyid(int id)
        {
            var product = productmanager.getproductbyid(id);
            return View(product);
        }
        public IActionResult getbyname(string name)
        {
            var products = productmanager.getproductbyname(name);
            return View(products);
        }
        [HttpGet]
        public IActionResult create()
        {
            ViewBag.Categories = new SelectList(productmanager.getallcategories(), "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(productmanager.getallcategories(), "Id", "Name");

            if (ModelState.IsValid) {
                if (product.image != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.image.FileName);
                    string path = Path.Combine(wwwRootPath, "image", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.image.CopyToAsync(fileStream);
                    }

                    product.imagename = fileName;
                }
                productmanager.createproduct(product);
                return RedirectToAction("getall");

            }
            return View(product);

        }
        [HttpGet]
        public IActionResult update(int id)
        {
            var product = productmanager.getproductbyid(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult update(Product product)
        {
            if (ModelState.IsValid)
            {
                productmanager.updateproduct(product);
                return RedirectToAction("getall");
            }
            return View(product);
        }        
        public IActionResult delete(int id)
        {

            if (ModelState.IsValid)
            { 
                productmanager.deleteproduct(id);
                return RedirectToAction("getall");
            }
            return View();
        }
    }
}

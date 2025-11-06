using Microsoft.AspNetCore.Mvc;

namespace Presentation_layer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

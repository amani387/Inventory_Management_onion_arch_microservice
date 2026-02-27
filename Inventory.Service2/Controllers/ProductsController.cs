using Microsoft.AspNetCore.Mvc;

namespace Inventory.Service.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

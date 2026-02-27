using Microsoft.AspNetCore.Mvc;

namespace Inventory.Service.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

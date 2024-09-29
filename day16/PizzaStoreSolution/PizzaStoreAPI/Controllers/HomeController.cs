using Microsoft.AspNetCore.Mvc;

namespace PizzaStoreAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using FirstWebApp.Models;

namespace FirstWebApp.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            // Using ViewData to pass individual items
            ViewData["Customer Name"] = "Niharika Garg";
            ViewData["Customer Age"] = 21;
            ViewData["Customer"] = new Customer { Id = 1, Name = "Niharika Garg", Age = 21 };

            // Using ViewBag to pass a dynamic object
            ViewBag.Customer = new Customer { Id = 2, Name = "Luna Ng", Age = 25 };
            return View();
        }

        public IActionResult ViewCustomerData()
        {
            // Passing a Customer object directly to the view
            Customer customer = new Customer { Id = 1, Name = "Niharika Garg", Age = 21 };
            return View(customer);
        }
    }
}

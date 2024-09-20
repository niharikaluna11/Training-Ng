using FirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers

{
    public class ProductController : Controller
    {
        static List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Pencil", Quantity = 10, Price = 10, Image = "/images/1.png" },
                new Product { Id = 2, Name = "Pen", Quantity = 20, Price = 20, Image = "/images/2.png" }
            };
        public ProductController()
        {

        }

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Product product= new Product();

            return View(product);
        }

       /* [HttpPost]
        public IActionResult Create(Product product)
        {
            products.Add(product);
            
            
            return RedirectToAction("Index");
        }
*/
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Count + 1;
            product.Image = "/images/" + product.Image;
            products.Add(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int pid)
        {
            Product product = products.FirstOrDefault(p => p.Id == pid);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int pid, Product product)
        {
           
            Product oldProduct = products.FirstOrDefault(p => p.Id == pid);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Quantity = product.Quantity;
                oldProduct.Price = product.Price;

                // Check if a new image is uploaded
                if (!string.IsNullOrEmpty(product.Image))
                {
                    oldProduct.Image = "/images/" + product.Image;
                }

                // If no new image is uploaded, keep the old image
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int pid)
        {
            Product product = products.FirstOrDefault(p => p.Id == pid);
            return View(product);
        }

        [HttpPost]
        
        public IActionResult Delete(int pid, Product product)
        {
            Product productToDelete = products.FirstOrDefault(p => p.Id == pid);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                // Handle case where the product is not found
                return NotFound();
            }

            return RedirectToAction("Index");
        }



    }
}

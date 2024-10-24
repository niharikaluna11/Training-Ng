﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using FirstWebApp.Models;
using System.Collections.Generic;

namespace FirstWebApp.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>
        {
            new Product{ Id = 1, Name = "Pencil", Price = 10, Description = "Used for drawing", Image = "/images/1.png", Quantity = 10 },
            //new Product{ Id = 2, Name = "Pen", Price = 20, Description = "Used for writing", Image = "/images/2.jpg", Quantity = 20 }
        };

        public IActionResult Index()
        {
            return View(products); // Passing the list of products to the view
        }
    }
}


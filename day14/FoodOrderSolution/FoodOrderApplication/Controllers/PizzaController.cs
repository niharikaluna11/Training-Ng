﻿using FoodOrderApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodOrderApplication.Services;
using FoodOrderApplication.Interfaces;

namespace FoodOrderApplication.Controllers
{
    public class PizzaController : Controller
    {

        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        public IActionResult Index()
        {
            try
            {
                var username = HttpContext.Session.GetString("username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("UserLogin", "Login");
                }
                ViewBag.username = username;
                var pizzas = _pizzaService.GetAllPizzas();
                return View(pizzas);
            }
            catch (System.Exception e)
            {
                ViewBag.ErrorMessage = "There was an error in retrieving the pizzas ... " + e.Message;
                return View();
            }

        }


    }
}

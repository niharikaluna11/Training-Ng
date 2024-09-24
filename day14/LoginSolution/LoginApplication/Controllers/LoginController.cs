using LoginApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public ActionResult Login() { return View(); }

        [HttpPost]
        public ActionResult Login(string username,string password) 
        {
            if (_loginService.ValidateLogin(username, password))
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.Error = "Invalid credentials";
            return View();

        }

    }

}

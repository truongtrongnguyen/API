using Login_Logout_Session.Session;
using Microsoft.AspNetCore.Mvc;

namespace Login_Logout_Session.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accoutServices;

        public AccountController(IAccountServices accoutServices)
        {
            _accoutServices = accoutServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var account = _accoutServices.LogIn(username, password);
            if (account != null)
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Welcom");
            }
            else
            {
                ViewBag.Message = "Invalid Login";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Welcom()
        {
            ViewBag.Message = "Success Login";
            ViewBag.Message2 = HttpContext.Session.GetString("username");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}

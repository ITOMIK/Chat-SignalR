using Chat_SignalR.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chat_SignalR.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOptionsMonitor<AuthOptions> _authOptions;
        public AuthController(IOptionsMonitor<AuthOptions> authOptions) 
        {
            _authOptions = authOptions;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                ViewData["Username"] = User.Identity.Name ?? "User";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == false)
            {
                return View();
            }
            return View("Login");
        }
    }
}

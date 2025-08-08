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

        public IActionResult Login()
        {
            return View();
        }
    }
}

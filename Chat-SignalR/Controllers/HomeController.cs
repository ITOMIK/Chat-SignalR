using System.Diagnostics;
using Chat_SignalR.Models;
using Chat_SignalR.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chat_SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BreanchRepository breanchRepository;

        public HomeController(ILogger<HomeController> logger, BreanchRepository _breanchRepository)
        {
            _logger = logger;
            breanchRepository = _breanchRepository;
        }

        public async Task<IActionResult> Index()
        {
            var brs =await breanchRepository.GetAllBreanches();
            return View(brs);
        }

        [Authorize]
        public async Task<IActionResult> Breanch(int id)
        {
            var b = await breanchRepository.GetBreanchById(id);
            return View(b);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Diagnostics;
using Chat_SignalR.Models;
using Chat_SignalR.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chat_SignalR.Controllers
{
    [Authorize]
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
        
        public async Task<IActionResult> Breanch(int id)
        {
            var b = await breanchRepository.GetBreanchById(id);
            return View(b);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;
using SignalRSample.Models;
using System.Diagnostics;

namespace SignalRSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> hub)
        {
            _logger = logger;
            _deathlyHub = hub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if(SD.DeathyHallowRace.ContainsKey(type))
            {
                SD.DeathyHallowRace[type]++;
            }
            await _deathlyHub.Clients.All.SendAsync("updateDeathlyHallowCount", SD.DeathyHallowRace[SD.Cloak], SD.DeathyHallowRace[SD.Stone], SD.DeathyHallowRace[SD.Wand]);
            return Accepted();
        }
        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult DeathlyHallowRace()
        {
            return View();
        }

        public IActionResult HappyPotterHouse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
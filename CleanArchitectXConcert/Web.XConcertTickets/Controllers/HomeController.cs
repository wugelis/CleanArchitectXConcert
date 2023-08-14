using Application.ConcertTickets;
using EasyArchitect.OutsideManaged.AuthExtensions.Crypto;
using EasyArchitect.OutsideManaged.AuthExtensions.Models;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.XConcertTickets.Models;

namespace Web.XConcertTickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthenticateRequest authenticate)
        {
            AccountTicketAppService accountService = new AccountTicketAppService(_userService);
            authenticate.Password = Rijndael.EncryptString(authenticate.Password);
            var result = accountService.Login(authenticate).Result;
            if(result != null)
            {
                bool isLoginValid = !string.IsNullOrEmpty(result.Token);
                if (isLoginValid)
                {
                    return RedirectToAction("Privacy");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
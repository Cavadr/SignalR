using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalR.Models;
using SignalR.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Chat()
        {
            return  View();
        }


        public async Task<IActionResult> CreateUser()
        {
            var user1 = new AppUser { FullName = "Cavad",UserName="Cavad_" };
            var user2 = new AppUser { FullName = "Cavad1",UserName = "Cavad1_" };
            var user3 = new AppUser { FullName = "Cavad2",UserName = "Cavad2_" };

            await _userManager.CreateAsync(user1, "12345@Ma");
            await _userManager.CreateAsync(user2, "12345@Fa");
            await _userManager.CreateAsync(user3, "12345@Ga");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            AppUser user = await _userManager.FindByNameAsync(loginVM.UserName);

            var  result= await _signInManager.PasswordSignInAsync(user, loginVM.Password, true,true);
            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Chat));
        }

        public async Task ShowUserAlert(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            await _hubContext.Clients.Client(appUser.ConnectionId).SendAsync("ShowAlert", appUser.Id);
        }
    }
}

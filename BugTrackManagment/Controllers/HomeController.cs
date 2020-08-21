using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BugTrackManagment.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTrackManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public HomeController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
      
        [HttpGet]
        public IActionResult Index()
        {
            if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return RedirectToAction("", "Administration");
            }

            bool k = User.IsInRole("Tester");
            if (signInManager.IsSignedIn(User) && User.IsInRole("Tester"))
            {
                return RedirectToAction("index", "tester");
            }

            if (signInManager.IsSignedIn(User) && User.IsInRole("Programmer"))
            {
                return RedirectToAction("index", "programmer");
            }

            return RedirectToAction("login","Account");
        }



        public IActionResult test()
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

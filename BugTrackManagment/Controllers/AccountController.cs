using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackManagment.Models;
using BugTrackManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace BugTrackManagment.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;


        public IActionResult Index()
        {
            return View();
        }

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult xLogin(string returnUrl)
        {
           
            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl, };
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return RedirectToAction("listusers", "Administration");
            }

            if (signInManager.IsSignedIn(User) && User.IsInRole("Tester"))
            {
                return RedirectToAction("index", "tester");
            }

            if (signInManager.IsSignedIn(User) && User.IsInRole("Programmer"))
            {
                return RedirectToAction("index", "programmer");
            }

            if (signInManager.IsSignedIn(User) && User.IsInRole("Manager"))
            {
                return RedirectToAction("index", "Manager");
            }

            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl, };
            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
             
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var user = await userManager.FindByEmailAsync(model.Email);
                        
                        if (await userManager.IsInRoleAsync(user,"Programmer"))
                        {
                            return RedirectToAction("Index", "Programmer");
                        }
                        if (await userManager.IsInRoleAsync(user, "tester"))
                        {
                            return RedirectToAction("index", "tester");
                        }
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("ListRoles", "Administration");
                        }
                        if (await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return RedirectToAction("index", "Manager");
                        }
                    }
                }

                await signInManager.SignOutAsync();

                var email = userManager.FindByEmailAsync(model.Email);

                if (email.Result != null)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Password !");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No such User Exist !  ");
                    ModelState.AddModelError(string.Empty, " New User? Try Registering first ");

                }

            }

            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Department = model.Department
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, user.Department.ToString());

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }


    }
}

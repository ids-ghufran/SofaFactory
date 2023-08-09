using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SofaFactory.Data;
using SofaFactory.Helper;
using SofaFactory.Models;

namespace SofaFactory.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public IActionResult Register()
        {
            //if (User.Identity.IsAuthenticated)
            //    return Redirect("~/");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm modal)
        {
            
            var emailExist = context.AppUsers.Any(x =>  x.Email == modal.Email);
            var mobileExist = context.AppUsers.Any(x => x.PhoneNumber == modal.MobileNo);
            if (emailExist)
                ModelState.AddModelError("Email", "This email was already taken");
            if (mobileExist)
                ModelState.AddModelError("MobileNo", "This Mobile No was already taken");
            if (!ModelState.IsValid)
                return View(modal);


            var user = new AppUser
            {
                UserName = modal.Email,
                NormalizedUserName = modal.Email.ToUpper(),
                Email = modal.Email,
                NormalizedEmail = modal.Email.ToUpper(),
                PhoneNumber = modal.MobileNo
            };

            var result = await userManager.CreateAsync(user, modal.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.User);
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            return Redirect("~/");
        }

        public IActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //    return Redirect("~/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm modal)
        {
            if (!ModelState.IsValid)
                return View(modal);

            var result = await signInManager.PasswordSignInAsync(modal.Email, modal.Password, true,false);

            if (result.Succeeded)
                return Redirect("~/");
            else
                ModelState.AddModelError("Password", "Email or/and Password is wrong");

            return View(modal);
        }
    }
}
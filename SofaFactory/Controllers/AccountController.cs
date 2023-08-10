using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using SofaFactory.Data;
using SofaFactory.Helper;
using SofaFactory.Models;
using SofaFactory.Services;

namespace SofaFactory.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        private readonly IOptions<SmtpOptions> option;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IEmailService emailService,
            IOptions<SmtpOptions> option
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.emailService = emailService;
            this.option = option;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("~/");

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
            if (User.Identity.IsAuthenticated)
                return Redirect("~/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm modal)
        {
            if (!ModelState.IsValid)
                return View(modal);

            var result = await signInManager.PasswordSignInAsync(modal.Email, modal.Password, true,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Email or/and Password is wrong");
                return View(modal);
            }

            return Redirect("~/");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("Login");
        }

       
        public IActionResult ForgetPassword()
        {
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(LoginVm modal)
        {
            var user = await userManager.FindByEmailAsync(modal.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email Does't exist");
                return View();
            };
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var cbUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            try
            {
                _ = emailService.SendEmailAsyn(option.Value.UserName, modal.Email, "Reset Password", $"Click the link to reset your password: <a href='{cbUrl}'>Reset Password</a>");
                TempData["message"] = $"We sent an Email to {modal.Email} with a link to Reset Password";
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");

            }
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            var message = "Password Reset Link has expired!";
            if (user == null)
            {
                ModelState.AddModelError("UserId", "User not found");
                TempData["message"] = message;
                return View(); // Redirect to an appropriate view
            }

            var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (!result)
            {
                TempData["message"] = message;
                return View(); // Redirect to an appropriate view
            }

            var model = new ResetPasswordVm
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        TempData["resetSuccess"] = true;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["message"] = "Password Reset failed";
                        return View();
                    }
                }
            }

            // If something went wrong, return back to the view with errors
            return View(model);
        }
    }

}
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Presentation_layer.VM;

namespace Presentation_layer.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [Authorize(Roles ="admin")]
        public IActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AdminRegister(AdminRegistervm appuser)
        {
            if (ModelState.IsValid)
            {
                Admin user = new Admin();
                user.UserName = appuser.username;
                user.fname = appuser.FirstName;
                user.lname = appuser.LastName;
                user.PasswordHash = appuser.password;
                user.address = appuser.Address;
                var register = await userManager.CreateAsync(user, appuser.password);
                if (register.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in register.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(appuser);
        }



        [Authorize(Roles = "admin")]
        public IActionResult BuyerRegister()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> BuyerRegister(BuyerRegistervm appuser)
        {
            if (ModelState.IsValid)
            {
                Buyer user = new Buyer();
                user.UserName = appuser.username;
                user.fname = appuser.FirstName;
                user.lname = appuser.LastName;
                user.PasswordHash = appuser.password;
                user.address = appuser.Address;
                user.age = appuser.age;
                user.PhoneNumber= appuser.phoneNumber;
                var register = await userManager.CreateAsync(user, appuser.password);
                if (register.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "buyer");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in register.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(appuser);
        }


        public IActionResult CustomerRegister()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CustomerRegister(CustomerRegistervm appuser)
        {
            if (ModelState.IsValid)
            {
                Customer user = new Customer();
                user.UserName = appuser.username;
                user.fname = appuser.FirstName;
                user.lname = appuser.LastName;
                user.PasswordHash = appuser.password;
                user.address = appuser.Address;
                user.PhoneNumber = appuser.phoneNumber;
                var register = await userManager.CreateAsync(user, appuser.password);
                if (register.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "customer");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in register.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(appuser);
        }



        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(loginvm login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(login.username);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, login.password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("getall", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "invalid user name or password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "invalid user name or password");

                }
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("getall", "Product");
        }


        
              
    }
}




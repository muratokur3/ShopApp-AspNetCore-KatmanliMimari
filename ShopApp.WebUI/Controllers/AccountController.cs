using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // generate token
                //mail
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Invalid Registeration");
            return View(model);
        }
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var user = await _userManager.FindByNameAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid UserName");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Invalid Password");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }

}

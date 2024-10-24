using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.WebUI.EmailServices;
using ShopApp.WebUI.Extentions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{

    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ICartService _cartService;
        private IEmailSender _emailSender;



        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,ICartService cartService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
            _emailSender = emailSender;
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
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                try
                {
                    // cart objesini oluştur
                    _cartService.initializeCart(user.Id);

                    // URL oluşturma
                    var url = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        token = code
                    }, protocol: HttpContext.Request.Scheme);

                    if (url == null)
                    {
                        throw new Exception("URL oluşturulamadı.");
                    }

                    // E-posta gönderme
                    //await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayın", $"Lütfen hesabınızı onaylamak için <a href='{url}'>Linke</a> tıklayınız");
                }
                catch (Exception ex)
                {
                    // Hata loglama
                    ModelState.AddModelError("", $"E-posta gönderme hatası: {ex.Message}");
                    return View(model);
                }
                //var url = url.action("confirmemail", "account", new
                //{
                //    userıd = user.ıd,
                //    token = code
                //});
                ////mail
                //await _emailsender.sendemailasync(model.email, "hesabınızı onaylayın", $"lütfen hesabınızı onaylamak için <a href='https://localhost:7047{url}'>linke</a> tıklayınız");

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Invalid Registeration");
            return View(model);
        }
        public IActionResult Login(string returnUrl = "~/")
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
            // ullanıcının email onayı var mı?
            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //{
            //    ModelState.AddModelError("", "Please confirm your email");
            //    return View(model);
            //}
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {

                //return Redirect(model.ReturnUrl == "~/" ? "~/" : model.ReturnUrl);
                return Redirect( "~/");
            }
            ModelState.AddModelError("", "Invalid Password");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage()
            {
                Title = "Logout Session",
                Message = "Logout Session",
                AlertType = "warning"
            });

            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Invalid Token",
                    Message = "Invalid Token",
                    AlertType = "danger"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "User not found",
                    Message = "User not found",
                    AlertType = "danger"
                });
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Your email is confirmed",
                    Message = "Your email is confirmed",
                    AlertType = "success"
                });
                return View();
            }
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Invalid Email",
                    Message = "Invalid Email",
                    AlertType = "danger"
                });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "User not found",
                    Message = "User not found",
                    AlertType = "danger"
                });
                return View();
            }
            // generate token
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            //mail
            await _emailSender.SendEmailAsync(Email, "Reset Password", $"Parolayı yenilemek için <a href='https://localhost:7047{url}'>Linke</a> tıklayınız");


            return View();
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {

                return RedirectToAction("Home", "Index");
            }
            var model = new ResetPasswordModel { Token = token };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "User not found",
                    Message = "User not found",
                    AlertType = "danger"
                });
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Password reset successful",
                    Message = "Password reset successful",
                    AlertType = "successful"
                });
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}

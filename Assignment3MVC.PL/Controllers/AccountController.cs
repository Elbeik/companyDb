using Assignment3MVC.DAL.Entities;
using Assignment3MVC.PL.Helpers;
using Assignment3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Assignment3MVC.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    IsAgree = registerViewModel.IsAgree,
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password); // Pass => L8i+CzSRL@JwAj_
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
             }

            return View(registerViewModel);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginView.Email);
                if (user != null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, loginView.Password);
                    if(flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Email is not existed");
            }
            return View(loginView);
        }
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel forgetPassword)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); // token is valid for only one time for this user
                    var resetPassLink = Url.Action("ResetPassword", "Account", new {Email  = forgetPassword.Email , Token = token}, Request.Scheme);
                    var email = new Email()
                    {

                        Subject = "Reset Your Password",
                        To = user.Email,
                        Body = resetPassLink 
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email Not Existed");
            }
            return View(forgetPassword);
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;

                var user =  await _userManager.FindByEmailAsync(email);

                var result = await _userManager.ResetPasswordAsync(user, token, viewModel.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var change in result.Errors)
                    ModelState.AddModelError(string.Empty, change.Description);

            }
            return View(viewModel);

           
        }   
    
        #endregion
    }
}

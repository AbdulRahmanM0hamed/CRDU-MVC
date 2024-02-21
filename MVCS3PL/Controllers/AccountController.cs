using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using MVC_DAL.Models;
using MVCS3PL.Helpers;
using MVCS3PL.Models;
using System.Threading.Tasks;

namespace MVCS3PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signIn;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn)
		{
			_userManager = userManager;
			_signIn = signIn;
		}

		[HttpGet]
		public IActionResult Register()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationUser()
				{
					FName = model.FName,
					LName = model.LName,
					Email = model.Email,
					IsActive = model.IsActive,
					UserName = model.Email.Split('@')[0],
				};
				var Result = await _userManager.CreateAsync(User, model.Password);
				if (Result.Succeeded)
					return RedirectToAction(nameof(Login));

				foreach (var error in Result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if (flag)
					{
						var Result = await _signIn.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded)
							return RedirectToAction("Index", "Home");

					}
					ModelState.AddModelError(string.Empty, "password is Invalid");
				}
				ModelState.AddModelError(string.Empty, "password is Invalid");
			}
			return View(model);
		}



		public new async Task<IActionResult> SignOut()
		{
			await _signIn.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		// [HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)//server sied validation
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var passwordResertLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = Token }, Request.Scheme);

					var email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = passwordResertLink
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}

				ModelState.AddModelError(string.Empty, "Email is not valid");

			}
			return View(model);
		}



		public IActionResult CheckYourInbox()
		{
			return View();
		}
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string Email = TempData["email"] as string;
				string token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(Email);
				var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);


				if (result.Succeeded)
					return RedirectToAction(nameof(Login));

				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}

			return View();
		}
	}
}
//P@ssw0rd
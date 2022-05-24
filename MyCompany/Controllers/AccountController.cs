using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Models;
using MyCompany.Service;
using System.Threading.Tasks;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Authorization rules apply to this area on the site.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILoginService _loginService;

        /// <summary>
        /// Through dependency injection, we pass userManager and signInManager to operate on users in the database.
        /// </summary>
        /// <param name="userManager">Managing user.</param>
        /// <param name="signInManager">Managing user sign in.</param>
        /// <param name="loginService">The Login action.</param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILoginService loginService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loginService = loginService;
        }

        /// <summary>
        /// Login action on the site.
        /// To login on the site you need to be an anonymous user.
        /// </summary>
        /// <param name="returnUrl">The page URL to return after Logining.</param>
        /// <returns>The view of Login page.</returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        /// <summary>
        /// POST version of the Login action.
        /// </summary>
        /// <param name="model">The Login model.</param>
        /// <param name="returnUrl">The page URL to return after Logining.</param>
        /// <returns>The view of the page returnURL.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            return await _loginService.LoginAsync(model, returnUrl);
        }

        /// <summary>
        /// User Logout.
        /// </summary>
        /// <returns>The view of the main page.</returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Models;
using System.Threading.Tasks;

namespace MyCompany.Service
{
    public class LoginService : Controller, ILoginService
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Through dependency injection, we pass userManager and signInManager to operate on users in the database.
        /// </summary>
        /// <param name="userManager">Managing user.</param>
        /// <param name="signInManager">Managing user sign in.</param>
        public LoginService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// The Login action.
        /// </summary>
        /// <param name="model">The Login model.</param>
        /// <param name="returnUrl">The page URL to return after Logining.</param>
        /// <returns>The view of the page returnURL.</returns>
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            // If the user entered all form data correctly.
            if (ModelState.IsValid)
            {
                // We are trying to find a user by the login specified in the model.
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);

                // If the user is found.
                if (user != null)
                {
                    // Forced exit.
                    await _signInManager.SignOutAsync();

                    // We are trying to login with a password.
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    // If the login action was successful.
                    if (result.Succeeded)
                    {
                        // We redirect the user by returnUrl,
                        // i.e. to the point where he tried to enter the Login page,
                        // for example, from the "Contacts" page.
                        // If the value was not set, then we send it to the main page.
                        return Redirect(returnUrl ?? "/");
                    }
                }

                // If the user is not found, then the error ...
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }

            return View(model);
        }
    }
}

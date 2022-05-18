using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Models;
using System.Threading.Tasks;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Для данной области на сайте действуют правила авторизации.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Через внедрение зависимости через конструктор класса передаём
        /// userManager и signInManager, чтобы оперировать пользователями в БД.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Действие Логин на сайте.
        /// Чтобы залогиниться на сайте нужно быть анонимным пользователем.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        /// <summary>
        /// Post-версия действия Логин.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // Если пользователь ввёл все данные формы правильно.
            if (ModelState.IsValid)
            {
                // Пытаемся найти пользователя по тому логину, который указан в модели.
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);

                // Если пользователь найден.
                if (user != null)
                {
                    // Принудительно делаем выход.
                    await _signInManager.SignOutAsync();

                    // Пытаемся войти по паролю.
                    Microsoft.AspNetCore.Identity.SignInResult result = 
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    // Если действие входа оказалось успешным.
                    if (result.Succeeded)
                    {
                        // Перенаправляем пользователя по returnUrl,
                        // т.е. в ту точку, где он попытался зайти в Admin-ку,
                        // например, со страницы "Контакты".
                        // Если значение не было задано, то отправляем на главную страницу.
                        return Redirect(returnUrl ?? "/");
                    }
                }

                // Если пользователь не найден, то ошибка...
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyCompany.Models;
using System.Threading.Tasks;

namespace MyCompany.Service
{
    public interface ILoginService
    {
        /// <summary>
        /// The Login action.
        /// </summary>
        /// <param name="model">The Login model.</param>
        /// <param name="returnUrl">The page URL to return after Logining.</param>
        /// <returns>The view of the page returnURL.</returns>
        public Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl);
    }
}

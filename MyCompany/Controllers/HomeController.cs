using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// A field for access to our domain model, to domain objects, to our database.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// A class constructor.
        /// </summary>
        /// <param name="dataManager">The domain model.</param>
        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Action for the main page.
        /// </summary>
        /// <returns>The view of the main page.</returns>
        public IActionResult Index()
        {
            return View(_dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }

        /// <summary>
        /// Action for the contact page.
        /// </summary>
        /// <returns>The view of the contact page.</returns>
        public IActionResult Contacts()
        {
            return View(_dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        /// Action to get the service list view.
        /// </summary>
        /// <returns>The view of the service list.</returns>
        public IActionResult Index()
        {
            return View(_dataManager.ServiceItems.GetServiceItems());
        }
    }
}
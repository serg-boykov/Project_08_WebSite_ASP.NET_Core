using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System;

namespace MyCompany.Controllers
{
    public class ServicesController : Controller
    {
        /// <summary>
        /// A field for access to our domain model, to domain objects, to our database.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// A class constructor.
        /// </summary>
        /// <param name="dataManager">The domain model.</param>
        public ServicesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Action for the service page.
        /// </summary>
        /// <param name="id">The service identifier.</param>
        /// <returns>The view of the service page.</returns>
        public IActionResult Index(Guid id)
        {
            // The view of the service by ID.
            if (id != default)
            {
                return View("Show", _dataManager.ServiceItems.GetServiceItemById(id));
            }

            // If there is no identifier, then a combined model.

            // Via Viewbag transfers the PageServices text field.
            ViewBag.TextField = _dataManager.TextFields.GetTextFieldByCodeWord("PageServices");

            // Full list of services.
            return View(_dataManager.ServiceItems.GetServiceItems());
        }
    }
}

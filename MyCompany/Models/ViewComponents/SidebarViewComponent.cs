using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System.Threading.Tasks;

namespace MyCompany.Models.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        /// <summary>
        /// A field for access to our domain model, to domain objects, to our database.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// A class constructor.
        /// </summary>
        /// <param name="dataManager">The domain model.</param>
        public SidebarViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// The asynchronous task that returns the view of the list of all services named "Default".
        /// </summary>
        /// <returns>The view of the list of all services.</returns>
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View("Default", _dataManager.ServiceItems.GetServiceItems()));
        }
    }
}

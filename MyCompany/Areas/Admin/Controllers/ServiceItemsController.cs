using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System;
using System.IO;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for editing services on the site.
    /// </summary>
    [Area("Admin")]
    public class ServiceItemsController : Controller
    {
        /// <summary>
        /// A field for access to our domain model, to domain objects, to our database.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Hosting environment for saving title images of services.
        /// </summary>
        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// A class constructor.
        /// </summary>
        /// <param name="dataManager">The domain model.</param>
        /// <param name="hostEnvironment">The hosting environment.</param>
        public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostEnvironment)
        {
            _dataManager = dataManager;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Editing a service by ID.
        /// </summary>
        /// <param name="id">The service identifier.</param>
        /// <returns>The service view with id.</returns>
        public IActionResult Edit(Guid id)
        {
            // If there is no such service, then add a new one, otherwise a service from the database.
            var entity = id == default ? new ServiceItem() : _dataManager.ServiceItems.GetServiceItemById(id);

            // The service view to edit.
            return View(entity);
        }

        /// <summary>
        /// Action after editing the service and sending the form to the server.
        /// </summary>
        /// <param name="model">The model passed from html-form.</param>
        /// <param name="titleImageFile">The title picture.</param>
        /// <returns>The service view to edit.</returns>
        [HttpPost]
        public IActionResult Edit(ServiceItem model, IFormFile titleImageFile)
        {
            // If the model is without errors.
            if (ModelState.IsValid)
            {
                // If there is a title image (this does not check that the file type is an image).
                if (titleImageFile != null)
                {
                    // The name of the picture in the model as the name of this file.
                    model.TitleImagePath = titleImageFile.FileName;

                    // Creating an image in the wwwroot directory and copying...
                    string path = Path.Combine(_hostEnvironment.WebRootPath, "images/", titleImageFile.FileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    titleImageFile.CopyTo(stream);
                }

                // Saving the service in the database.
                _dataManager.ServiceItems.SaveServiceItem(model);

                // Redirecting the user to the main admin page.
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            // The service view to edit.
            return View(model);
        }

        /// <summary>
        /// Removing the service by ID.
        /// </summary>
        /// <param name="id">The service identifier.</param>
        /// <returns>The view of the service list.</returns>
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _dataManager.ServiceItems.DeleteServiceItem(id);

            // Redirecting the user to the view of the service list.
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }

    }
}

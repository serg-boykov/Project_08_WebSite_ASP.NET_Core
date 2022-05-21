using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TextFieldsController : Controller
    {
        /// <summary>
        /// A field for access to our domain model, to domain objects, to our database.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// A class constructor.
        /// </summary>
        /// <param name="dataManager">The domain model.</param>
        public TextFieldsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Page editing by code word.
        /// </summary>
        /// <param name="codeWord">The code word of the page.</param>
        /// <returns>The view of the text field.</returns>
        public IActionResult Edit(string codeWord)
        {
            // Getting the textField entity according to the code word.
            var entity = _dataManager.TextFields.GetTextFieldByCodeWord(codeWord);

            // The view of the textField entity.
            return View(entity);
        }

        /// <summary>
        /// Action after editing the page and saving the form.
        /// </summary>
        /// <param name="model">The model passed from html-form.</param>
        /// <returns>The view of the service list.</returns>
        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            // If the model is without errors.
            if (ModelState.IsValid)
            {
                // We save the model in the database.
                _dataManager.TextFields.SaveTextField(model);

                // We redirect the user to the HomeController with the "Index" action in the "Admin"-area:
                // The view of the service list.
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            
            // The view of the model page.
            return View(model);
        }
    }
}

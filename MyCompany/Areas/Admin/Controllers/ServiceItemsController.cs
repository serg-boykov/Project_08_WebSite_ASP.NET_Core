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
    /// Контроллер для редактирования услуг на сайте.
    /// </summary>
    [Area("Admin")]
    public class ServiceItemsController : Controller
    {
        /// <summary>
        /// Поле для доступа к нашей доменной модели, к доменным объектам, к нашей БД.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Окружение хостинга для сохранения титульных картинок услуг.
        /// </summary>
        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="dataManager"></param>
        /// <param name="hostEnvironment"></param>
        public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostEnvironment)
        {
            _dataManager = dataManager;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Поиск услуги в БД по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <returns></returns>
        public IActionResult Edit(Guid id)
        {
            // Если нет такой услуги, то добавить новую, иначе из услугу из БД.
            var entity = id == default ? new ServiceItem() : _dataManager.ServiceItems.GetServiceItemById(id);
            
            // Для редактирования и создания записи служит одно представление.
            return View(entity);
        }

        /// <summary>
        /// Действие после редактирования услуги и сохранение формы.
        /// </summary>
        /// <param name="model">Модель, переданная с html-формы.</param>
        /// <param name="titleImageFile">Титульная картинка.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(ServiceItem model, IFormFile titleImageFile)
        {
            // Если модель без ошибок.
            if (ModelState.IsValid)
            {
                // Если есть титульная картинка (здесь не проверяется что это точно картинка).
                if (titleImageFile != null)
                {
                    // Название картинки, которое будет в моделе, как название этого файла.
                    model.TitleImagePath = titleImageFile.FileName;

                    // Создаём картинку в директории wwwroot и копируем...
                    using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }

                // Сохраняем услугу в БД.
                _dataManager.ServiceItems.SaveServiceItem(model);

                // Перенаправляем пользователя на главную страницу в Admin-ке.
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _dataManager.ServiceItems.DeleteServiceItem(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }

    }
}

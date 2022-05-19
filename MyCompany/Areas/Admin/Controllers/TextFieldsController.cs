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
        /// Поле для доступа к нашей доменной модели, к доменным объектам, к нашей БД.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="dataManager"></param>
        public TextFieldsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Редактирование страницы по кодовому слову.
        /// </summary>
        /// <param name="codeWord"></param>
        /// <returns></returns>
        public IActionResult Edit(string codeWord)
        {
            // Получение текстового поля по кодовому слову.
            var entity = _dataManager.TextFields.GetTextFieldByCodeWord(codeWord);
            
            // Передаём текстовое поле в представление.
            return View(entity);
        }

        /// <summary>
        /// Действие после редактирования страницы и сохранение формы.
        /// </summary>
        /// <param name="model">Модель, переданная с html-формы.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            // Если модель без ошибок.
            if (ModelState.IsValid)
            {
                // Сохраняем модель в БД.
                _dataManager.TextFields.SaveTextField(model);

                // Перенаправляем пользователя на HomeController c действием "Index" в "Admin"-area.
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            
            return View(model);
        }
    }
}

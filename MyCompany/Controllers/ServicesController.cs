using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System;

namespace MyCompany.Controllers
{
    public class ServicesController : Controller
    {
        /// <summary>
        /// Поле для доступа к нашей доменной модели, к доменным объектам, к нашей БД.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="dataManager"></param>
        public ServicesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Действие для страницы Наши Услуги.
        /// </summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <returns>Представление для страницы услуг.</returns>
        public IActionResult Index(Guid id)
        {
            // Если передаётся конкретный идентификатор услуги,
            // то представление этой услуги.
            if (id != default)
            {
                return View("Show", _dataManager.ServiceItems.GetServiceItemById(id));
            }

            // Если идентификатор услуги НЕ передаётся (пустой), то комбинированная модель.

            // Через ViewBag передаётся текстовое поле PageServices.
            ViewBag.TextField = _dataManager.TextFields.GetTextFieldByCodeWord("PageServices");

            // Полный перечень услуг.
            return View(_dataManager.ServiceItems.GetServiceItems());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Поле для доступа к нашей доменной модели, к доменным объектам, к нашей БД.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="dataManager"></param>
        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Действие для главной страницы.
        /// </summary>
        /// <returns>Представление для главной страницы.</returns>
        public IActionResult Index()
        {
            return View(_dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }

        /// <summary>
        /// Действие для страницы контактов.
        /// </summary>
        /// <returns>Представление для страницы контактов.</returns>
        public IActionResult Contacts()
        {
            return View(_dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
    }
}

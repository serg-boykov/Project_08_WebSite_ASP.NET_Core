using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult Index()
        {
            return View(_dataManager.ServiceItems.GetServiceItems());
        }
    }
}
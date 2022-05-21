using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System.Threading.Tasks;

namespace MyCompany.Models.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        /// <summary>
        /// Поле для доступа к нашей доменной модели, к доменным объектам, к нашей БД.
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="dataManager"></param>
        public SidebarViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Асинхронная задача, возвращающая представление списка всех услуг по имени "Default".
        /// </summary>
        /// <returns>Представление списка всех услуг.</returns>
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View("Default", _dataManager.ServiceItems.GetServiceItems()));
        }
    }
}

using MyCompany.Domain.Entities;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для наших услуг.
    /// </summary>
    public interface IServiceItemsRepository
    {
        /// <summary>
        /// Сделать выборку всех наших услуг.
        /// </summary>
        /// <returns></returns>
        IQueryable<ServiceItem> GetServiceItems();

        /// <summary>
        /// Выбрать услугу по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceItem GetServiceItemById(Guid id);

        /// <summary>
        /// Сохранить изменения услуги в базу данных.
        /// </summary>
        /// <param name="serviceItem"></param>
        void SaveServiceItem(ServiceItem serviceItem);

        /// <summary>
        /// Удалить услугу.
        /// </summary>
        /// <param name="id"></param>
        void DeleteServiceItem(Guid id);
    }
}

using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс реализации EF интерфейса IServiceItemsRepository
    /// для услуг.
    /// </summary>
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        /// <summary>
        /// Поле для связи объектов нашего сайта с базой данных.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Внедрение зависимости связи с базой данных через конструктор.
        /// </summary>
        /// <param name="context"></param>
        public EFServiceItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выбираем все записи из таблицы ServiceItem.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ServiceItem> GetServiceItems()
        {
            return _context.ServiceItems;
        }

        /// <summary>
        /// Выбираем одну запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceItem GetServiceItemById(Guid id)
        {
            return _context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Сохраняем изменения в базе данных после добавления записи или её изменения.
        /// </summary>
        /// <param name="serviceItem"></param>
        public void SaveServiceItem(ServiceItem serviceItem)
        {
            if (serviceItem.Id == default)
            {
                _context.Entry(serviceItem).State = EntityState.Added;
            }
            else
            {
                _context.Entry(serviceItem).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляем запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteServiceItem(Guid id)
        {
            _context.ServiceItems.Remove(new ServiceItem() { Id = id });
            _context.SaveChanges();
        }

    }
}

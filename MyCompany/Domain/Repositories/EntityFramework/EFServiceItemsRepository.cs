using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// EF implementation class 
    /// of the IServiceItemsRepository interface for services.
    /// </summary>
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        /// <summary>
        /// A field for linking the objects of our site with the database.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Dependency injection for linking with the database through the constructor.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EFServiceItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Select all records from the ServiceItem table.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ServiceItem> GetServiceItems()
        {
            return _context.ServiceItems;
        }

        /// <summary>
        /// Select one record by ID.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <returns>The record in the database.</returns>
        public ServiceItem GetServiceItemById(Guid id)
        {
            return _context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// We save changes in the database after adding a record or changing it.
        /// </summary>
        /// <param name="serviceItem">The record in the database.</param>
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
        /// Delete record by ID.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        public void DeleteServiceItem(Guid id)
        {
            _context.ServiceItems.Remove(new ServiceItem() { Id = id });
            _context.SaveChanges();
        }

    }
}

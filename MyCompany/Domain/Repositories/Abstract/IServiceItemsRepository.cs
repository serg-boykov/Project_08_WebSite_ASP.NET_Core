using MyCompany.Domain.Entities;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Interface for our services.
    /// </summary>
    public interface IServiceItemsRepository
    {
        /// <summary>
        /// Make a selection of all our services.
        /// </summary>
        /// <returns>The all our services.</returns>
        IQueryable<ServiceItem> GetServiceItems();

        /// <summary>
        /// Select a service by ID.
        /// </summary>
        /// <param name="id">The service identifier.</param>
        /// <returns>The service by ID.</returns>
        ServiceItem GetServiceItemById(Guid id);

        /// <summary>
        /// Save service changes to the database.
        /// </summary>
        /// <param name="serviceItem">The company service.</param>
        void SaveServiceItem(ServiceItem serviceItem);

        /// <summary>
        /// Delete the service by ID.
        /// </summary>
        /// <param name="id">The service identifier.</param>
        void DeleteServiceItem(Guid id);
    }
}

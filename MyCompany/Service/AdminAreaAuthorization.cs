using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Linq;

namespace MyCompany.Service
{
    /// <summary>
    /// A class for creating a convention 
    /// by implementing the IControllerModelConvention interface.
    /// </summary>
    public class AdminAreaAuthorization : IControllerModelConvention
    {
        private readonly string _area;
        private readonly string _policy;

        public AdminAreaAuthorization(string area, string policy)
        {
            _area = area;
            _policy = policy;
        }

        /// <summary>
        /// For the controller, we check its attributes. 
        /// If there is an _area attribute, 
        /// then we add a filter for this controller to AuthorizeFilter(), 
        /// i.e. send the user for authorization.
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a =>
                a is AreaAttribute && (a as AreaAttribute).RouteValue.Equals(_area, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(r =>
                r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) && r.Value.Equals(_area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(_policy));
            }

        }
    }
}

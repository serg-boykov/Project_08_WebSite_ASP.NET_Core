using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Linq;

namespace MyCompany.Service
{
    /// <summary>
    /// Класс для создания соглашения (Convention),
    /// реализуя интерфейс IControllerModelConvention.
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
        /// Для контроллера проверяем его атрибуты.
        /// Если есть атрибут _area, то добавляем фильтр для данного контроллера
        /// в AuthorizeFilter(),
        /// т.е. отправляем пользователя на авторизацию.
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

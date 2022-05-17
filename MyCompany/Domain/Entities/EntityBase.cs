using System;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    /// <summary>
    /// Абстрактный базовый класс для всех сущностей.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// При создании объекта сразу присваивается значение свойству DateAdded.
        /// </summary>
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        /// <summary>
        /// Обязательный первичный ключ типа Guid.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Название, например, услуги компании.
        /// </summary>
        [Display(Name = "Title (headline)")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Краткое описание, например, услуги компании.
        /// </summary>
        [Display(Name = "Short description")]
        public virtual string Subtitle { get; set; }

        /// <summary>
        /// Полное описание, например, услуги компании.
        /// </summary>
        [Display(Name = "Full description")]
        public virtual string Text { get; set; }

        /// <summary>
        /// Картинка, например, для услуги компании.
        /// </summary>
        [Display(Name = "Title picture")]
        public virtual string TitleImagePath { get; set; }

        /// <summary>
        /// Значение для метатега Title.
        /// </summary>
        [Display(Name = "SEO metateg Title")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Значение для метатега Description.
        /// </summary>
        [Display(Name = "SEO metateg Description")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Значение для метатега Keywords.
        /// </summary>
        [Display(Name = "SEO metateg Keywords")]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Дата создания сущности.
        /// </summary>
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}

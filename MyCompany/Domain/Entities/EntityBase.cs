using System;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    /// <summary>
    /// Abstract basic class for all entities.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// When creating an object, 
        /// the value of the DateAdeded property is immediately assigned.
        /// </summary>
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        /// <summary>
        /// Required primary key of type Guid.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The name, for example, the services of the company.
        /// </summary>
        [Display(Name = "Title (headline)")]
        public virtual string Title { get; set; }

        /// <summary>
        /// A brief description of, for example, the services of a company.
        /// </summary>
        [Display(Name = "Short description")]
        public virtual string Subtitle { get; set; }

        /// <summary>
        /// Full description, for example, the services of the company.
        /// </summary>
        [Display(Name = "Full description")]
        public virtual string Text { get; set; }

        /// <summary>
        /// The title picture, for example, for the service of the company.
        /// </summary>
        [Display(Name = "Title picture")]
        public virtual string TitleImagePath { get; set; }

        /// <summary>
        /// The value for the Title meta tag.
        /// </summary>
        [Display(Name = "SEO metateg Title")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// The value for the Description meta tag.
        /// </summary>
        [Display(Name = "SEO metateg Description")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// The value for the Keywords meta tag.
        /// </summary>
        [Display(Name = "SEO metateg Keywords")]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// The date of creating entity.
        /// </summary>
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}

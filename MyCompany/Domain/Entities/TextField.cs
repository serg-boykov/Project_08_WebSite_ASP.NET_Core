using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    /// <summary>
    /// Class of the Text Field entity.
    /// </summary>
    public class TextField : EntityBase
    {
        /// <summary>
        /// Required keyword to access the Text Field entity.
        /// </summary>
        [Required]
        public string CodeWord { get; set; }

        /// <summary>
        /// Page title override.
        /// </summary>
        [Display(Name = "Название страницы (заголовок)")]
        public override string Title { get; set; } = "Информационная страница";

        /// <summary>
        /// Redefining page content.
        /// </summary>
        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Содержание заполняется администратором";
    }
}

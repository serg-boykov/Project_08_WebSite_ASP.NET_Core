using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    /// <summary>
    /// Класс Текстовое поле.
    /// </summary>
    public class TextField : EntityBase
    {
        /// <summary>
        /// Обязательное ключевое слово для обращения к текстовому полю.
        /// </summary>
        [Required]
        public string CodeWord { get; set; }

        /// <summary>
        /// Переопределение названия страницы.
        /// </summary>
        [Display(Name = "Название страницы (заголовок)")]
        public override string Title { get; set; } = "Информационная страница";

        /// <summary>
        /// Переопределение содержания страницы.
        /// </summary>
        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Содержание заполняется администратором";
    }
}

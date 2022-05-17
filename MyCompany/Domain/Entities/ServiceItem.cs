using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    public class ServiceItem : EntityBase
    {
        /// <summary>
        /// Обязательное название услуги.
        /// </summary>
        [Required(ErrorMessage = "Заполните название услуги")]
        [Display(Name = "Название услуги")]
        public override string Title { get; set; }

        /// <summary>
        /// Переопределение краткого описания услуги.
        /// </summary>
        [Display(Name = "Краткое описание услуги")]
        public override string Subtitle { get; set; }

        /// <summary>
        /// Переопределение полного описания услуги.
        /// </summary>
        [Display(Name = "Полное описание услуги")]
        public override string Text { get; set; }
    }
}

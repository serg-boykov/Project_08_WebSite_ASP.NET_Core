using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    /// <summary>
    /// The company services.
    /// </summary>
    public class ServiceItem : EntityBase
    {
        /// <summary>
        /// Required service name.
        /// </summary>
        [Required(ErrorMessage = "Заполните название услуги")]
        [Display(Name = "Название услуги")]
        public override string Title { get; set; }

        /// <summary>
        /// Redefining the short description of the service.
        /// </summary>
        [Display(Name = "Краткое описание услуги")]
        public override string Subtitle { get; set; }

        /// <summary>
        /// Redefining the full description of the service.
        /// </summary>
        [Display(Name = "Полное описание услуги")]
        public override string Text { get; set; }
    }
}

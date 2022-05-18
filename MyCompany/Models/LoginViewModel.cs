using System.ComponentModel.DataAnnotations;

namespace MyCompany.Models
{
    /// <summary>
    /// Класс для ввода пользователем логина и пароля.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

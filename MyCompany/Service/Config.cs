namespace MyCompany.Service
{
    /// <summary>
    /// Класс-обёртка для файла appsettings.json
    /// </summary>
    public class Config
    {
        // Каждое свойство этого класса соотвествует
        // настройкам в файле appsettings.json

        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Имя компании.
        /// </summary>
        public static string CompanyName { get; set; }

        /// <summary>
        /// Телефон компании.
        /// </summary>
        public static string CompanyPhone { get; set; }

        /// <summary>
        /// Телефон компании в краткй форме для использования его по ссылке для дозвона.
        /// </summary>
        public static string CompanyPhoneShort { get; set; }

        /// <summary>
        /// Почтовый адрес компании.
        /// </summary>
        public static string CompanyEmail { get; set; }
    }
}

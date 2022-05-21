namespace MyCompany.Service
{
    /// <summary>
    /// Wrapper class for appsettings.json file
    /// </summary>
    public class Config
    {
        // Each property of this class corresponds
        // to the settings in the appsettings.json file

        /// <summary>
        /// Database connection string.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Name of the company.
        /// </summary>
        public static string CompanyName { get; set; }

        /// <summary>
        /// The company's phone.
        /// </summary>
        public static string CompanyPhone { get; set; }

        /// <summary>
        /// Phone number of the company in short form for using it on the link for dialing.
        /// </summary>
        public static string CompanyPhoneShort { get; set; }

        /// <summary>
        /// Company postal address.
        /// </summary>
        public static string CompanyEmail { get; set; }
    }
}

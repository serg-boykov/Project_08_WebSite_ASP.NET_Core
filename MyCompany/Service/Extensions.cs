namespace MyCompany.Service
{
    public static class Extensions
    {
        /// <summary>
        /// The method removes the word "Controller" from the string.
        /// </summary>
        /// <param name="str">The string to remove the word "Controller".</param>
        /// <returns>A string without the word "Controller".</returns>
        public static string CutController(this string str)
        {
            return str.Replace("Controller", "");
        }
    }
}

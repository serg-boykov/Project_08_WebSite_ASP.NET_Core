using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    /// <summary>
    /// Helper class for managing text fields and services, as well as operations on them.
    /// </summary>
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldsRepository textFieldsRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}

using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    /// <summary>
    /// Класс-помощник для управления текстовыми полями и услугами,
    /// а также операциями над ними.
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

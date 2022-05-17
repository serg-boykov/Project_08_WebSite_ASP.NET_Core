using MyCompany.Domain.Entities;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для текстового поля.
    /// </summary>
    public interface ITextFieldsRepository
    {
        /// <summary>
        /// Сделать выборку всех текстовых полей.
        /// </summary>
        /// <returns></returns>
        IQueryable<TextField> GetTextFields();

        /// <summary>
        /// Выбрать текстовое поле по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TextField GetTextFieldById(Guid id);

        /// <summary>
        /// Выбрать текстовое поле по кодовому слову.
        /// </summary>
        /// <param name="codeWord"></param>
        /// <returns></returns>
        TextField GetTextFieldByCodeWord(string codeWord);

        /// <summary>
        /// Сохранить изменения текстового поля в базу данных.
        /// </summary>
        /// <param name="textField"></param>
        void SaveTextField(TextField textField);

        /// <summary>
        /// Удалить текстовое поле.
        /// </summary>
        /// <param name="id"></param>
        void DeleteTextField(Guid id);
    }
}

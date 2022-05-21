using MyCompany.Domain.Entities;
using System;
using System.Linq;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Interface for the Text Field entity.
    /// </summary>
    public interface ITextFieldsRepository
    {
        /// <summary>
        /// Make a selection of all text fields.
        /// </summary>
        /// <returns></returns>
        IQueryable<TextField> GetTextFields();

        /// <summary>
        /// Select the text field by ID.
        /// </summary>
        /// <param name="id">The Text Field identifier.</param>
        /// <returns>The text field by ID.</returns>
        TextField GetTextFieldById(Guid id);

        /// <summary>
        /// Select the text field by code word.
        /// </summary>
        /// <param name="codeWord">The code word of the Text Field.</param>
        /// <returns>The Text Field by code word.</returns>
        TextField GetTextFieldByCodeWord(string codeWord);

        /// <summary>
        /// Save changes to the text field to the database.
        /// </summary>
        /// <param name="textField"></param>
        void SaveTextField(TextField textField);

        /// <summary>
        /// Delete the Text Field by ID.
        /// </summary>
        /// <param name="id">The Text Field identifier.</param>
        void DeleteTextField(Guid id);
    }
}

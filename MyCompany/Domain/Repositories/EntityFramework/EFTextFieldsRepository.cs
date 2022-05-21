using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// The EF implementation class 
    /// of the ITextFieldRepository interface for text fields.
    /// </summary>
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        /// <summary>
        /// A field for linking the objects of our site with the database.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Dependency injection for linking with the database through the constructor.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EFTextFieldsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Select all records from the TextField table.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TextField> GetTextFields()
        {
            return _context.TextFields;
        }

        /// <summary>
        /// Select one record by ID.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <returns>The record in the database.</returns>
        public TextField GetTextFieldById(Guid id)
        {
            return _context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// We select one record by the keyword codeWord.
        /// </summary>
        /// <param name="codeWord">The keyword codeWord.</param>
        /// <returns>The record in the database.</returns>
        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return _context.TextFields.FirstOrDefault(x =>x.CodeWord == codeWord);
        }

        /// <summary>
        /// We save changes in the database after adding a record or changing it.
        /// </summary>
        /// <param name="textField">The record in the database.</param>
        public void SaveTextField(TextField textField)
        {
            if (textField.Id == default)
            {
                _context.Entry(textField).State = EntityState.Added;
            }
            else
            {
                _context.Entry(textField).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Delete record by ID.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        public void DeleteTextField(Guid id)
        {
            _context.TextFields.Remove(new TextField() { Id = id });
            _context.SaveChanges();
        }
    }
}

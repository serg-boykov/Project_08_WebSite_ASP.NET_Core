using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс реализации EF интерфейса ITextFieldRepository
    /// для текстовых полей.
    /// </summary>
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        /// <summary>
        /// Поле для связи объектов нашего сайта с базой данных.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Внедрение зависимости связи с базой данных через конструктор.
        /// </summary>
        /// <param name="context"></param>
        public EFTextFieldsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выбираем все записи из таблицы TextField.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TextField> GetTextFields()
        {
            return _context.TextFields;
        }

        /// <summary>
        /// Выбираем одну запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TextField GetTextFieldById(Guid id)
        {
            return _context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Выбираем одну запись по ключевому слову codeWord.
        /// </summary>
        /// <param name="codeWord"></param>
        /// <returns></returns>
        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return _context.TextFields.FirstOrDefault(x =>x.CodeWord == codeWord);
        }

        /// <summary>
        /// Сохраняем изменения в базе данных после добавления записи или её изменения.
        /// </summary>
        /// <param name="textField"></param>
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
        /// Удаляем запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTextField(Guid id)
        {
            _context.TextFields.Remove(new TextField() { Id = id });
            _context.SaveChanges();
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Contacts.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Класс списка контактов (эмуляция)
    /// </summary>
    public class ContactList: List<Contact>
    {
        /// <summary>
        /// Возвращет Contact из списка по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact ById(int id)
        {
            return (from e in this where e.Id == id select e).First();
        }

        /// <summary>
        /// Возвращет Contact из списка по его ID
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact ById(Contact contact)
        {
            return ById(contact.Id);
        }

        /// <summary>
        /// Создадим список контактов и наполним его тестовыми данными
        /// </summary>
        private static ContactList FromResources()
        {
            var result = new ContactList();
            var i = 1;
            while (true)
            {
                var c = Contact.FromResource(i);
                if (!c.IsValid)
                    break;
                result.Add(c);
                i++;
            }
            return result;
        }

        private static ContactList _contacts;

        /// <summary>
        /// Экземпляр списка контактов
        /// </summary>
        public static ContactList Contacts => _contacts ?? (_contacts = FromResources());
    }
}
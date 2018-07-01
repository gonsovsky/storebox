using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using Contacts.Helpers;
using Contacts.Properties;

namespace Contacts.Models
{
    /// <summary>
    /// Класс карточки контакта
    /// </summary>
    public class Contact
    {
        [Required] public int Id { get; set; }

        [Required] public string Fio { get; set; }

        /// <summary>
        /// Представление телефона в приведенном числовом виде для хранения
        /// </summary>
        [Required]
        [ScriptIgnore]
        [CustomValidation(typeof(Validation), "ValidatePhone")]
        public long Phone { get; set; }

        /// <summary>
        /// Форматированное представление RU мобильноготелефона
        /// </summary>
        [Required]
        [CustomValidation(typeof(Validation), "ValidatePhone")]
        public string HumanizedPhone
        {
            get => $"{Phone:+#-###-###-##-##}";
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                Phone = long.Parse(new string(value.Where(char.IsDigit).ToArray()));
            }
        }

        private byte[] _photo;

        /// <summary>
        /// Фото
        /// </summary>
        [ScriptIgnore]
        public byte[] Photo
        {
            get => _photo ?? DefaultPhoto;
            set => _photo = value;
        }

        /// <summary>
        /// Возвращает Http ссылку на фотографию
        /// </summary>
        public string PhotoLink => $"/contact/GetPhoto/{Id}?{Environment.TickCount}";

        /// <summary>
        /// Объект валидирован
        /// </summary>
        [ScriptIgnore]
        public bool IsValid
        {
            get
            {
                var results = new List<ValidationResult>();
                return Validator.TryValidateObject(
                    this,
                    new ValidationContext(this, null, null),
                    results,
                    true);
            }
        }

        /// <summary>
        /// Создаст экземпляр контакта из ресурсов
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Contact FromResource(int index)
        {
            return new Contact
            {
                Id = index,
                Fio = Resources.ResourceManager.GetString($"Fio{index}"),
                HumanizedPhone = Resources.ResourceManager.GetString($"Phone{index}"),
                Photo = ((Bitmap)Resources.ResourceManager.GetObject($"Photo{index}")).ToByteArray() ?? DefaultPhoto
            };
        }

        private static byte[] _defaultPhoto;

        /// <summary>
        /// Фотография по умолчанию
        /// </summary>
        protected static byte[] DefaultPhoto => _defaultPhoto ?? (_defaultPhoto = Resources.DefaultPhoto.ToByteArray());
    }
}

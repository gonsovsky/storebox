using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Contacts.Helpers
{
    /// <summary>
    /// Класс - валидатор общепринятых величин
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Проверить "строковое человеческое" или "приведенное цифровое" представление моб. телефона к плану нумерации (RU)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ValidationResult ValidatePhone(object value, ValidationContext context)
        {
            if (value==null)
                return new ValidationResult(InvalidRuMobileHint);

            //представлен числом
            if (value is long val)
            {
                if (val >= 79000000000 && val <= 79999999999)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(InvalidRuMobileHint);
            }
            //представлен cтрокой
            return long.TryParse(new string(value.ToString().Where(char.IsDigit).ToArray()),out val) ? ValidatePhone(val, context) : new ValidationResult(InvalidRuMobileHint);
        }

        protected const string InvalidRuMobileHint = "Телефон должен быть в формате: +7-9XX-XXX-XX-XX";
    }
}
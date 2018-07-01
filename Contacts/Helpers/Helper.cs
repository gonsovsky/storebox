using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Contacts.Helpers
{
    /// <summary>
    /// Класс расширений
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Копирует "открытые" свойства объекта в другой
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void Assign<T, TU>(this T dest, TU source)
        {
            var sourceProps = source.GetVisibleProperties();
            var destProps = dest.GetVisibleProperties();
            foreach (var sourceProp in sourceProps)
            {
                if (destProps.All(x => x.Name != sourceProp.Name)) continue;
                var p = destProps.First(x => x.Name == sourceProp.Name);
                if (p.CanWrite)
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Возвращает список "открытых" для копирования свойств объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetVisibleProperties(this object obj)
        {
            return obj.GetType().GetProperties()
                .Where(
                    x => x.CanWrite &&
                         x.GetCustomAttributes(typeof(ScriptIgnoreAttribute), false).Length == 0)
                .ToList();
        }

        /// <summary>
        /// Представляет картинку из ресурсов в виде массива байтов для браузера
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Bitmap bmp)
        {
            if (bmp is null)
                return new byte[0];
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
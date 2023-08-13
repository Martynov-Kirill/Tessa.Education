using System.ComponentModel;
using System.Reflection;

namespace Tessa.Education.BLL.Utils
{
    public static class EnumHelper
    {
        /// <summary>
        /// Получить дескриптор значения перечисления 
        /// </summary>
        /// <param name="enumValue">значение перечисление</param>
        /// <returns></returns>
        public static string? GetDescription(this Enum enumValue)
        {
            if (enumValue == null)
                throw new ArgumentNullException(nameof(enumValue));
            
            string? value = enumValue.ToString();
            FieldInfo? field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs.Length == 0)
                return value;

            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }

}

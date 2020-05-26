using System.ComponentModel;

namespace MunroLibrary.Data.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string into any object.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="item">The item to convert.</param>
        /// <returns></returns>
        public static T ConvertFromString<T>(this string item)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            return (T)typeConverter.ConvertFromString(item);
        }

    }
}

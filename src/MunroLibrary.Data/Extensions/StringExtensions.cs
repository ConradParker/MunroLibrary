using System.ComponentModel;

namespace MunroLibrary.Data.Extensions
{
    public static class StringExtensions
    {
        public static T ConvertFromString<T>(this string item)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            return (T)typeConverter.ConvertFromString(item);
        }

    }
}

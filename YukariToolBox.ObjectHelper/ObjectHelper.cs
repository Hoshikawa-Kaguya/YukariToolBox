using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace YukariToolBox.ObjectHelper
{
    public static class ObjectHelper
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>实例</returns>
        public static T CreateInstance<T>()
            => (T) typeof(T).CreateInstance();

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>实例</returns>
        public static object CreateInstance(this Type type)
        {
            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                                  .FirstOrDefault(con => con.GetParameters().Length == 0);


            return constructor?.Invoke(null) ?? FormatterServices.GetUninitializedObject(type);
        }

        /// <summary>
        /// 字符串类型转换
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="input">需要转换的字符串</param>
        /// <returns>转换值</returns>
        public static T? Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) converter.ConvertFromString(input)!;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// 字符串类型转换
        /// </summary>
        /// <param name="input">需要转换的字符串</param>
        /// <param name="type">转换类型</param>
        /// <returns>转换值</returns>
        public static object? Convert(this string input, Type type)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(type);
                return converter.ConvertFromString(input);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetCore.DynaX
{
    public static partial class DynaX
    {
        /// <summary>
        /// 转换为 Json 字符串
        /// </summary>
        /// <param name="source">输入值</param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            return source == null ? "{}" : JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// 转换为 Json 字符串
        /// </summary>
        /// <param name="source">输入值</param>
        /// <param name="formatDate">日期格式</param>
        /// <returns></returns>
        public static string ToJson(this object source, string formatDate)
        {
            return source == null ? "{}" : JsonConvert.SerializeObject(source, new IsoDateTimeConverter { DateTimeFormat = formatDate });
        }

        /// <summary>
        /// Json 字符串转换为 T
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="source">Json字符串</param>
        /// <returns></returns>
        public static T ToObj<T>(this string source) where T :  class, new()
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        /// <summary>
        /// Json 字符串转换为 List&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="source">Json字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string source) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(source);
        }
    }
}

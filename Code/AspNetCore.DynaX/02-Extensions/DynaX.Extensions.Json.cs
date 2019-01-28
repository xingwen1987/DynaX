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
    }
}

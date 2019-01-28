using System;

namespace AspNetCore.DynaX
{
    public static partial class DynaX
    {
        /// <summary>
        /// 转换为安全字符串
        /// </summary>
        /// <param name="source">输入值</param>
        /// <returns></returns>
        public static string ToSafeString(this object source)
        {
            return source?.ToString().Trim() ?? string.Empty;
        }

        /// <summary>
        /// 转换为 Guid
        /// </summary>
        /// <param name="source">输入值</param>
        /// <returns></returns>
        public static Guid ToGuid(this string source)
        {
            return ToGuidOrNull(source) ?? Guid.Empty;
        }

        /// <summary>
        /// 转换为可空 Guid
        /// </summary>
        /// <param name="source">输入值</param>
        /// <returns></returns>
        public static Guid? ToGuidOrNull(this string source)
        {
            return Guid.TryParse(source, out var result) ? (Guid?)result : null;
        }
    }
}

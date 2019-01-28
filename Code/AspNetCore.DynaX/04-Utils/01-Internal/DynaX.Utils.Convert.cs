using System;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Utils 扩展集合
        /// </summary>
        public static partial class Utils
        {
            #region Enum

            /// <summary>
            /// 枚举转换
            /// </summary>
            /// <typeparam name="T">T</typeparam>
            /// <param name="value">值</param>
            /// <returns>T值</returns>
            public static T EnumParse<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }

            #endregion
        }
    }
}

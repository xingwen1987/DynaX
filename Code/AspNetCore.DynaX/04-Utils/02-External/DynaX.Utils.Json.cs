using System.Collections.Generic;
using Newtonsoft.Json;

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
            /// <summary>
            /// DynaX Utils Json 扩展集合
            /// </summary>
            public static class Json
            {
                public static T ToEntity<T>(string source)
                {
                    return source == null ? default(T) : JsonConvert.DeserializeObject<T>(source);
                }

                public static List<T> ToList<T>(string source)
                {
                    return source == null ? null : JsonConvert.DeserializeObject<List<T>>(source);
                }
            }
        }
    }
}

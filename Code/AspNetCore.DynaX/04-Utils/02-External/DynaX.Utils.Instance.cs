using System;
using AspectCore.Extensions.Reflection;

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
            /// 创建实例
            /// </summary>
            /// <typeparam name="T">转出类型 T</typeparam>
            /// <param name="args">参数集合</param>
            /// <returns></returns>
            public static T CreateInstance<T>(params object[] args) where T : class
            {
                var constructorInfo = typeof(T).GetConstructor(Types.Of(args));
                return constructorInfo == null ? (T) Activator.CreateInstance(typeof(T), args) : (T) constructorInfo.GetReflector().Invoke(args);
            }
        }
    }
}

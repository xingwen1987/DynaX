using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            /// DynaX Utils Types 扩展集合
            /// </summary>
            public static partial class Types
            {
                /// <summary>
                /// 获取对象实际类型
                /// </summary>
                /// <param name="source">输入值</param>
                /// <returns></returns>
                public static Type GetType(object source)
                {
                    var type = source.GetType();
                    if (source is System.Collections.IEnumerable == false) return type;
                    if (type.IsArray) return type.GetElementType();
                    var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
                    if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                        throw new ArgumentException("泛型类型参数不能为空");
                    return genericArgumentsTypes[0];
                }

                /// <summary>
                /// 获取类型参数列表
                /// </summary>
                /// <param name="arg">参数</param>
                /// <returns></returns>
                public static Type[] Of(object arg)
                {
                    return new[] {arg.GetType()};
                }

                /// <summary>
                ///  获取类型集合参数列表
                /// </summary>
                /// <param name="args">参数集合</param>
                /// <returns></returns>
                public static Type[] Of(object[] args)
                {
                    var typeList = new List<Type>();
                    args.ToList().ForEach(arg => typeList.Add(arg.GetType()));
                    return typeList.ToArray();
                }
            }
        }
    }
}

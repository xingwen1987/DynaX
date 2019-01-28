using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// EF 精度扩展
        /// </summary>
        /// <param name="builder">属性编辑器</param>
        /// <param name="precision">长度</param>
        /// <param name="scale">精度</param>
        /// <returns></returns>
        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        /// <summary>
        /// EF 精度扩展
        /// </summary>
        /// <param name="builder">属性编辑器</param>
        /// <param name="precision">长度</param>
        /// <param name="scale">精度</param>
        /// <returns></returns>
        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }
    }
}

using System.IO;

namespace AspNetCore.DynaX
{
    public static partial class DynaX
    {
        public static string FileFullName(this string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string FileName(this string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static string FileSuffix(this string filePath)
        {
            return Path.GetExtension(filePath)?.TrimStart('.');
        }
    }
}

using System;
using System.IO;
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// 文件转换为字节数组
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="byteLen">字节组长度</param>
        /// <param name="fileMode"></param>
        /// <returns></returns>
        public static byte[] ToFileBytes(string filePath, int byteLen = 0, FileMode fileMode = FileMode.Open)
        {
            var fileStream = new FileStream(filePath, fileMode);
            var fileBytes = new byte[byteLen > 0 ? byteLen : fileStream.Length];
            fileStream.Read(fileBytes, 0, fileBytes.Length);
            fileStream.Dispose();
            return fileBytes;
        }

        /// <summary>
        /// 文件流转换为字节数组
        /// </summary>
        /// <param name="formFile">上传的文件信息</param>
        /// <param name="byteLen">字节组长度</param>
        /// <returns></returns>
        public static byte[] ToFileBytes(this IFormFile formFile, int byteLen = 0)
        {
            var fileStream = formFile.OpenReadStream();
            var fileBytes = new byte[byteLen > 0 ? byteLen : fileStream.Length];
            fileStream.Read(fileBytes, 0, fileBytes.Length);
            fileStream.Dispose();
            return fileBytes;
        }
    }
}

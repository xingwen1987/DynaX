using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <param name="fileMode">文件操作类型</param>
        /// <returns></returns>
        public static byte[] ToFileBytes(this string filePath, int byteLen = 0, FileMode fileMode = FileMode.Open)
        {
            var fileStream = new FileStream(filePath, fileMode);
            var fileBytes = new byte[byteLen > 0 ? byteLen : fileStream.Length];
            fileStream.Read(fileBytes, 0, fileBytes.Length);
            fileStream.Dispose();
            return fileBytes;
        }

        /// <summary>
        /// 表单文件转换为字节数组
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

        /// <summary>
        /// byte[] 转 int[]
        /// </summary>
        /// <param name="source">数据流</param>
        /// <returns></returns>
        public static int[] ToInts(this byte[] source)
        {
            return source.Select(b => (int)b).ToArray();
        }

        /// <summary>
        /// int[] 转 byte[]
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <returns></returns>
        public static byte[] ToBytes(this int[] source)
        {
            return source.Select(c => (byte)(c - '0')).ToArray();
        }

        /// <summary>
        /// byte[] 转 Hex
        /// </summary>
        /// <param name="source">数据流</param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] source)
        {
            return BitConverter.ToString(source).Replace("-", " ");
        }

        /// <summary>
        /// hex string 转 byte[]
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string source, string separator)
        {
            return Enumerable.Range(0, source.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(source.Substring(x, 2), 16))
                .ToArray();
        }

        /// <summary>
        /// byte[] 转 string[]
        /// </summary>
        /// <param name="source">数据流</param>
        /// <returns></returns>
        public static string[] ToStrs(this byte[] source)
        {
            return source.Select(t => t < 10 ? "0" + (int) t : "" + (int) t).ToArray();
        }

        /// <summary>
        /// string[] 转 byte[]
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string[] source)
        {
            return source.Select(c => (byte)(c.ToInt() - '0')).ToArray();
        }
    }
}

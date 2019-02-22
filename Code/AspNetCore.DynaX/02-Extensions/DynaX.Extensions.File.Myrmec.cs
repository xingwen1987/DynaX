using System.Collections.Generic;
using Myrmec;
using System.Linq;

namespace AspNetCore.DynaX
{
    public static partial class DynaX
    {
        /// <summary>
        /// 对比匹配文件
        /// </summary>
        /// <param name="source">文件流</param>
        /// <param name="fileTypes">文件目标类型</param>
        /// <returns></returns>
        private static List<string> SnifferFile(byte[] source, IEnumerable<string> fileTypes)
        {
            var fileSniffer = new Sniffer();
            var snifferTypes = FileRecord.ConfigTypes(fileTypes);
            fileSniffer.Populate(snifferTypes);
            return fileSniffer.Match(source);
        }

        #region Extensions

        #region Office

        /// <summary>
        /// 检验 Word 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsWord(this byte[] source)
        {
            var result = SnifferFile(source, new[] {"doc"});
            return result.Contains("doc") || result.Contains("docx");
        }

        /// <summary>
        /// 检验 Excel 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsExcel(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "xls" });
            return result.Contains("xls") || result.Contains("xlsx");
        }

        /// <summary>
        /// 检验 PPT 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsPpt(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "ppt" });
            return result.Contains("ppt") || result.Contains("pptx");
        }

        /// <summary>
        /// 检验 Visio 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsVisio(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "vsd" });
            return result.Contains("vsd") || result.Contains("vsdx");
        }

        /// <summary>
        /// 检验 PDF 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsPdf(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "pdf" });
            return result.Contains("pdf");
        }

        /// <summary>
        /// 检验 Office 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsOffice(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "office" });
            return result.Contains("doc") || result.Contains("docx") || result.Contains("xls") || result.Contains("xlsx") || result.Contains("ppt") || result.Contains("pptx") || result.Contains("vsd") || result.Contains("vsdx") || result.Contains("pdf");
        }

        #endregion

        #region Image

        /// <summary>
        /// 检验 JPG 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsJpg(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "jpg" });
            return result.Contains("jpg");
        }

        /// <summary>
        /// 检验 Jpeg 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsJpeg(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "jpeg" });
            return result.Contains("jpeg");
        }

        /// <summary>
        /// 检验 Png 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsPng(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "png" });
            return result.Contains("png");
        }

        /// <summary>
        /// 检验 Gif 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsGif(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "gif" });
            return result.Contains("gif");
        }

        /// <summary>
        /// 检验 Bmp 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsBmp(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "bmp" });
            return result.Contains("bmp");
        }

        /// <summary>
        /// 检验 Jp2 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsJp2(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "jp2" });
            return result.Contains("jp2");
        }

        /// <summary>
        /// 检验 Bmp 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsImage(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "image" });
            return result.Contains("jpg") || result.Contains("jpeg") || result.Contains("png") || result.Contains("gif") || result.Contains("bmp") || result.Contains("jp2");
        }

        #endregion

        #region Audio

        /// <summary>
        /// 检验 Mp3 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsMp3(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "mp3" });
            return result.Contains("mp3");
        }

        /// <summary>
        /// 检验 Wma 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsWma(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "wma" });
            return result.Contains("wma");
        }

        /// <summary>
        /// 检验 Flac 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsFlac(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "flac" });
            return result.Contains("flac");
        }

        /// <summary>
        /// 检验 Ogg、Oga、Ogv 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsOgx(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "ogx" });
            return result.Contains("ogg") || result.Contains("oga") || result.Contains("ogv");
        }

        /// <summary>
        /// 检验 Mid、Midi 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsMidi(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "mid" });
            return result.Contains("mid") || result.Contains("midi");
        }

        /// <summary>
        /// 检验 Audio 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsAudio(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "audio" });
            return result.Contains("mp3") || result.Contains("wma") || result.Contains("flac") || result.Contains("ogg") || result.Contains("oga") || result.Contains("ogv") || result.Contains("mid") || result.Contains("midi");
        }

        #endregion

        #region Video

        /// <summary>
        /// 检验 AVI 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsAvi(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "avi" });
            return result.Contains("avi");
        }

        /// <summary>
        /// 检验 Mp4 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsMp4(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "mp4" });
            return result.Contains("mp4");
        }

        /// <summary>
        /// 检验 Rmvb 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsRmvb(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "rmvb" });
            return result.Contains("rmvb");
        }

        /// <summary>
        /// 检验 Mkv 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsMkv(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "mkv" });
            return result.Contains("mkv");
        }

        /// <summary>
        /// 检验 Ts 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsTs(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "ts" });
            return result.Contains("ts");
        }

        /// <summary>
        /// 检验 Video 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsVideo(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "video" });
            return result.Contains("avi") || result.Contains("mp4") || result.Contains("rmvb") || result.Contains("mkv") || result.Contains("ts");
        }

        #endregion

        #region CompressFiles

        /// <summary>
        /// 检验 Zip 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsZip(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "zip" });
            return result.Contains("zip");
        }

        /// <summary>
        /// 检验 Rar 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsRar(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "rar" });
            return result.Contains("rar");
        }

        /// <summary>
        /// 检验 7z 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool Is7z(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "7z" });
            return result.Contains("7z");
        }

        /// <summary>
        /// 检验 Compress 文件
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static bool IsCompress(this byte[] source)
        {
            var result = SnifferFile(source, new[] { "compress" });
            return result.Contains("compress");
        }

        #endregion

        #endregion

        /// <summary>
        /// 文件类型集合
        /// </summary>
        public class FileRecord
        {
            /// <summary>
            /// 配置文件检测类型
            /// </summary>
            /// <param name="fileTypes">文件类型集合</param>
            /// <returns></returns>
            public static List<Record> ConfigTypes(IEnumerable<string> fileTypes)
            {
                var fileRecords = new List<Record>();
                foreach (var fileType in fileTypes)
                {
                    var currType = fileType.ToLower();

                    #region Office

                    if (currType.Contains("doc")) RecordWord(fileRecords);
                    if (currType.Contains("xls")) RecordExcel(fileRecords);
                    if (currType.Contains("ppt")) RecordPpt(fileRecords);
                    if (currType.Contains("vsd")) RecordVisio(fileRecords);
                    if (currType == "pdf") RecordPdf(fileRecords);
                    if (currType == "office") RecordOffice(fileRecords);

                    #endregion

                    #region Image

                    if (currType =="jpg") RecordJpg(fileRecords);
                    if (currType == "jpeg") RecordJpeg(fileRecords);
                    if (currType == "png") RecordPng(fileRecords);
                    if (currType == "gif") RecordGif(fileRecords);
                    if (currType == "bmp") RecordBmp(fileRecords);
                    if (currType == "jp2") RecordJp2(fileRecords);
                    if (currType == "image") RecordImage(fileRecords);

                    #endregion

                    #region Audio

                    if (currType == "mp3") RecordMp3(fileRecords);
                    if (currType == "wma") RecordWma(fileRecords);
                    if (currType == "flac") RecordFlac(fileRecords);
                    if (currType == "ogx") RecordOgx(fileRecords);
                    if (currType == "mid") RecordMidi(fileRecords);
                    if (currType == "audio") RecordAudio(fileRecords);

                    #endregion

                    #region Video

                    if (currType == "avi") RecordAvi(fileRecords);
                    if (currType == "mp4") RecordMp4(fileRecords);
                    if (currType == "rmvb") RecordRmvb(fileRecords);
                    if (currType == "mkv") RecordMkv(fileRecords);
                    if (currType == "ts") RecordTs(fileRecords);
                    if (currType == "video") RecordVideo(fileRecords);

                    #endregion

                    #region CompressFiles

                    if (currType == "zip") RecordZip(fileRecords);
                    if (currType == "rar") RecordRar(fileRecords);
                    if (currType == "7z") Record7Z(fileRecords);
                    if (currType == "compress") RecordCompress(fileRecords);

                    #endregion

                }
                return fileRecords.GroupBy(r => new  { r.Extentions, r.Hex }).Select(r => r.First()).ToList();
            }

            #region Office

            /// <summary>
            /// 配置 Word
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordWord(ICollection<Record> records)
            {
                records.Add(new Record("doc", "D0 CF 11 E0 A1 B1 1A E1"));
                records.Add(new Record("docx", "50,4b,03,04"));
                records.Add(new Record("docx", "50,4b,05,06"));
                records.Add(new Record("docx", "50,4b,07,08"));
            }

            /// <summary>
            /// 配置 Excel
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordExcel(ICollection<Record> records)
            {
                records.Add(new Record("xls", "D0 CF 11 E0 A1 B1 1A E1"));
                records.Add(new Record("xlsx", "50,4b,03,04"));
                records.Add(new Record("xlsx", "50,4b,05,06"));
                records.Add(new Record("xlsx", "50,4b,07,08"));
            }

            /// <summary>
            /// 配置 PPT
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordPpt(ICollection<Record> records)
            {
                records.Add(new Record("ppt", "D0 CF 11 E0 A1 B1 1A E1"));
                records.Add(new Record("pptx", "50,4b,03,04"));
                records.Add(new Record("pptx", "50,4b,05,06"));
                records.Add(new Record("pptx", "50,4b,07,08"));
            }

            /// <summary>
            /// 配置 Visio
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordVisio(ICollection<Record> records)
            {
                //records.Add(new Record("vsd", "D0 CF 11 E0 A1 B1 1A E1"));
                records.Add(new Record("vsdx", "50,4b,03,04"));
                records.Add(new Record("vsdx", "50,4b,05,06"));
                records.Add(new Record("vsdx", "50,4b,07,08"));
            }

            /// <summary>
            /// 配置 Pdf
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordPdf(ICollection<Record> records)
            {
                records.Add(new Record("pdf", "25 50 44 46"));
            }

            /// <summary>
            /// 配置 Office
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordOffice(ICollection<Record> records)
            {
                RecordWord(records);
                RecordExcel(records);
                RecordPpt(records);
                RecordVisio(records);
                RecordPdf(records);
            }

            #endregion

            #region Image

            /// <summary>
            /// 配置 Jpg
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordJpg(ICollection<Record> records)
            {
                records.Add(new Record("jpg", "FF,D8,FF,DB"));
                records.Add(new Record("jpg", "FF D8 FF E0 ?? ?? 4A 46 49 46 00 01"));
                records.Add(new Record("jpg", "FF D8 FF E1 ?? ?? 45 78 69 66 00 00"));
            }

            /// <summary>
            /// 配置 Jpeg
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordJpeg(ICollection<Record> records)
            {
                records.Add(new Record("jpeg", "ff,d8,ff,db"));
                records.Add(new Record("jpeg", "FF D8 FF E0 ?? ?? 4A 46 49 46 00 01"));
                records.Add(new Record("jpeg", "FF D8 FF E1 ?? ?? 45 78 69 66 00 00"));
            }

            /// <summary>
            /// 配置 Png
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordPng(ICollection<Record> records)
            {
                records.Add(new Record("png", "89,50,4e,47,0d,0a,1a,0a"));
            }

            /// <summary>
            /// 配置 Gif
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordGif(ICollection<Record> records)
            {
                records.Add(new Record("gif", "47 49 46 38 37 61"));
                records.Add(new Record("gif", "47 49 46 38 39 61"));
            }

            /// <summary>
            /// 配置 Bmp
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordBmp(ICollection<Record> records)
            {
                records.Add(new Record("bmp", "42 4D"));
            }

            /// <summary>
            /// 配置 Jp2
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordJp2(ICollection<Record> records)
            {
                records.Add(new Record("jp2", "00 00 00 0C 6A 50 20 20 0D 0A", "Various JPEG-2000 image file formats"));
            }

            /// <summary>
            /// 配置 Image
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordImage(ICollection<Record> records)
            {
                RecordJpg(records);
                RecordJpeg(records);
                RecordPng(records);
                RecordGif(records);
                RecordBmp(records);
                RecordJp2(records);
            }

            #endregion

            #region Audio

            /// <summary>
            /// 配置 Mp3
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordMp3(ICollection<Record> records)
            {
                records.Add(new Record("mp3", "FF FB"));
                records.Add(new Record("mp3", "49 44 33"));
                records.Add(new Record("mp3", "73 68 51 03"));
            }

            /// <summary>
            /// 配置 Wma
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordWma(ICollection<Record> records)
            {
                records.Add(new Record("wma", "30 26 B2 75 8E 66 CF 11 A6 D9 00 AA 00 62 CE 6C"));
            }

            /// <summary>
            /// 配置 Flac
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordFlac(ICollection<Record> records)
            {
                records.Add(new Record("flac", "66 4C 61 43"));
            }

            /// <summary>
            /// 配置 Ogx
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordOgx(ICollection<Record> records)
            {
                records.Add(new Record("ogg oga ogv", "4F 67 67 53"));
            }

            /// <summary>
            /// 配置 Mid
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordMidi(ICollection<Record> records)
            {
                records.Add(new Record("mid midi", "4D 54 68 64"));
            }

            /// <summary>
            /// 配置 Audio
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordAudio(ICollection<Record> records)
            {
                RecordMp3(records);
                RecordWma(records);
                RecordFlac(records);
                RecordMidi(records);
                RecordOgx(records);
            }

            #endregion

            #region Video

            /// <summary>
            /// 配置 AVI
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordAvi(ICollection<Record> records)
            {
                records.Add(new Record("avi", "52 49 46 46"));
            }

            /// <summary>
            /// 配置 MP4
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordMp4(ICollection<Record> records)
            {
                records.Add(new Record("mp4", "00 00 00 ?? 66 74 79 70 ?? ?? ?? ?? 00 00 00 01 69 73 6F 6D"));
            }

            /// <summary>
            /// 配置 RMVB
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordRmvb(ICollection<Record> records)
            {
                records.Add(new Record("rmvb", "2E 52 4D 46"));
            }

            /// <summary>
            /// 配置 MKV
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordMkv(ICollection<Record> records)
            {
                records.Add(new Record("mkv", "1A 45 DF A3"));
            }

            /// <summary>
            /// 配置 TS
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordTs(ICollection<Record> records)
            {
                records.Add(new Record("ts", "47 40 11 10"));
            }

            /// <summary>
            /// 配置 Video
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordVideo(ICollection<Record> records)
            {
                RecordAvi(records);
                RecordMp4(records);
                RecordRmvb(records);
                RecordMkv(records);
                RecordTs(records);
            }

            #endregion

            #region CompressFiles

            /// <summary>
            /// 配置 Zip
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordZip(ICollection<Record> records)
            {
                records.Add(new Record("zip", "50,4b,03,04"));
                records.Add(new Record("zip", "50,4b,05,06"));
                records.Add(new Record("zip", "50,4b,07,08"));
            }

            /// <summary>
            /// 配置 Rar
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordRar(ICollection<Record> records)
            {
                records.Add(new Record("rar", "52,61,72,21,1a,07,00"));
                records.Add(new Record("rar", "52,61,72,21,1a,07,01,00"));
            }

            /// <summary>
            /// 配置 7z
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void Record7Z(ICollection<Record> records)
            {
                records.Add(new Record("7z", "37 7A BC AF 27 1C"));
            }

            /// <summary>
            /// 配置 Compress
            /// </summary>
            /// <param name="records">文件类型集合</param>
            public static void RecordCompress(ICollection<Record> records)
            {
                RecordZip(records);
                RecordRar(records);
                Record7Z(records);
            }

            #endregion
        }
    }
}

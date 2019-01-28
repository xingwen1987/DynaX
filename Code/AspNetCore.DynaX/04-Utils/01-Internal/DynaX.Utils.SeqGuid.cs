using System;
using System.Security.Cryptography;

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
            /// DynaX Utils SeqGuid 连续 Guid 类型
            /// https://github.com/claudio4/SequentialGuid
            /// </summary>
            public static class SeqGuid
            {
                private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
                /// <summary>
                /// generatorType visit https://git.io/vS1hL
                /// </summary>
                public static Guid Next(DataBaseType dbType = DataBaseType.SqlServer)
                {
                    var randomBytes = new byte[10];
                    Rng.GetBytes(randomBytes);
                    var timestamp = DateTime.UtcNow.Ticks / 10000L;
                    var timestampBytes = BitConverter.GetBytes(timestamp);
                    if (BitConverter.IsLittleEndian) { Array.Reverse(timestampBytes); }
                    var guidBytes = new byte[16];
                    var generatorType = SeqGuidType.SequentialAtEnd;
                    switch (dbType)
                    {
                        case DataBaseType.SqlServer:
                            generatorType = SeqGuidType.SequentialAtEnd;
                            break;
                        case DataBaseType.MySql:
                        case DataBaseType.PostgreSql:
                            generatorType = SeqGuidType.SequentialAsString;
                            break;
                        case DataBaseType.Oracle:
                            generatorType = SeqGuidType.SequentialAsBinary;
                            break;
                        case DataBaseType.MongoDb:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null);
                    }
                    switch (generatorType)
                    {
                        case SeqGuidType.SequentialAsString:
                        case SeqGuidType.SequentialAsBinary:
                            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                            Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);
                            if (generatorType == SeqGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(guidBytes, 0, 4);
                                Array.Reverse(guidBytes, 4, 2);
                            }
                            break;
                        case SeqGuidType.SequentialAtEnd:
                            Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    return new Guid(guidBytes);
                }
            }

            /// <summary>
            /// 连续 Guid 类型
            /// </summary>
            public enum SeqGuidType
            {
                SequentialAsString,
                SequentialAsBinary,
                SequentialAtEnd
            }
        }
    }
}

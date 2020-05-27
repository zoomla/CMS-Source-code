using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZoomLa.BLL.Helper
{
    /// <summary>
    /// 用于IO对象的相互转换,写入由SafeSC完成
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// 转为二进制,用于存储与提交并入
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            MemoryStream ms = StreamToMStream(stream);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            ms.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// 普通流转内存
        /// 用于:HttpRepsonse等必须先读取才能操作的流
        /// </summary>
        public static MemoryStream StreamToMStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = new byte[1024];
            while (true)
            {
                int sz = stream.Read(buffer, 0, 1024);
                if (sz == 0) break;
                ms.Write(buffer, 0, sz);
            }
            ms.Position = 0;
            return ms;
        }
        /// <summary>
        /// 二进制转换为内存流
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}

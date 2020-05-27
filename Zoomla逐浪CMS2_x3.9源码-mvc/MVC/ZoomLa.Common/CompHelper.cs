using SharpCompress.Common;
using SharpCompress.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/*
 * 用于支持zip,rar,7zip,tar,gzipo,bzip2压缩和解压
 * 暂只需要实现rar
 */
namespace ZoomLa.Common
{
    public class CompHelper
    {
        /// <summary>
        /// Rar解压缩,物理路径
        /// </summary>
        /// <param name="rarpath">来源rar文件</param>
        /// <param name="dirpath">目标目录</param>
        public void UnComp_rar(string rarPath, string dirPath)
        {
            using (Stream stream = File.OpenRead(rarPath))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        reader.WriteEntryToDirectory(dirPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                }
            }
        }
        /// <summary>
        /// Rar压缩,物理路径(winrar必须作者授权,所以只能通过winrar.exe实现)
        /// </summary>
        /// <param name="dirPath">需要压缩的文件或文件夹路径</param>
        /// <param name="rarPath">目标压缩包路径</param>
        public void Comp_rar(string dirPath, string rarPath)
        {
            throw new Exception("未实现该方法");
        }
        public void UnComp_7zip(string zipPath, string dirPath) 
        {
            //using (Stream stream = File.OpenRead(zipPath))
            //{
            //    var reader = ReaderFactory.Open(stream);
            //    while (reader.MoveToNextEntry())
            //    {
            //        if (!reader.Entry.IsDirectory)
            //        {
            //            reader.WriteEntryToDirectory(dirPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
            //        }
            //    }
            //}

        }
        public void Comp_7zip(string dirPath,string zipPath) { }
    }
}

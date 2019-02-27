using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System.Web;
using System.Xml;
namespace ZoomLa.Common
{
    public class ZipClass
    {
     
        /*
         * 需改进:静态变量的方法只适合单个使用
         */ 
        /// <summary>
        ///  需要压缩的文件数
        /// </summary>
        public static int zipTotal = 0;
        /// <summary>
        /// 已处理的文件数,使用前清零操作
        /// </summary>
        public static int zipProgress = 0;
        /// <summary>
        /// 整个文件流的大小
        /// </summary>
        public static long unZipTotal = 0;
        /// <summary>
        /// 已处理到的字节,使用前清零操作
        /// </summary>
        public static long unZipProgress = 0;
        //标识已完成
        public readonly static int completeFlag=-2;
        public static bool ContainTemp = false;
        public string IgnoreFile = "";//忽略压缩文件本身
        #region ZipFileDictory
        /// <summary>
        /// 递归压缩文件夹方法,实际的处理方法,排除temp目录
        /// </summary>
        /// <param name="FolderToZip"></param>
        /// <param name="s"></param>
        /// <param name="ParentFolderName"></param>
        private bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            //勾选全部备份,则包含temp目录
            if (!ContainTemp&&FolderToZip.Substring(FolderToZip.Length - 5, 5).ToLower().Equals(@"\temp")) return true;
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                //创建当前文件夹

                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));  //加上 “/” 才会当成是文件夹创建
                //entry.CompressionMethod = CompressionMethod.Stored;//如果有报错mathc则加上此句
                s.PutNextEntry(entry);
                s.Flush();
                //先压缩文件，再递归压缩文件夹 
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    if (file.ToLower().Equals(IgnoreFile.ToLower())) continue;
                    //打开压缩文件
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                    zipProgress++;
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                    entry = null;
                GC.Collect();
                GC.Collect(1);
            }
            folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                    return false;
            }
            return res;
        }
        #endregion
        #region ZipFileDictory
        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后的文件名，全路径格式</param>
        /// <returns></returns>
        private bool ZipFileDictory(string FolderToZip, string ZipedFile, string Password)
        {
            bool res;
                if (!Directory.Exists(FolderToZip))
                    return false;
                ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
                s.SetLevel(6);
                if (!string.IsNullOrEmpty(Password.Trim()))
                    s.Password = Password.Trim();
                res = ZipFileDictory(FolderToZip, s, "");
                s.Finish();
                s.Close();
            return res;
        }
        #endregion
        #region ZipFile
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        /// <returns></returns>
        private bool ZipFile(string FileToZip, string ZipedFile, string Password)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            else if (FileToZip.ToLower().Equals(IgnoreFile.ToLower())) return true;//要打包的文件是压缩文件本身,则不处理
            //FileStream fs = null;
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {
                ZipFile = File.OpenRead(FileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();
                ZipFile = File.Create(ZipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                if (!string.IsNullOrEmpty(Password.Trim()))
                    ZipStream.Password = Password.Trim();
                ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(6);
                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;
        }
        #endregion
        #region Zip
        /// <summary>
        /// 压缩文件和文件夹
        /// </summary>
        /// <param name="FileToZip">待压缩的文件或文件夹，物理路径</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名，物理路径</param>
        /// <param name="Password">压缩密码</param>
        /// <returns></returns>
        public bool Zip(string FileToZip, string ZipedFile, string Password="")
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, Password);
            }
            else if (File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, Password);
            }
            else
            {
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 来源压缩文件,目标目录
        /// </summary>
        public  bool UnZipFiles(string file, string dir,string flag="",HttpContext ct=null)
        {
            unZipProgress = 0;
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                ZipInputStream s = new ZipInputStream(File.OpenRead(file));

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);

                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                            if (!string.IsNullOrEmpty(flag) && ct != null)//未设定则用静态变量保存
                            {
                                ct.Application[flag] = s.Position;
                            }
                            else { unZipProgress = s.Position; }
                        }
                        streamWriter.Close();
                    }
                }

                s.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                unZipProgress = unZipTotal;
                if (!string.IsNullOrEmpty(flag) && ct != null) ct.Application[flag] = completeFlag;
            }
        }
        /// <summary>
        /// 输入文件大小或文件数,如果nowProgress=-2,则也表示完成
        /// </summary>
        public static int GetPercent(long total,long nowProgress) 
        {
            if (total <1) return -1;
            long percent = total / 100;
            if (nowProgress == completeFlag) return 100;
            return Convert.ToInt32(nowProgress / percent);
        }
    }
}
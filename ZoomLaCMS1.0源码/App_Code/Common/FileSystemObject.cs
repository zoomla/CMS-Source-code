namespace ZoomLa.Common
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Web;

    /// <summary>
    /// FileSystemObject 的摘要说明
    /// </summary>
    public class FileSystemObject
    {
        public FileSystemObject()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将文件大小输出为GB MB KB B格式显示
        /// </summary>
        public static string ConvertSizeToShow(int fileSize)
        {
            int num = fileSize / 0x400;
            if (num >= 1)
            {
                if (num >= 0x400)
                {
                    int num2 = num / 0x400;
                    if (num2 >= 1)
                    {
                        if (num2 >= 0x400)
                        {
                            num2 /= 0x400;
                            return (num2.ToString() + "<span style='color:red'>&nbsp;GB</span>");
                        }
                        return (num2.ToString() + "<span style='color:red'>&nbsp;MB</span>");
                    }
                }
                return (num.ToString() + "<span style='color:red'>&nbsp;KB</span>");
            }
            return (fileSize.ToString() + "<span style='color:red'>&nbsp;&nbsp;B</span>");
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        public static void CopyDirectory(string oldDir, string newDir)
        {
            DirectoryInfo od = new DirectoryInfo(oldDir);
            CopyDirInfo(od, oldDir, newDir);
        }
        /// <summary>
        /// 复制文件夹及子文件夹到新文件夹
        /// </summary>
        private static void CopyDirInfo(DirectoryInfo od, string oldDir, string newDir)
        {
            if (!IsExist(newDir, FsoMethod.Folder))
            {
                Create(newDir, FsoMethod.Folder);
            }
            DirectoryInfo[] directories = od.GetDirectories();
            foreach (DirectoryInfo info in directories)
            {
                CopyDirInfo(info, info.FullName, newDir + info.FullName.Replace(oldDir, ""));
            }
            FileInfo[] files = od.GetFiles();
            foreach (FileInfo info2 in files)
            {
                CopyFile(info2.FullName, newDir + info2.FullName.Replace(oldDir, ""));
            }
        }
        /// <summary>
        /// 复制设备信息
        /// </summary>
        public static DataTable CopyDT(DataTable parent, DataTable child)
        {
            for (int i = 0; i < child.Rows.Count; i++)
            {
                DataRow row = parent.NewRow();
                for (int j = 0; j < parent.Columns.Count; j++)
                {
                    row[j] = child.Rows[i][j];
                }
                parent.Rows.Add(row);
            }
            return parent;
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        public static void CopyFile(string oldFile, string newFile)
        {
            File.Copy(oldFile, newFile, true);
        }
        /// <summary>
        /// 复制文件流
        /// </summary>
        public static bool CopyFileStream(string oldPath, string newPath)
        {
            try
            {
                FileStream input = new FileStream(oldPath, FileMode.Open, FileAccess.Read);
                FileStream output = new FileStream(newPath, FileMode.Create, FileAccess.Write);
                BinaryReader reader = new BinaryReader(input);
                BinaryWriter writer = new BinaryWriter(output);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                reader.BaseStream.Seek(0L, SeekOrigin.End);
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    writer.Write(reader.ReadByte());
                }
                reader.Close();
                writer.Close();
                input.Flush();
                input.Close();
                output.Flush();
                output.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 创建文件
        /// <param name="file">文件名</param>
        /// <param name="method">创建方式 Folder文件夹 File文件</param>
        /// </summary>
        public static void Create(string file, FsoMethod method)
        {
            try
            {
                if (method == FsoMethod.File)
                {
                    WriteFile(file, "");
                }
                else if (method == FsoMethod.Folder)
                {
                    Directory.CreateDirectory(file);
                }
            }
            catch
            {
                throw new UnauthorizedAccessException("无法创建文件");
            }
        }
        /// <summary>
        /// 创建文件夹
        /// <param>folderName文件夹名</param>
        /// <returns>返回文件夹路径</returns>
        /// </summary>
        public static string CreateFileFolder(string folderName)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                throw new HttpException("创建文件夹出错");
            }
            return CreateFileFolder(folderName, current);
        }
        /// <summary>
        /// 创建文件夹
        /// <param>folderName文件夹名</param>
        /// <param>context当前Http请求对象</param>
        /// <returns>返回文件夹Url路径</returns>
        /// </summary>
        public static string CreateFileFolder(string folderName, HttpContext context)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName", "文件夹参数不能为空");
            }
            string path = Path.Combine(context.Request.PhysicalApplicationPath, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        /// <summary>
        /// 删除操作
        /// <param name="file">file待删除的文件</param>
        /// <param name="method">method 操作对象FsoMethod的枚举类型Folder文件夹 File文件</param>
        /// </summary>
        public static void Delete(string file, FsoMethod method)
        {
            if ((method == FsoMethod.File) && File.Exists(file))
            {
                File.Delete(file);
            }
            if ((method == FsoMethod.Folder) && Directory.Exists(file))
            {
                Directory.Delete(file, true);
            }
        }
        /// <summary>
        /// 获取目录下的子目录及文件信息
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static DataTable GetFileList(string dirPath)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Path", typeof(string));
            table.Columns.Add("UpdateTime", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Size", typeof(long));
            string[] directories = Directory.GetDirectories(dirPath);
            string[] files = Directory.GetFiles(dirPath);
            foreach (string str in directories)
            {
                DirectoryInfo d = new DirectoryInfo(str);
                DataRow row = table.NewRow();
                row["Name"] = d.Name;
                row["Path"] = str;
                row["UpdateTime"] = d.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                row["Type"] = "";
                row["Size"] = DirInfo(d)[0];
                table.Rows.Add(row);
                d = null;
            }
            foreach (string str2 in files)
            {
                new DirectoryInfo(str2);
                FileInfo info2 = new FileInfo(str2);
                DataRow row2 = table.NewRow();
                row2["Name"] = info2.Name;
                row2["Path"] = str2;
                row2["UpdateTime"] = info2.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                row2["Type"] = Path.GetExtension(str2).ToLower();
                row2["Size"] = info2.Length;
                table.Rows.Add(row2);
                info2 = null;
            }
            return table;
        }
        /// <summary>
        /// 获取文件夹中文件数量信息
        /// </summary>
        private static long[] DirInfo(DirectoryInfo d)
        {
            long[] numArray = new long[3];
            long num = 0L;
            long num2 = 0L;
            long num3 = 0L;
            FileInfo[] files = d.GetFiles();
            num3 += files.Length;
            foreach (FileInfo info in files)
            {
                num += info.Length;
            }
            DirectoryInfo[] directories = d.GetDirectories();
            num2 += directories.Length;
            foreach (DirectoryInfo info2 in directories)
            {
                num += DirInfo(info2)[0];
                num2 += DirInfo(info2)[1];
                num3 += DirInfo(info2)[2];
            }
            numArray[0] = num;
            numArray[1] = num2;
            numArray[2] = num3;
            return numArray;
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        private static DataTable GetDirectoryAllInfo(DirectoryInfo d, FsoMethod method)
        {
            DataRow row;
            DataTable parent = new DataTable();
            parent.Columns.Add("name");
            parent.Columns.Add("rname");
            parent.Columns.Add("content_type");
            parent.Columns.Add("type");
            parent.Columns.Add("path");
            parent.Columns.Add("creatime", typeof(DateTime));
            parent.Columns.Add("size", typeof(int));
            DirectoryInfo[] directories = d.GetDirectories();
            foreach (DirectoryInfo info in directories)
            {
                if (method == FsoMethod.File)
                {
                    parent = CopyDT(parent, GetDirectoryAllInfo(info, method));
                }
                else
                {
                    row = parent.NewRow();
                    row[0] = info.Name;
                    row[1] = info.FullName;
                    row[2] = "";
                    row[3] = 1;
                    row[4] = info.FullName.Replace(info.Name, "");
                    row[5] = info.CreationTime;
                    row[6] = "";
                    parent.Rows.Add(row);
                    parent = CopyDT(parent, GetDirectoryAllInfo(info, method));
                }
            }
            if (method != FsoMethod.Folder)
            {
                FileInfo[] files = d.GetFiles();
                foreach (FileInfo info2 in files)
                {
                    row = parent.NewRow();
                    row[0] = info2.Name;
                    row[1] = info2.FullName;
                    row[2] = info2.Extension.Replace(".", "");
                    row[3] = 2;
                    row[4] = info2.DirectoryName + @"\";
                    row[5] = info2.CreationTime;
                    row[6] = info2.Length;
                    parent.Rows.Add(row);
                }
            }
            return parent;
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        public static DataTable GetDirectoryAllInfos(string dir, FsoMethod method)
        {
            DataTable directoryAllInfo;
            try
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                directoryAllInfo = GetDirectoryAllInfo(d, method);
            }
            catch (Exception exception)
            {
                throw new FileNotFoundException(exception.ToString());
            }
            return directoryAllInfo;
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        public static DataTable GetDirectoryInfos(string dir, FsoMethod method)
        {
            DataRow row;
            int num;
            DataTable table = new DataTable();
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("type");
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("content_type");
            table.Columns.Add("createTime", typeof(DateTime));
            table.Columns.Add("lastWriteTime", typeof(DateTime));
            if (method != FsoMethod.File)
            {
                for (num = 0; num < getDirs(dir).Length; num++)
                {
                    row = table.NewRow();
                    DirectoryInfo d = new DirectoryInfo(getDirs(dir)[num]);
                    long[] numArray = DirInfo(d);
                    row[0] = d.Name;
                    row[1] = 1;
                    row[2] = numArray[0];
                    row[3] = "";
                    row[4] = d.CreationTime;
                    row[5] = d.LastWriteTime;
                    table.Rows.Add(row);
                }
            }
            if (method != FsoMethod.Folder)
            {
                for (num = 0; num < getFiles(dir).Length; num++)
                {
                    row = table.NewRow();
                    FileInfo info2 = new FileInfo(getFiles(dir)[num]);
                    row[0] = info2.Name;
                    row[1] = 2;
                    row[2] = info2.Length;
                    row[3] = info2.Extension.Replace(".", "");
                    row[4] = info2.CreationTime;
                    row[5] = info2.LastWriteTime;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        public static long[] GetDirInfos(string dir)
        {
            long[] numArray = new long[3];
            DirectoryInfo d = new DirectoryInfo(dir);
            return DirInfo(d);
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        private static string[] getDirs(string dir)
        {
            return Directory.GetDirectories(dir);
        }
        /// <summary>
        /// 获取文件夹下的文件名数组
        /// </summary>
        private static string[] getFiles(string dir)
        {
            return Directory.GetFiles(dir);
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        public static string GetFileSize(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            float num = info.Length / 0x400L;
            return (num.ToString() + "KB");
        }
        /// <summary>
        /// 操作对象是否存在
        /// </summary>
        public static bool IsExist(string file, FsoMethod method)
        {
            if (method == FsoMethod.File)
            {
                return File.Exists(file);
            }
            return ((method == FsoMethod.Folder) && Directory.Exists(file));
        }
        /// <summary>
        /// 文件夹是否存在，如果不存在则创建文件夹
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId="categorDir")]
        public static bool IsExistCategoryDirAndCreate(string categorDir, HttpContext context)
        {
            if (context != null)
            {
                string file = Path.Combine(context.Request.PhysicalApplicationPath, categorDir);
                if (IsExist(file, FsoMethod.Folder))
                {
                    return true;
                }
                Create(file, FsoMethod.Folder);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        public static void Move(string oldFile, string newFile, FsoMethod method)
        {
            if (method == FsoMethod.File)
            {
                File.Move(oldFile, newFile);
            }
            if (method == FsoMethod.Folder)
            {
                Directory.Move(oldFile, newFile);
            }
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        public static string ReadFile(string file)
        {
            string str = "";
            using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                {
                    try
                    {
                        str = reader.ReadToEnd();
                    }
                    catch
                    {
                    }
                    return str;
                }
            }
        }
        /// <summary>
        /// 替换文件夹下所有文件的文件内容
        /// </summary>
        public static void ReplaceFileContent(string dir, string originalContent, string newContent)
        {
            FileInfo[] files = new DirectoryInfo(dir).GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo info2 in files)
            {
                StreamReader reader = info2.OpenText();
                string str = reader.ReadToEnd();
                reader.Dispose();
                if (str.Contains(originalContent))
                {
                    WriteFile(info2.FullName, str.Replace(originalContent, newContent));
                }
            }
        }
        /// <summary>
        /// 查找文件夹下包含查找内容的所有文件
        /// </summary>
        public static DataTable SearchFileContent(string dir, string searchPattern)
        {
            DataTable table = new DataTable();
            DirectoryInfo info = new DirectoryInfo(dir);
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("content_type");
            table.Columns.Add("createTime", typeof(DateTime));
            table.Columns.Add("lastWriteTime", typeof(DateTime));
            FileInfo[] files = info.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo info2 in files)
            {
                DataRow row = table.NewRow();
                StreamReader reader = info2.OpenText();
                string str = reader.ReadToEnd();
                reader.Dispose();
                if (str.Contains(searchPattern))
                {
                    row[0] = info2.FullName.Remove(0, info.FullName.Length);
                    row[1] = 2;
                    row[2] = info2.Length;
                    row[3] = info2.Extension.Replace(".", "");
                    row[4] = info2.CreationTime;
                    row[5] = info2.LastWriteTime;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        /// <summary>
        /// 查找文件夹下某类后缀名的文件
        /// </summary>
        public static DataTable SearchFiles(string dir, string searchPattern)
        {
            DataTable table = new DataTable();
            DirectoryInfo info = new DirectoryInfo(dir);
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("content_type");
            table.Columns.Add("createTime", typeof(DateTime));
            table.Columns.Add("lastWriteTime", typeof(DateTime));
            FileInfo[] files = info.GetFiles(searchPattern, SearchOption.AllDirectories);
            foreach (FileInfo info2 in files)
            {
                DataRow row = table.NewRow();
                row[0] = info2.FullName.Remove(0, info.FullName.Length);
                row[1] = 2;
                row[2] = info2.Length;
                row[3] = info2.Extension.Replace(".", "");
                row[4] = info2.CreationTime;
                row[5] = info2.LastWriteTime;
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 查找目录下的模板文件
        /// </summary>
        public static DataTable SearchTemplateFiles(string dir, string searchPattern)
        {
            DataTable table = new DataTable();
            DirectoryInfo info = new DirectoryInfo(dir);
            string str = searchPattern;
            string str2 = searchPattern.ToLower();
            int length = searchPattern.Length;
            if (length < 4)
            {
                str = "*" + str + "*.html";
            }
            else if ((str2.Substring(length - 4, 4) != ".html") || (str2.Substring(length - 3, 3) != ".htm"))
            {
                str = "*" + str + "*.html";
            }
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("content_type");
            table.Columns.Add("createTime", typeof(DateTime));
            table.Columns.Add("lastWriteTime", typeof(DateTime));
            try
            {
                FileInfo[] files = info.GetFiles(str, SearchOption.AllDirectories);
                foreach (FileInfo info2 in files)
                {
                    DataRow row = table.NewRow();
                    row[0] = info2.FullName.Remove(0, info.FullName.Length).Replace("/", "\"");
                    row[1] = 2;
                    row[2] = info2.Length;
                    row[3] = info2.Extension.Replace(".", "");
                    row[4] = info2.CreationTime;
                    row[5] = info2.LastWriteTime;
                    table.Rows.Add(row);
                }
            }
            catch (ArgumentException)
            {
            }
            return table;
        }
        /// <summary>
        /// 向文件添加内容
        /// </summary>
        public static string WriteAppend(string file, string fileContent)
        {
            string str;
            FileInfo info = new FileInfo(file);
            if (!Directory.Exists(info.DirectoryName))
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            FileStream stream = new FileStream(file, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("utf-8"));
            try
            {
                writer.Write(fileContent);
                str = fileContent;
            }
            catch (Exception exception)
            {
                throw new FileNotFoundException(exception.ToString());
            }
            finally
            {
                writer.Flush();
                stream.Flush();
                writer.Close();
                stream.Close();
            }
            return str;
        }
        /// <summary>
        /// 向文件写入内容
        /// </summary>
        public static string WriteFile(string file, string fileContent)
        {
            string str;
            FileInfo info = new FileInfo(file);
            if (!Directory.Exists(info.DirectoryName))
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            try
            {
                writer.Write(fileContent);
                str = fileContent;
            }
            catch (Exception exception)
            {
                throw new FileNotFoundException(exception.ToString());
            }
            finally
            {
                writer.Flush();
                stream.Flush();
                writer.Close();
                stream.Close();
            }
            return str;
        }
        /// <summary>
        /// 向文件写入内容并根据bool append参数决定是改写还是追加 append=false则改写
        /// </summary>
        public static void WriteFile(string file, string fileContent, bool append)
        {
            FileInfo info = new FileInfo(file);
            if (!Directory.Exists(info.DirectoryName))
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            StreamWriter writer = new StreamWriter(file, append, Encoding.GetEncoding("utf-8"));
            try
            {
                try
                {
                    writer.Write(fileContent);
                }
                catch (Exception exception)
                {
                    throw new FileNotFoundException(exception.ToString());
                }
            }
            finally
            {
                writer.Flush();
                writer.Close();
            }
        }
    }
}
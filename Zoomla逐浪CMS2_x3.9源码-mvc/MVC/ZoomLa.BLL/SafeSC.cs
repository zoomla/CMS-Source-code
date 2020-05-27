using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Safe;
using ZoomLa.Common;

namespace ZoomLa.BLL
{
    /*
     * IO安全控制,上传,下载,写入,读出,写入数据库
     */ 
    public static class SafeSC
    {
        //------------页面
        public static void Submit_Begin(StateBag s)
        {
            SafeC.Submit_Begin(s);
        }
        public static bool Submit_Check(StateBag s)
        {
            return SafeC.Submit_Check(s);
        }
        //------------IO相关(本地文件写入,读取)
        public static void CreateDir(string ppath)
        {
            SafeC.CreateDir(ppath);
        }
        public static void CreateDir(string dirPath, string dirName)
        {
            SafeC.CreateDir(dirPath,dirName);
        }
        public static bool CheckDirName(string dirname)
        {
            return SafeC.CheckDirName(dirname);
        }
        public static bool DelFile(string vpath)
        {
            return SafeC.DelFile(vpath);
        }
        public static bool FileNameCheck(params string[] fnames)
        {
            return SafeC.FileNameCheck(fnames);
        }
        public static bool VPathCheck(string vpath)
        {
            return SafeC.VPathCheck(vpath);
        }
        public static string PathDeal(string path)
        {
            return SafeC.PathDeal(path);
        }
        public static void DirPathDel(ref string vpath)
        {
            SafeC.DirPathDel(ref vpath);
        }
        public static string SaveFile(string vpath, FileUpload file, string fileName = "")
        {
            return SafeC.SaveFile(vpath, file, fileName);
        }
        public static string SaveFile(string vpath, HttpPostedFile file, string fileName="")
        {
            return SafeC.SaveFile(vpath, file, fileName);
        }
        /// <summary>
        /// SafeSC.SaveFile(Path.GetDirectoryName(strFileName) + "\\", Path.GetFileName(strFileName), bytes);
        /// </summary>
        public static string SaveFile(string vpath, string fileName, byte[] file)
        {
            return SafeC.SaveFile(vpath, fileName, file);
        }
        public static string WriteFile(string vpath, string fname, string content)
        {
            return SafeC.WriteFile(vpath, fname, content);
        }
        public static string WriteFile(string vpath, string content)
        {
            return SafeC.WriteFile(vpath,content);
        }
        public static string ReadFileStr(string vpath, bool lines = false)
        {
            if (!File.Exists(function.VToP(vpath))) { return ""; }
            return SafeC.ReadFileStr(vpath, lines);
        }
        public static byte[] ReadFileByte(string vpath)
        {
            return SafeC.ReadFileByte(vpath);
        }
        //------------文件上传下载(上传置入ZoomlaSecurity)
        public static void DownFile(string vpath, string fname = "")
        {
            SafeC.DownFile(vpath, fname);
        }
        public static void DownFile(byte[] bytes, string fname)
        {
            SafeC.DownFile(bytes,fname);
        }
        public static void DownStr(string str,string fname)
        {
            SafeC.DownStr(str,fname);
        }
        //------------数据库相关
        public static bool CheckIDS(string ids)
        {
            return SafeC.CheckIDS(ids);
        }
        public static void CheckIDSEx(string ids)
        {
            SafeC.CheckIDSEx(ids);
        }
        public static bool CheckData(string inputData)
        {
            return SafeC.CheckData(inputData);
        }
        public static void CheckDataEx(params string[] inputData) 
        {
            SafeC.CheckDataEx(inputData);
        }
        //------------XSS
        public static string RemoveXss(string input)
        {
            return SafeC.RemoveXss(input);
        }
        //------------其它
        public static bool CheckUName(string uname)
        {
            return SafeC.CheckUName(uname);
        }
        public static bool IsImage(string file) 
        {
            return SafeC.IsImage(file);
        }
    }
}

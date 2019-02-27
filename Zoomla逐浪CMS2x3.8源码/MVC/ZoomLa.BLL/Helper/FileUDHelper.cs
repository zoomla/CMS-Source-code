using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ZoomLa.Common;
/*
 * 文件的上传下载支持类(服务端发送文件)
 */

namespace ZoomLa.BLL.Helper
{
    public class FileUDHelper
    {
        /// <summary>
        /// 下载文件至服务器,建议异步调用
        /// </summary>
        /// <param name="url">http文件路径</param>
        /// <param name="vpath">保存,虚拟路径,带文件名</param>
        /// <param name="flag">标记,用于记录状态(Session)</param>
        public static void DownFile(string url, string vpath, ProgMod model)
        {
            string ppath = function.VToP(vpath);
            if (SafeSC.FileNameCheck(ppath)) { throw new Exception(vpath + "文件不允许下载"); }
            //已下载字节,每一进度是多少字节,每次下载多少字节,进度条状态,
            int byteLen = 2048;
            FileStream FStream;
            if (File.Exists(ppath)) { File.Delete(ppath); }
            FStream = new FileStream(ppath, FileMode.Create);
            //打开网络连接
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
                model.Percent = webResponse.ContentLength / 100;
                Stream myStream = webResponse.GetResponseStream();
                byte[] btContent = new byte[byteLen];
                //开始写入
                int count = 0;
                while ((count = myStream.Read(btContent, 0, byteLen)) > 0)//返回读了多少字节,为0表示全部读完
                {
                    FStream.Write(btContent, 0, count);//知道有多少个数字节后再写入
                    model.CompleteLen += count;
                    model.UpdateProg();
                }
                myStream.Close();
            }
            finally
            {
                FStream.Close();
            }
        }
        public static void UploadFile(Stream stream, ProgMod model) { }
    }
    //进度类,用于上传下载,或压缩
    public class ProgMod
    {
        public string ProgStatus = "0";
        public long FileLen = 0;//总长度
        public long CompleteLen = 0;
        public long Percent = 0;
        public void UpdateProg()
        {
            ProgStatus = (CompleteLen / Percent).ToString();
        }
    }
}

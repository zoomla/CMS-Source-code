using System;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;
using ZoomLa.Common;
namespace ZoomLa.BLL
{
    /// <summary>
    /// B_UpLoadFile 的摘要说明
    /// </summary>
    public class B_UpLoadFile
    {
        public B_UpLoadFile()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public string GetUpLoadFile(HtmlInputFile FilePicName, string FilePicPath, int FileSize)
        {
            if ((FileSize != 0) && (FilePicName.PostedFile.ContentLength > (FileSize * 0x400)))
            {
                HttpContext.Current.Response.Write("<script>alert('文件大小超出!');window.close();</script>");
                HttpContext.Current.Response.End();
            }
            switch (Path.GetFileName(FilePicName.PostedFile.FileName))
            {
                case "":
                case null:
                    HttpContext.Current.Response.Write("<script language=javascript>alert('文件名不能为空!');window.close();</script>");
                    HttpContext.Current.Response.End();
                    break;
            }
            if ((((((FilePicName.PostedFile.ContentType != "image/gif") && (FilePicName.PostedFile.ContentType != "image/pjpeg")) && ((FilePicName.PostedFile.ContentType != "image/bmp") && (FilePicName.PostedFile.ContentType != "image/x-png"))) && (((FilePicName.PostedFile.ContentType != "image/jpeg") && (FilePicName.PostedFile.ContentType != "application/x-shockwave-flash")) && ((FilePicName.PostedFile.ContentType != "application/vnd.ms-excel") && (FilePicName.PostedFile.ContentType != "application/msword")))) && ((((FilePicName.PostedFile.ContentType != "application/vnd.ms-powerpoint") && (FilePicName.PostedFile.ContentType != "application/octet-stream")) && ((FilePicName.PostedFile.ContentType != "application/x-zip-compressed") && (FilePicName.PostedFile.ContentType != "pplication/vnd.rn-realmedia"))) && (((FilePicName.PostedFile.ContentType != "application/vnd.rn-realmedia-vbr") && (FilePicName.PostedFile.ContentType != "video/x-ms-wmv")) && ((FilePicName.PostedFile.ContentType != "audio/x-ms-wma") && (FilePicName.PostedFile.ContentType != "video/x-ms-asf"))))) && ((((FilePicName.PostedFile.ContentType != "video/avi") && (FilePicName.PostedFile.ContentType != "audio/mp3")) && ((FilePicName.PostedFile.ContentType != "video/mpeg4") && (FilePicName.PostedFile.ContentType != "video/mpg"))) && (((FilePicName.PostedFile.ContentType != "audio/mid") && (FilePicName.PostedFile.ContentType != "video/avi")) && ((FilePicName.PostedFile.ContentType != "application/x-rar-compressed") && (FilePicName.PostedFile.ContentType != "application/x-zip-compressed")))))
            {
                HttpContext.Current.Response.Write("<script>alert('文件类型不是允许上传的类型');window.close();</script>");
                HttpContext.Current.Response.End();
                return "";
            }
            string extension = Path.GetExtension(FilePicName.PostedFile.FileName);
            string fileName = function.GetFileName();
            string str4 = DateTime.Now.ToString("yyyyMM");
            string path = HttpContext.Current.Server.MapPath(FilePicPath) + str4 + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + fileName + extension;
            FilePicName.PostedFile.SaveAs(filename);
            return (str4 + "/" + fileName + extension);
        }
    }
}
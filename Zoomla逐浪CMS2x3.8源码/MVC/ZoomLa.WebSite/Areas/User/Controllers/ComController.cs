using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Controls;
using ZoomLa.Model;
using ZoomLa.Safe;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ComController : Controller
    {
        B_User buser = new B_User();
        B_Admin badmin = new B_Admin();
        public string SaveFile()
        {
            C_SFileUP model = JsonConvert.DeserializeObject<C_SFileUP>(Request.Form["model"]);
            HttpPostedFileBase file = Request.Files["file"];
            string result = "";
            if (file.ContentLength < 100 || string.IsNullOrEmpty(file.FileName)) { return ""; }
            string fname = DateTime.Now.ToString("yyyyMMddHHmm") + function.GetRandomString(4) + Path.GetExtension(file.FileName);
            switch (model.FileType)
            {
                case "img":
                    {
                        // function.WriteErrMsg(Path.GetExtension(file.FileName) + "不是有效的图片格式!");
                        if (!SafeSC.IsImage(file.FileName)) { return ""; }
                        ImgHelper imghelp = new ImgHelper();
                        //if (IsCompress)//压缩与最大比只能有一个生效
                        //{
                        //    imghelp.CompressImg(FileUp_File.PostedFile, 1000, vpath);
                        //}
                        bool hasSave = false;
                        if (model.MaxWidth > 0 || model.MaxHeight > 0)
                        {
                            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                            img = imghelp.ZoomImg(img, model.MaxHeight, model.MaxWidth);
                            result = ImgHelper.SaveImage(GetSaveDir(model.SaveType) + fname, img);
                            hasSave = true;
                        }
                        if (!hasSave) { result = SafeC.SaveFile(GetSaveDir(model.SaveType), fname, file.InputStream, file.ContentLength); }
                    }
                    break;
                case "office":
                    {
                        string[] exname = "doc|docx|xls|xlsx".Split('|');
                        if (!exname.Contains(Path.GetExtension(file.FileName))) { function.WriteErrMsg("必须上传doc|docx|xls|xlsx格式的文件!"); return ""; }
                       // result = SafeC.SaveFile(GetSaveDir(model.SaveType), fname, file.InputStream, file.ContentLength);
                    }
                    break;
                case "all":
                default:
                    {
                       // result = SafeC.SaveFile(GetSaveDir(model.SaveType), fname, file.InputStream, file.ContentLength);
                    }
                    break;
            }
            return result;
        }
        //获取可保存到的目录,不含文件名
        private string GetSaveDir(string type)
        {
            string vpath = SiteConfig.SiteOption.UploadDir.TrimEnd('/') + "/";
            switch (type)
            {
                case "admin":
                    {
                        M_AdminInfo adminMod = badmin.GetAdminLogin();
                        if (adminMod.IsNull) { throw new Exception("管理员未登录,无法上传文件"); }
                        vpath += "Admin/" + adminMod.AdminName + adminMod.AdminId + "/";
                    }
                    break;
                case "visitor":
                    {
                        vpath += "User/NoLogin/";
                    }
                    break;
                case "user":
                default:
                    {
                        M_UserInfo mu = buser.GetLogin();
                        if (mu.IsNull) { throw new Exception("用户未登录,无法上传文件"); }
                        vpath += "User/" + mu.UserName + mu.UserID + "/";
                    }
                    break;
            }
            return vpath;
        }

    }
}

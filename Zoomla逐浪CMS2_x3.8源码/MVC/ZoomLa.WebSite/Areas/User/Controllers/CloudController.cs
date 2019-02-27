using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Models.Cloud;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class CloudController : ZLCtrl
    {
        B_User_Cloud cloudBll = new B_User_Cloud();
        public ActionResult Index()
        {
            var model = new VM_Cloud(CPage, PSize, mu, Request);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Cloud_List", model);
            }
            return View(model);
        }
        public void Cloud_Open()
        {
            string baseDir = "/UploadFiles/Cloud/" + mu.UserName + mu.UserID + "/";
            string pathfile = baseDir + "我的文档/";
            string pathphoto = baseDir + "我的相册/";
            string pathmusic = baseDir + "我的音乐/";
            string pathvideo = baseDir + "我的视频/";
            Directory.CreateDirectory(function.VToP(pathfile));
            Directory.CreateDirectory(function.VToP(pathphoto));
            Directory.CreateDirectory(function.VToP(pathmusic));
            Directory.CreateDirectory(function.VToP(pathvideo));
            buser.UpdateIsCloud(mu.UserID, 1);
            function.WriteSuccessMsg("云盘开通成功", "Index"); return;
        }
        public int Cloud_NewDir()
        {
            var model = new VM_Cloud(mu, Request);
            M_User_Cloud cloudMod = new M_User_Cloud();
            cloudMod.FileName = Request.Form["DirName_T"];
            cloudMod.VPath = model.CurrentDir;
            cloudMod.UserID = mu.UserID;
            cloudMod.FileType = 2;
            SafeSC.CreateDir(Server.MapPath(cloudMod.VPath), cloudMod.FileName);
            cloudBll.Insert(cloudMod);
            return Success;
        }
        public int Cloud_Del(string id)
        {
            M_User_Cloud cloudMod = cloudBll.SelReturnModel(id);
            if (cloudMod == null) { return -1; }
            if (cloudMod.FileType == 1)
            {
                FileSystemObject.Delete(Server.MapPath(cloudMod.VPath + cloudMod.SFileName), FsoMethod.File);
            }
            else
            {
                FileSystemObject.Delete(Server.MapPath(cloudMod.VPath + cloudMod.FileName), FsoMethod.Folder);
            }
            cloudBll.DelByFile(cloudMod.Guid);
            return 1;
        }
    }
}

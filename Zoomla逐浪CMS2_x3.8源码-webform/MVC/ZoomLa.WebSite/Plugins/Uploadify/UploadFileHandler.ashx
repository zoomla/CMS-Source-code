<%@ WebHandler Language="C#" Class="UploadFileHandler" %>
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Linq;
using ZoomLa.BLL;
using ZoomLa.BLL.FTP;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.FTP;
using ZoomLa.Model.Plat;
using ZoomLa.Model.Design;
using ZoomLa.Safe;
/*
 *用于文件工厂,OA,Plat附件上传
 */

public class UploadFileHandler : IHttpHandler, IRequiresSessionState
{
    M_DocModel model = new M_DocModel();
    M_UserInfo mu = new M_UserInfo();
    B_DocModel bll = new B_DocModel();
    B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    ZipClass zipBll = new ZipClass();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        if (!buser.CheckLogin() && !badmin.CheckLogin()) { throw new Exception("未登录"); }
        HttpPostedFile file = context.Request.Files["Filedata"];
        if (file == null)
        {
            file = context.Request.Files["file"];//接受Uploadify或WebUploader传参,优先Uploadify 
        }
        if (file==null||file.ContentLength < 1) { return; }
        if (SafeSC.FileNameCheck(file.FileName))
        {
            throw new Exception("不允许上传该后缀名的文件");
        }
        /*-------------------------------------------------------------------------------------------*/
        mu = buser.GetLogin();
        string uploadPath = SiteConfig.SiteOption.UploadDir.TrimEnd('/') + "/", filename = "", ppath = "", result = "0";//上传根目录,文件名,上物理路径,结果
        string action = (context.Request["action"] ?? ""), value = context.Request["value"];
        try
        {
            switch (action)
            {
                case "OAattach"://OA--公文||事务--附件
                    uploadPath += "OA/" + mu.UserName + mu.UserID + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                    ppath = context.Server.MapPath(uploadPath);
                    //判断是否有同名文件的存在
                    break;
                case "Blog"://能力中心--博客
                    uploadPath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Blog);
                    ppath = context.Server.MapPath(uploadPath);
                    break;
                case "Plat_Doc"://能力中心--我的文档
                    uploadPath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Person) + SafeSC.PathDeal(context.Request["Dir"]);
                    ppath = context.Server.MapPath(uploadPath);
                    break;
                case "Plat_Doc_Common"://能力中心--公司文档
                    uploadPath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Company) + SafeSC.PathDeal(context.Request["Dir"]);
                    ppath = context.Server.MapPath(uploadPath);
                    break;
                case "Plat_Task"://能力中心--任务中心附件
                    int tid = Convert.ToInt32(value);
                    ZoomLa.Model.Plat.M_Plat_Task taskMod = new B_Plat_Task().SelReturnModel(tid);
                    uploadPath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Plat_Task) + taskMod.TaskName + "/";
                    break;
                case "Plat_Project"://能力中心--项目
                    int pid = Convert.ToInt32(value);
                    ZoomLa.Model.Plat.M_Plat_Pro proMod = new B_Plat_Pro().SelReturnModel(pid);
                    uploadPath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Plat_Task) + proMod.Name + "/";
                    break;
                case "ModelFile"://组图,多图等
                    int nodeid = Convert.ToInt32(value);
                    M_Node nodeMod = new B_Node().GetNodeXML(nodeid);
                    string exname = Path.GetExtension(file.FileName).Replace(".", "");
                    string fpath = nodeMod.NodeDir + "/" + exname + "/" + DateTime.Now.ToString("yyyy/MM/");
                    filename = DateTime.Now.ToString("HHmmss") + function.GetRandomString(6, 2) + "." + exname;
                    uploadPath += fpath;
                    break;
                case "design"://动力模块
                    {
                        //int siteID = DataConverter.CLng(context.Request["SiteID"]);
                        if (mu.SiteID > 0)
                        {
                            B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
                            M_Design_SiteInfo sfMod = sfBll.SelModelByUid(mu.UserID);
                            uploadPath = B_Design_SiteInfo.GetSiteUpDir(mu.SiteID);
                        }
                        else { uploadPath = "/UploadFiles/Design/Tlp/"; }
                        //siteID为0进入普通目录,用于设计(后期做划分)
                    }
                    break;
                default://通常格式,不需做特殊处理的格式但必须登录
                    if (mu.UserID > 0)
                    {
                        uploadPath = context.Server.UrlDecode(uploadPath + "User/" + mu.UserName + mu.UserID + "/");
                    }
                    else if (badmin.CheckLogin())
                    {
                        M_AdminInfo adminMod = badmin.GetAdminLogin();
                        uploadPath = context.Server.UrlDecode(uploadPath + "Admin/" + adminMod.AdminName + adminMod.AdminId + "/");
                    }
                    else { throw new Exception("当前用户未登录"); }
                    uploadPath += DateTime.Now.ToString("yyyyMMdd") + "/";
                    break;
            }
            if (!Directory.Exists(function.VToP(uploadPath))) { Directory.CreateDirectory(function.VToP(uploadPath)); }
            if (action.Equals("Plat_Doc") || action.Equals("Plat_Doc_Common"))
            {
                M_Plat_File fileMod = new M_Plat_File();
                B_Plat_File fileBll = new B_Plat_File();
                M_User_Plat upMod = B_User_Plat.GetLogin();
                fileMod.FileName = file.FileName;
                fileMod.SFileName = function.GetRandomString(12) + Path.GetExtension(file.FileName);
                fileMod.VPath = uploadPath.Replace("//","/");
                fileMod.UserID = upMod.UserID.ToString();
                fileMod.CompID = upMod.CompID;
                SafeSC.SaveFile(uploadPath, file, fileMod.SFileName);
                fileMod.FileSize = new FileInfo(ppath + fileMod.SFileName).Length.ToString();
                fileBll.Insert(fileMod);
            }
            else if (action.Equals("Cloud_Doc"))
            {
                if (!buser.CheckLogin()) { throw new Exception("云盘,用户未登录"); }
                M_User_Cloud cloudMod = new M_User_Cloud();
                B_User_Cloud cloudBll = new B_User_Cloud();
                uploadPath = context.Server.UrlDecode(cloudBll.H_GetFolderByFType(context.Request["type"], mu)) + context.Request["value"];
                cloudMod.FileName = file.FileName;
                cloudMod.SFileName = function.GetRandomString(12) + Path.GetExtension(file.FileName);
                cloudMod.VPath = (uploadPath + "/").Replace("//", "/");
                cloudMod.UserID = buser.GetLogin().UserID;
                cloudMod.FileType = 1;
                result = SafeSC.SaveFile(cloudMod.VPath, file, cloudMod.SFileName);
                //if (SafeSC.IsImage(cloudMod.SFileName))
                //{
                //    string icourl = SiteConfig.SiteOption.UploadDir + "YunPan/" + mu.UserName + mu.UserID + "/ico" + value + "/";
                //    if (!Directory.Exists(function.VToP(icourl))) { Directory.CreateDirectory(function.VToP(icourl)); }
                //    ImgHelper imghelp = new ImgHelper();
                //    imghelp.CompressImg(file, 100, icourl + cloudMod.SFileName);
                //}
                cloudMod.FileSize = new FileInfo(context.Server.MapPath(cloudMod.VPath)+cloudMod.SFileName).Length.ToString();
                cloudBll.Insert(cloudMod);
            }
            else
            {
                switch ((context.Request["upmode"] ?? ""))//组图,多图上传
                {
                    case "1"://压缩包上传(默认不开启压缩包,避免大包攻击)
                        return;
                    //string zipvpath = SafeC.SaveFile(uploadPath, file, Path.GetFileName(file.FileName));
                    //string imgpdir = function.VToP(uploadPath + Path.GetFileNameWithoutExtension(file.FileName)) + @"\";
                    //zipBll.UnZipFiles(function.VToP(zipvpath), imgpdir);
                    ////移除目录下的非图片文件
                    //DataTable dirDT = FileSystemObject.GetDirectoryInfos(imgpdir, FsoMethod.All);
                    //foreach (DataRow dr in dirDT.Rows)
                    //{
                    //    if (!SafeC.IsImage(dr["Name"].ToString()))
                    //    {
                    //        SafeC.DelFile(function.PToV(dr["FullPath"].ToString()));
                    //    }
                    //}
                    //DataTable imgdt = FileSystemObject.SearchImg(imgpdir);
                    //result = "";
                    //foreach (DataRow dr in imgdt.Rows)
                    //{
                    //    result += dr["Path"] + "|";
                    //}
                    //result = result.TrimEnd('|');
                    //break;
                    default:
                        if (SafeC.IsImageFile(file.FileName) && file.ContentLength > (5 * 1024 * 1024))//图片超过5M则压缩
                        {
                            result = uploadPath + function.GetRandomString(6) + CreateFName(file.FileName);
                            new ImgHelper().CompressImg(file, 5 * 1024, result);
                        }
                        else
                        {
                            result = SafeSC.SaveFile(uploadPath, file, CreateFName(file.FileName));
                        }
                        //添加水印
                        if (context.Request["IsWater"] != null && context.Request["IsWater"].Equals("1"))
                        { result = ImgHelper.AddWater(result); }
                        break;
                }
            }
            ZLLog.L(ZLEnum.Log.fileup,new M_Log()
            {
                UName = mu.UserName,
                Source = context.Request.RawUrl,
                Message = "上传成功|文件名:" + file.FileName + "|" + "保存路径:" + uploadPath
            });
            //else if (SafeC.IsImageFile(file.FileName) && file.ContentLength > 500 * 1024)//图片文件先压缩再保存,500K以上才压
            //{
            //    string exname = Path.GetExtension(file.FileName).ToLower();
            //    result = uploadPath + file.FileName;
            //    ImgHelper imghelper = new ImgHelper();
            //    imghelper.CompressImg(file, result);
            //}
        }
        catch (Exception ex)
        {
            ZLLog.L(ZLEnum.Log.fileup, new M_Log()
            {
                UName = mu.UserName,
                Source = context.Request.RawUrl,
                Message = "上传失败|文件名:" + file.FileName + "|" + "原因:" + ex.Message
            });
        }
        context.Response.Write(result); context.Response.End();
    }
    private string CreateFName(string fname)
    {
        return DateTime.Now.ToString("yyyyMMdd") + function.GetRandomString(6) + Path.GetExtension(fname);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
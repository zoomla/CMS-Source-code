using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Plat.Doc
{
    public partial class DownFile : System.Web.UI.Page
    {
        /*
     * 文件下载页,用于Plat文档中心,ZL_Webup.js,云盘
     * 动力模块(用户站点)
     */
        B_User buser = new B_User();
        B_Plat_File fileBll = new B_Plat_File();
        B_User_Cloud cloudBll = new B_User_Cloud();
        //下载类型
        public string DType { get { return Request.QueryString["DType"] ?? ""; } }
        public string FName { get { return Request.QueryString["FName"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                switch (DType)
                {
                    case "design":
                        {
                            int siteID = DataConverter.CLng(Request.QueryString["SiteID"]);
                            string vpath = B_Design_SiteInfo.GetSiteUpDir(siteID) + FName;
                            if (siteID < 1 || string.IsNullOrEmpty(FName)) { function.WriteErrMsg("文件路径不正确"); }
                            SafeSC.DownFile(vpath);
                        }
                        break;
                    default:
                        if (!string.IsNullOrEmpty(Request["File"]))//仅用于云盘Guid
                        {
                            string guid = Request.QueryString["File"];
                            M_Plat_File fileMod = fileBll.SelReturnModel(guid);
                            SafeSC.DownFile(fileMod.VPath + fileMod.SFileName, fileMod.FileName);
                        }
                        else if (!string.IsNullOrEmpty(Request["CloudFile"]))
                        {
                            string guid = Request["CloudFile"];
                            M_User_Cloud cloudMod = cloudBll.SelReturnModel(guid);
                            SafeSC.DownFile(cloudMod.VPath + cloudMod.SFileName, cloudMod.FileName);
                        }
                        else if (!string.IsNullOrEmpty(FName))//其他通过来源和文件名下载
                        {
                            string fname = FName.ToLower().Replace("/UploadFiles/", "");
                            SafeSC.DownFile("/UploadFiles/" + fname);
                        }
                        break;
                }
            }
        }
    }
}
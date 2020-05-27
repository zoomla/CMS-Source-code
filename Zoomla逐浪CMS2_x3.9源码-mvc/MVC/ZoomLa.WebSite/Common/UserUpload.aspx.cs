using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
//用户中心独有上传页,仅用于用户中心图片上传

namespace ZoomLaCMS.Common
{
    public partial class UserUpload : System.Web.UI.Page
    {
        private int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
        private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        private string FieldName { get { return Request.QueryString["FieldName"] ?? ""; } }
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteConfig.SiteOption.EnableUploadFiles) { ShowRepStr("本站不允许上传文件!"); }
            if (!buser.CheckLogin()) { ShowRepStr("您没有权限访问该页面!"); }
        }
        protected void FileUp_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            if (!FileUp_File.HasFile) { function.Script(this, "请指定一个上传文件"); return; }
            string ext = Path.GetExtension(FileUp_File.FileName).ToLower();
            //if (ext.Equals(".html") || ext.Equals(".html") || ext.Equals(".shtml")) { function.Script(this, "不允许上传html文件"); return; }

            string fname = DataSecurity.MakeFileRndName() + ext;
            string dirPath = SiteConfig.SiteOption.UploadDir + "UserUpload/" + mu.UserName + mu.UserID + "/";
            string savePath = SafeSC.SaveFile(dirPath, FileUp_File, fname);
            if (WaterModuleConfig.WaterConfig.IsUsed && SafeSC.IsImage(FileUp_File.FileName))
            {
                savePath = ImgHelper.AddWater(savePath);
            }
            function.Script(this, "ZLIfrField.UploadPic(\"" + savePath + "\",\"" + FieldName + "\");");
            LblMsg_L.Text = "文件上传成功";
        }
        //Tools
        private void ShowRepStr(string msg) { Response.Write(msg); Response.Flush(); Response.End(); }
    }
}
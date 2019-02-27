using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;
/*
 * 支持选择图片和视频
 */ 

namespace ZoomLaCMS.Common
{
    public partial class SelFiles : System.Web.UI.Page
    {
        B_Content_Video videoBll = new B_Content_Video();
        public string FilePath { get { return SafeSC.PathDeal(Request.QueryString["cururl"]); } }
        public string VPath { get { return SiteConfig.SiteOption.UploadDir + FilePath; } }
        public string name { get { return Request.QueryString["name"]; } }
        public string Action
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["action"]))
                    return "";
                else
                    return Request.QueryString["action"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                MyBind();
                PVal_Hid.Value = Request.QueryString["pval"];
            }
        }
        public void MyBind()
        {
            DataTable filedt = null;
            if (Action.Equals("dbvideo"))
                filedt = videoBll.SelForFile();
            else
                filedt = FileSystemObject.SearchImg(function.VToP(VPath));

            string strwhere = "";
            if (!string.IsNullOrEmpty(ImgName_T.Text))
                strwhere = "Name LIKE '%" + ImgName_T.Text + "%' AND ";
            if (!string.IsNullOrEmpty(Sdate_T.Text) && !string.IsNullOrEmpty(Edate_T.Text))
                strwhere += "CreateTime >= '" + Sdate_T.Text + "' AND CreateTime <= '" + Edate_T.Text + "' AND ";
            strwhere += "1=1";
            filedt.DefaultView.RowFilter = strwhere;
            File_RPT.DataSource = filedt.DefaultView.ToTable();
            File_RPT.DataBind();
        }
        public string GetFileInfo()
        {
            if (!string.IsNullOrEmpty(Eval("Path").ToString()))
                return "<img src='" + function.PToV(Eval("Path").ToString()) + "' />";
            else
                return "<img src='/Images/nopic.gif' />";
        }
        public string GetVpath()
        {
            if (Action.Equals("dbvideo"))
                return Eval("FilePath").ToString();
            return function.PToV(Eval("Path").ToString());
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            File_RPT.CPage = 1;
            MyBind();
        }
        protected void FIleUp_B_Click(object sender, EventArgs e)
        {
            //{yyyy}{mm}{dd}/{time}{rand:6}
            string url = SafeSC.SaveFile(SiteConfig.SiteOption.UploadDir + DateTime.Now.ToString("yyyyMMdd"), FileUp_F);
            function.Script(this, "ShowUpFile('" + url + "');");
        }
    }
}
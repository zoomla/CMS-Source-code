using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Sentiment
{
    public partial class ReportList : System.Web.UI.Page
    {
        private string BaseDir = "/UploadFiles/Report/";
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (!Directory.Exists(function.VToP(BaseDir)))
            {
                SafeSC.CreateDir(BaseDir);
            }
            DataTable fileList = FileSystemObject.SearchWord(function.VToP(BaseDir));
            EGV.DataSource = fileList;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DownFiles")
            {
                SafeSC.DownFile(BaseDir + e.CommandArgument.ToString());
            }
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string names = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(names))
            {
                foreach (string name in names.Split(','))
                {
                    SafeSC.DelFile(BaseDir + name);
                }
                function.WriteSuccessMsg("操作成功");
            }
            else { function.Script(this, "alert('请先选择文档');"); }
        }
    }
}
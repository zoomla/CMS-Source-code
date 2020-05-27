using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Plat
{
    public partial class PlatInfoManage : System.Web.UI.Page
    {
        B_Blog_Msg msgBll = new B_Blog_Msg();

        public int PlatStauts
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["status"]) ? 1 : DataConverter.CLng(Request.QueryString["status"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + Request.RawUrl + "'>能力中心</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a>  [<a href='PlatInfoManage.aspx?status=0'>回收站</a>]</li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = msgBll.Sel(0);
            dt.DefaultView.RowFilter = "Status=" + PlatStauts;
            Rels.Visible = (PlatStauts == 0);
            Clear_Btn.Visible = (PlatStauts == 0);
            EGV.DataSource = dt.DefaultView.ToTable();
            EGV.DataBind();
        }
        public string getText()
        {
            string str = StringHelper.StripHtml(Eval("MsgContent").ToString(), 500).Replace(" ", "");
            return str.Length > 30 ? str.Substring(0, 29) + "..." : str;
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Dels_Click(object sender, EventArgs e)
        {
            string ids = string.IsNullOrEmpty(Request.Form["idchk"]) ? "" : Request.Form["idchk"];
            msgBll.DelByIds(ids, PlatStauts == 0);
            MyBind();
        }
        public string GetStatus()
        {
            string result = "";
            switch (Eval("Status").ToString())
            {
                case "0":
                    result = "<span style='color:red;'>前台删除</span>";
                    break;
                case "1":
                    result = "<span style='color:green;'>正常</span>";
                    break;
            }
            return result;
        }
        protected void Rels_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                msgBll.RelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            msgBll.ClearRecycle();
            MyBind();
        }
    }
}
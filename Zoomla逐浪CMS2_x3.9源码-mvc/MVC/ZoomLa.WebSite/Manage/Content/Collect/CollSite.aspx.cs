using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model.Site;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollSite : System.Web.UI.Page
    {
        B_IDC_Sites bll = new B_IDC_Sites();
        M_IDC_Sites mod = new M_IDC_Sites();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../'Content/CollectionManage.aspx'>采集检索</a></li><li>统一检索</li><li>[<a href='../Content/AddSites.aspx' style=" + "color:red" + ">添加检索源</a>]</li>");
        }
        private void MyBind()
        {
            DataTable dt = new DataTable();
            dt = bll.Sel();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    int ID = Convert.ToInt32(e.CommandArgument);
                    if (bll.Del(ID))
                    {
                        function.WriteSuccessMsg("删除成功！", "CollSite.aspx");
                    }
                    break;
            }
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int ID = Convert.ToInt32(chkArr[i]);
                    bll.Del(ID);
                }
            }
            MyBind();
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}
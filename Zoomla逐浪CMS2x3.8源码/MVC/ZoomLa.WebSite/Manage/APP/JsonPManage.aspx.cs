using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;
using ZoomLa.Model.CreateJS;

namespace ZoomLaCMS.Manage.APP
{
    public partial class JsonPManage : CustomerPageAction
    {
        B_API_JsonP apiBll = new B_API_JsonP();
        M_API_JsonP apiMod = new M_API_JsonP();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "user/UserManage.aspx'>用户管理</a></li><li class='active'>移动接口[<a href='AddJsonP.aspx'>添加调用</a>]</li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = apiBll.Sel();
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
            switch (e.CommandName)
            {
                case "del2":
                    break;
            }
            MyBind();
        }
        protected void Dels_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                apiBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
        //获得调用js的方法
        public string GetJsLink()
        {
            return "/Tools/API.html?alias=" + Eval("Alias") + "&params=" + Server.UrlEncode(Eval("Params").ToString());
        }
        public string GetStr(object obj)
        {
            return StringHelper.SubStr(obj.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class WxAppManage : CustomerPageAction
    {
        B_WX_APPID WxBll = new B_WX_APPID();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>微信管理 [<a href='WxConfig.aspx'>添加公众号</a>]</li>");
            }
        }
        public void MyBind()
        {
            Egv.DataSource = WxBll.Sel();
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    WxBll.Del(DataConverter.CLng(e.CommandArgument));
                    function.Script(this, "DelWxApp(" + e.CommandArgument + ");");
                    MyBind();
                    break;
                case "Default":
                    WxBll.SetDefault(DataConverter.CLng(e.CommandArgument));
                    MyBind();
                    break;
                default:
                    break;
            }
        }
        public string GetDefault()
        {
            int isdefault = DataConverter.CLng(Eval("IsDefault"));
            return isdefault == 1 ? "<span class='fa fa-check'></span>" : "";
        }
        protected void Dels_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                WxBll.DelByIds(Request.Form["idchk"]);
                MyBind();
                function.Script(this, "DelsWxApp('" + Request.Form["idchk"] + "');");
            }
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='WxConfig.aspx?ID=" + (e.Row.DataItem as DataRowView)["ID"] + "'");
            }
        }
    }
}
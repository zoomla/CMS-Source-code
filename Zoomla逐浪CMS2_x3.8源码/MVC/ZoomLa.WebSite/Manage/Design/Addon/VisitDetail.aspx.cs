using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Design.Addon
{
    public partial class VisitDetail : CustomerPageAction
    {
        B_User buser = new B_User();
        B_Com_VisitCount visitBll = new B_Com_VisitCount();
        public string InfoTitle { get { return Request.QueryString["title"] ?? ""; } }
        public string InfoId { get { return Request.QueryString["id"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                string looklink = "/design/h5/preview.aspx?id=" + InfoId;
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Default.aspx'>动力模块</a></li><li><a href='../SceneList.aspx'>场景列表</a></li><li><a href='VisitCount.aspx'>统计访问</a></li><li class='active'>[" + InfoTitle + "]的访问详情</li><div class='pull-right hidden-xs'><a href='" + looklink + "' target='_blank' title='浏览场景'><i class='fa fa-eye'></i></a></div>");
            }
        }
        public void MyBind()
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(InfoId))
            {
                dt = visitBll.SelByInfoID(InfoId);
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                //e.Row.Attributes.Add("ondblclick", "location='VisitCount.aspx?view=detail&skey=" + dr["InfoTitle"] + "'");
            }
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
                case "del":
                    visitBll.Del(DataConverter.CLng(e.CommandArgument));
                    break;
            }
            MyBind();
        }

        public string GetUser()
        {
            int uid = DataConverter.CLng(Eval("UserID", ""));
            if (uid <= 0) { return "游客"; }
            else
            {
                M_UserInfo mu = buser.SelReturnModel(uid);
                return "<a href='javascript:;' onclick='showuser(" + mu.UserID + ");' >" + mu.UserName + "</a>";
            }
        }
        public string GetIpLocation()
        {
            return IPScaner.IPLocation(Eval("IP", ""), "@province|@city", true);
        }
        public string GetSEType()
        {
            string ztype = Eval("ztype", "");
            switch (ztype)
            {
                case "mbh5":
                    return "微场景";
                case "h5":
                    return "PC场景";
                default:
                    return "";
            }
        }
    }
}
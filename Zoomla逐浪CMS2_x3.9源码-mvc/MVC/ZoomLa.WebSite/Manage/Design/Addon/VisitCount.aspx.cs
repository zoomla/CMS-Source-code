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
    public partial class VisitCount : CustomerPageAction
    {
        B_Com_VisitCount visitBll = new B_Com_VisitCount();
        B_User buser = new B_User();
        public string ZType { get { return Request.QueryString["type"] ?? "h5,mbh5"; } }
        public string Skey { get { return Request.QueryString["skey"] ?? ""; } }
        public string view { get { return Request.QueryString["view"] ?? "detail"; } }
        public int UserID { get { return DataConverter.CLng(Request.QueryString["userid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind(Skey);
            }
        }

        public void MyBind(string infotitle = "")
        {
            switch (view)
            {
                case "detail":
                    {
                        EGV.DataSource = visitBll.SelByType(ZType, infotitle, UserID);
                        EGV.DataBind();
                    }
                    break;
                case "overview":
                    {
                        EGV.Visible = false;
                        rpt_div.Visible = true;
                        DataTable dt = visitBll.SelForSum("h5,mbh5", infotitle);
                        if (dt.Rows.Count <= 0) { Empty_Tr.Visible = true; Title_Tr.Visible = false; }
                        RPT.DataSource = dt;
                        RPT.DataBind();
                    }
                    break;
            }
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind(Skey);
        }

        public string GetUser()
        {
            int uid = DataConverter.CLng(Eval("UserID", ""));
            if (uid <= 0) { return "游客"; }
            else
            {
                M_UserInfo mu = buser.SelReturnModel(uid);
                return "<a href='VisitCount.aspx?userid=" + mu.UserID + "' >" + mu.UserName + "</a>";
            }
        }
        public string GetIpLocation()
        {
            return IPScaner.IPLocation(Eval("IP", ""), "@province|@city", true);
        }
        public string GetType()
        {
            string ztype = Eval("ztype", "");
            switch (ztype)
            {
                case "mbh5":
                    return "<a href='VisitCount.aspx?type=mbh5'>微场景</a>";
                case "h5":
                    return "<a href='VisitCount.aspx?type=h5'>PC场景</a>";
                default:
                    return "";
            }
        }

        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del":
                    visitBll.Del(DataConverter.CLng(e.CommandArgument));
                    break;
            }
            MyBind(Skey);
        }

        protected void souchok_Click(object sender, EventArgs e)
        {
            string skey = souchkey.Text.Trim(' ');
            MyBind(skey);
            if (!string.IsNullOrEmpty(skey))
            {
                function.Script(this, "showsel();");
            }
        }


        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='VisitCount.aspx?view=detail&skey=" + dr["InfoTitle"] + "'");
            }
        }
    }
}
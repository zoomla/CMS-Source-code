using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model.Design;

namespace ZoomLaCMS.Manage.Design.Ask
{
    public partial class AnsList : CustomerPageAction
    {
        B_Design_Answer answerBll = new B_Design_Answer();
        B_Design_Ask askBll = new B_Design_Ask();
        B_User buser = new B_User();
        public int AskID { get { return DataConverter.CLng(Request.QueryString["askid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                M_Design_Ask askMod = new M_Design_Ask();
                if (AskID > 0) { askMod = askBll.SelReturnModel(AskID); }
                else { askMod.Title = "全部回答"; }
                AskTitle_L.Text = askMod.Title;
            }
        }
        public void MyBind(string Skey = "")
        {
            DataTable dt = answerBll.Sel(-100, AskID, Skey);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AnsDetailList.aspx?ansid=" + dr["ID"] + "'");
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    {
                        askBll.Del(id);
                    }
                    break;
            }
            MyBind();
        }
        public string GetIP()
        {
            return IPScaner.IPLocation(Eval("IP", ""));
        }
        public string GetCUser()
        {
            if (DataConverter.CLng(Eval("UserID")) < 1) { return "游客"; }
            else { return "<a href='javascript:;' onclick='showuser(" + Eval("UserID") + ");'>" + Eval("UserName") + "</a>"; }
        }

        protected void Search_B_Click(object sender, EventArgs e)
        {
            string Skey = Skey_T.Text.Trim();
            if (!string.IsNullOrEmpty(Skey)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); template.Attributes.Add("style", "margin-top:5px;"); }
            MyBind(Skey);
        }
    }
}
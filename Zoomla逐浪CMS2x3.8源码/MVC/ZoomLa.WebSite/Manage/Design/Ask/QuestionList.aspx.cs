using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;

namespace ZoomLaCMS.Manage.Design.Ask
{
    public partial class QuestionList : CustomerPageAction
    {
        B_Design_Question quBll = new B_Design_Question();
        B_Design_Ask askBll = new B_Design_Ask();
        B_User buser = new B_User();
        public int AskID { get { return DataConverter.CLng(Request.QueryString["askid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AskID <= 0) { function.WriteErrMsg("没有传入问卷编号"); }
                MyBind();
                M_Design_Ask askMod = askBll.SelReturnModel(AskID);
                AskTitle_T.Text = askMod.Title;
                // Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='AskList.aspx'>问卷调查</a></li><li class='active'>" + askMod.Title + "[<a href='QuestionAdd.aspx'>添加问题</a>]</li>");
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            DataTable dt = quBll.Sel(AskID);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    {
                        quBll.Del(id);
                    }
                    break;
            }
            MyBind();
        }
        public string GetRequired()
        {
            bool Required = DataConverter.CBool(Eval("Required", ""));
            if (Required)
            {
                return "<i class='fa fa-check' style='color:green;'></i>";
            }
            else
            {
                return "<i class='fa fa-cloas' style='color:red;'></i>";
            }
        }
        public string GetUserName()
        {
            int userid = DataConverter.CLng(Eval("CUser", ""));
            return buser.SelReturnModel(userid).UserName;
        }
        public string GetQType()
        {
            return quBll.GetQType(Eval("QType", ""));
        }
        public string GetOption()
        {
            switch (Eval("qtype", ""))
            {
                case "radio":
                case "checkbox":
                    try
                    {
                        DataTable opdt = JsonConvert.DeserializeObject<DataTable>(Eval("QOption", ""));
                        string result = "";
                        foreach (DataRow dr in opdt.Rows)
                        {
                            result += dr["text"].ToString() + "|";
                        }
                        return result;
                    }
                    catch { return ""; }
                default:
                    return "";
            }
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='QuestionAdd.aspx?ID=" + dr["ID"] + "'");
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Design.Ask
{
    public partial class AnsDetailList : CustomerPageAction
    {
        public int AnsID { get { return DataConvert.CLng(Request.QueryString["ansid"]); } }
        B_Design_AnsDetail ansdeBll = new B_Design_AnsDetail();
        B_Design_Answer ansBll = new B_Design_Answer();
        B_Design_Ask askBll = new B_Design_Ask();
        B_Design_Question questBll = new B_Design_Question();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Design_Answer ansMod = ansBll.SelReturnModel(AnsID);
                M_Design_Ask askMod = askBll.SelReturnModel(ansMod.AskID);
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='AnsList.aspx?AskID=" + ansMod.AskID + "'>回答列表</a></li><li class='active'>" + askMod.Title + "</li>");
                MyBind();
            }
        }
        private void MyBind()
        {
            EGV.DataSource = ansdeBll.Sel("", -100, -100, AnsID);
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
                    int id = Convert.ToInt32(e.CommandArgument);
                    ansdeBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView dr = e.Row.DataItem as DataRowView;
            //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
            //}
        }
        public string GetOptionByVal(string qoption, string values)
        {
            string r = "";
            if (string.IsNullOrEmpty(values)) { return r; }
            if (string.IsNullOrEmpty(qoption)) { return r; }
            DataTable opdt = JsonConvert.DeserializeObject<DataTable>(qoption);
            foreach (string val in values.Split(','))
            {
                if (string.IsNullOrEmpty(val)) { continue; }
                DataRow[] drs = opdt.Select("value='" + val + "'");
                if (drs.Length > 0) { r = drs[0]["text"].ToString(); }
            }
            return r;
        }
        public string GetAnswer()
        {
            string r = "";
            switch (Eval("QType", ""))
            {
                case "radio":
                case "checkbox":
                    r = GetOptionByVal(Eval("QOption", ""), Eval("Answer", ""));
                    break;
                case "score":
                    r = Eval("Answer", "");
                    break;
                case "blank":
                    r = Eval("Answer", "");
                    break;
            }
            return r;
        }
        public string GetRequired() { return DataConvert.CBool(Eval("Required", "")) ? ComRE.Icon_OK : ComRE.Icon_Error; }
        public string GetQType() { return questBll.GetQType(Eval("QType", "")); }
    }
}
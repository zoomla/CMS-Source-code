using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;


namespace ZoomLa.WebSite.Manage.Template
{
    public partial class LabelManage : System.Web.UI.Page
    {
        private B_Label bll = new B_Label();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("LabelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string LabelCateList = this.bll.LabelCateList();
                string[] CateArr = LabelCateList.Split(new char[] { ',' });
                string labellist = "";
                for (int i = 0; i < CateArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(CateArr[i]))
                        labellist = labellist + "<a href=\"LabelManage.aspx?Cate=" + CateArr[i] + "\">" + CateArr[i] + "</a>&nbsp;|&nbsp;";
                }
                this.lblLabel.Text = labellist;
                string LabelCate = this.Request.QueryString["Cate"];
                int CurrentPage;
                if (string.IsNullOrEmpty(this.Request.QueryString["p"]))
                {
                    CurrentPage = 1;
                }
                else
                    CurrentPage = DataConverter.CLng(this.Request.QueryString["p"]);

                int CountPerPage = 20;
                this.repFile.DataSource = this.bll.LabelList(LabelCate, CountPerPage, CurrentPage);
                this.repFile.DataBind();
                int RecordCount = this.bll.GetLabelListCount(LabelCate);
                this.pager1.InnerHtml = function.ShowPage(RecordCount, CountPerPage, CurrentPage, true, "个");
            }
        }
        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                M_Label labelInfo = this.bll.GetLabel(DataConverter.CLng(Id));
                if (labelInfo.LableType == 1)
                    Response.Redirect("LabelHtml.aspx?LabelID=" + Id);
                if (labelInfo.LableType > 1)
                    Response.Redirect("LabelSql.aspx?LabelID=" + Id);
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.DelLabel(DataConverter.CLng(Id));
                Response.Redirect("LabelManage.aspx");
            }
            if (e.CommandName == "Copy")
            {
                string Id = e.CommandArgument.ToString();
                M_Label newlbl = this.bll.GetLabel(DataConverter.CLng(Id));
                newlbl.LableName = newlbl.LableName + DataSecurity.RandomNum(4);
                this.bll.AddLabel(newlbl);
                Response.Redirect("LabelManage.aspx");
            }
        }
        public string GetLabelLink(string id)
        {
            string re = "";
            M_Label labelInfo = this.bll.GetLabel(DataConverter.CLng(id));
            if (labelInfo.LableType == 1)
                re = "<a href=\"LabelHtml.aspx?LabelID=" + id + "\" title=\""+labelInfo.Desc+"\">" + labelInfo.LableName + "</a>";
            if (labelInfo.LableType > 1)
                re = "<a href=\"LabelSql.aspx?LabelID=" + id + "\" title=\"" + labelInfo.Desc + "\">" + labelInfo.LableName + "</a>";
            return re;
        }
        public string GetLabelType(string type)
        {
            if (type == "1")
                return "静态标签";
            if (type == "2")
                return "动态标签";
            if (type == "3")
                return "数据源标签";
            if(type=="4")
                return "分页列表标签";
            return "";
        }
    }
}
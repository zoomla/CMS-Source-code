namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class CollectionManage : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_Node bn = new B_Node();
        private B_CollectionItem bc = new B_CollectionItem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='" + Request.RawUrl + "'>信息采集</a></li><li class='active'><a href='" + Request.RawUrl + "'>项目管理</a></li><li><a style=\"color:red;\" href='CollectionStep1.aspx'>[添加采集项目]</a></li>" + Call.GetHelp(95));
            }
        }
        private void DataBind(string key="")
        {
            Egv.DataSource = bc.Sel();
            Egv.DataBind();
        }
        protected string GetMode(string str)
        {
            return bll.GetModelById(DataConverter.CLng(str)).ModelName;
        }
        protected string GetNode(string nodelist)
        {
            string[] nodearr = nodelist.Split(new char[] { ',' });
            string str = "";
            foreach (string node in nodearr)
            {
                if (!string.IsNullOrEmpty(node))
                {
                    str += GetParent(DataConverter.CLng(node)) + "<br />";
                }
            }
            if (str.EndsWith("<br />"))
            {
                str = str.Remove(str.LastIndexOf("<br />"));
            }
            return str;
        }
        private string GetParent(int ParentID)
        {
            string str = "";
            M_Node mn = bn.GetNodeXML(ParentID);
            if (mn.ParentID > 0)
            {
                str = GetParent(mn.ParentID) + "&gt;&gt;" + mn.NodeName;
            }
            else
            {
                str = mn.NodeName;
            }
            return str;
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                if (bc.DelByIds(Request.Form["idchk"]))
                    function.WriteErrMsg("成功删除采集项目！", customPath2 + "Content/CollectionManage.aspx");
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del2":
                    bc.GetDelete(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}
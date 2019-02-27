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
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_Page_SetNodeOrder : System.Web.UI.Page
{
    protected B_Templata tll = new B_Templata();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "NodeManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        string pid = base.Request.QueryString["ParentID"];
        if (string.IsNullOrEmpty(pid))
        {
            pid = "0";
        }
        this.ViewState["ParentID"] = pid;
        if (!this.Page.IsPostBack)
        {
            MyBind();
        }
        Call.SetBreadCrumb(Master, "<li>企业黄页</li><li><a href='FlowManager.aspx'>栏目列表</a></li><li>" + Literal1.Text + "</li><li>子栏目排序</li>");
    }
    public void MyBind()
    {
        int ParentID;
        ParentID = DataConverter.CLng(this.ViewState["ParentID"].ToString());
        if (ParentID == 0)
            this.Literal1.Text = "根节点";
        else
        {
            this.Literal1.Text = tll.SelReturnModel(ParentID).TemplateName;
        }
        DataTable dt = tll.Sel();
        dt.DefaultView.RowFilter = "ParentID=" + ParentID;
        dt.DefaultView.Sort = "OrderID ASC";
        dt = dt.DefaultView.ToTable();
        this.RepSystemModel.DataSource = dt;
        this.RepSystemModel.DataBind();
    }
    public string GetNodeType(string NodeType)
    {
        string restr = string.Empty;
        switch (NodeType)
        {
            case "1":
                restr = "文本型栏目";
                break;
            case "2":
                restr = "栏目型栏目";
                break;
            case "3":
                restr = "Url转发型栏目";
                break;
            case "4":
                restr = "功能型栏目";
                break;
            default:
                restr = "未知栏目";
                break;
        }
        return restr;
    }
    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        M_Templata model = new M_Templata();
        if (e.CommandName == "DownMove")
        {
            if (e.Item.ItemIndex < RepSystemModel.Items.Count - 1)
            {
                model = tll.SelReturnModel(Convert.ToInt32((RepSystemModel.Items[e.Item.ItemIndex + 1].FindControl("Hid_TemplateID") as HiddenField).Value));
                int oiddown = model.OrderID;
                model = tll.SelReturnModel(Convert.ToInt32(e.CommandArgument.ToString()));
                int oid = model.OrderID;
                tll.UpdateOrder(Convert.ToInt32((RepSystemModel.Items[e.Item.ItemIndex + 1].FindControl("Hid_TemplateID") as HiddenField).Value), oid);
                tll.UpdateOrder(Convert.ToInt32(e.CommandArgument.ToString()), oiddown);
            }
        }
        if (e.CommandName == "UpMove")
        {
            if (e.Item.ItemIndex > 0)
            {
                model = tll.SelReturnModel(Convert.ToInt32((RepSystemModel.Items[e.Item.ItemIndex - 1].FindControl("Hid_TemplateID") as HiddenField).Value));
                int oiddown = model.OrderID;
                model = tll.SelReturnModel(Convert.ToInt32(e.CommandArgument.ToString()));
                int oid = model.OrderID;
                tll.UpdateOrder(Convert.ToInt32((RepSystemModel.Items[e.Item.ItemIndex - 1].FindControl("Hid_TemplateID") as HiddenField).Value), oid);
                tll.UpdateOrder(Convert.ToInt32(e.CommandArgument.ToString()), oiddown);
            }
        }
        MyBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string[] oid = Request.Form["order_T"].Split(',');//需要更换成的ID
        string[] hid = Request.Form["order_Hid"].Split(',');//信息描述
        for (int i = 0; i < oid.Length; i++)
        {
            int newoid = Convert.ToInt32(oid[i]);
            int tid = Convert.ToInt32(hid[i].Split(':')[0]);
            int oldoid = Convert.ToInt32(hid[i].Split(':')[1]);
            if (newoid != oldoid)
            {
                tll.UpdateOrder(tid, newoid);
            }
        }
        function.Script(Page, "alert('更新成功！！');");
        MyBind();
    }
}

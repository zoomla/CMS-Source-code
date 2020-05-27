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
using BDUBLL;
using BDUModel;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Zone_LogTypeManage : CustomerPageAction
{
    #region 调用业务逻辑
    //GSManageBLL gsbll = new GSManageBLL();
    B_Admin ubll = new B_Admin();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Egv.txtFunc = txtPageFunc;
        ZoomLa.Common.function.AccessRulo();
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='GSManage.aspx'>空间族群列表</a></li><li class='active'>族群类型管理<a href='LogTypeAdd.aspx'>[添加族群类型]</a></li>");
    }

    #region 页面调用方法

    public void DataBind(string key="")
    {
        //Egv.DataSource = gsbll.GetGSTypeList();
        //Egv.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = Egv.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = Egv.PageSize;
        }
        Egv.PageSize = pageSize;
        Egv.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> dt = new Dictionary<string, string>();
            dt.Add("Addtime", "0");
            return dt;
        }
    }
    //绑定数据
    protected void btn_DeleteRecords_Click(object sender, EventArgs e)
    {
        string idst = Request.Form["chkSel"];
        if (!string.IsNullOrWhiteSpace(idst))
        {
            string[] list = idst.Split(new char[] { ','});
            foreach (string st in list)
            {
                //gsbll.DelType(new Guid(st));
            }
        }
        DataBind();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        GridViewRow gr = lb.Parent.Parent as GridViewRow;
        //Guid id = new Guid(this.gvGrouplist.DataKeys[gr.RowIndex].Value.ToString());
        //gsbll.DelType(id);
        //GetData();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del1":
                //gsbll.DelType(new Guid(e.CommandArgument.ToString()));
                break;
            default :
                break;
        }
        DataBind();
    }
    #endregion
    
}

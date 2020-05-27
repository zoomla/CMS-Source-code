using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class User_PubInfo : System.Web.UI.Page
{
    protected B_User buse = new B_User();
    protected B_Pub bpub = new B_Pub();
    M_Pub mpub = new M_Pub();
    public int pubid;
    protected void Page_Load(object sender, EventArgs e)
    {
        EGV.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind(string key = "")
    {
        mpub = bpub.SelReturnModel(DataConvert.CLng(Request.QueryString["PubID"]));
        EGV.DataSource = bpub.GetComments(mpub.PubTableName, buse.GetLogin().UserName);
        EGV.DataBind();
    }
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        mpub = bpub.SelReturnModel(DataConvert.CLng(Request.QueryString["PubID"]));
        switch (e.CommandName.ToLower())
        {
            case "view":
                ShowDiv1.Visible = false;
                ShowDiv.Visible = true;
                DataTable dt = bpub.GetCommentT(mpub.PubTableName, DataConvert.CLng(e.CommandArgument.ToString()));
                CID.Text = dt.Rows[0]["ID"].ToString();
                UserName.Text = dt.Rows[0]["PubUserName"].ToString();
                Ctitle.Text = dt.Rows[0]["PubTitle"].ToString();
                Content.Text = dt.Rows[0]["PubContent"].ToString();
                CIP.Text = dt.Rows[0]["PubIP"].ToString();
                AddTime.Text = dt.Rows[0]["PubAddTime"].ToString();
                break;
            case "pass":
                if (mpub.PubPermissions.IndexOf("Sen") > -1)
                    bpub.PassComments(mpub.PubTableName, DataConvert.CLng(e.CommandArgument.ToString()));
                else
                    function.WriteErrMsg("后台未赋予审核权限");
                break;
            case "npass":
                if (mpub.PubPermissions.IndexOf("Sen") > -1)
                    bpub.NPassComments(mpub.PubTableName, DataConvert.CLng(e.CommandArgument.ToString()));
                else
                    function.WriteErrMsg("后台未赋予审核权限");
                break;
            case "edit1":
                if (mpub.PubPermissions.IndexOf("Edit") > -1)
                {
                    PubID.Value = e.CommandArgument.ToString();
                    ShowDiv1.Visible = false;
                    ShowDiv2.Visible = true;
                    DataTable dt1 = bpub.GetCommentT(mpub.PubTableName, DataConvert.CLng(e.CommandArgument.ToString()));
                    Title.Text = dt1.Rows[0]["PubTitle"].ToString();
                    pubContent.Text = dt1.Rows[0]["PubContent"].ToString();
                    
                }
                else
                    function.WriteErrMsg("后台未赋予修改权限");
                break;
            case "del2":
                if(mpub.PubPermissions.IndexOf("del")>-1)
                    bpub.DelComments(mpub.PubTableName, DataConvert.CLng(e.CommandArgument.ToString()));
                else
                    function.WriteErrMsg("后台未赋予删除权限");
                break;
        }
        DataBind();
    }
    public string GetStatus(string pubstrat)
    {
        switch (pubstrat)
        {
            case "0":
                return "<font color=green>未审核</font>";
            case "1":
                return "<font color=red>已审核</font>";
            default:
                return "";
        }
    }
    public string GetUserName(string userID)
    {
        return buse.GetUserNameByIDS(userID);
    }
    protected void BackTo_Click(object sender, EventArgs e)
    {
        ShowDiv1.Visible = true;
        ShowDiv.Visible = false;
    }
    protected void EditBtn_Click(object sender, EventArgs e)
    {
        mpub = bpub.SelReturnModel(DataConvert.CLng(Request.QueryString["PubID"]));
        if (bpub.UpdateComments(mpub.PubTableName, DataConvert.CLng(PubID.Value), Title.Text, pubContent.Text))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
        Response.Redirect(Request.Url.ToString()); 
    }
}
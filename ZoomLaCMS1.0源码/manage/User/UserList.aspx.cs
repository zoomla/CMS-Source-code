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
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;


public partial class manage_User_UserList : System.Web.UI.Page
{
    protected int i;
    protected string m_allUser;
    protected string m_allUserName;
    protected string m_UserInput;
    protected int PageSize;
    protected int CurrentPageIndex;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        badmin.CheckMulitLogin();
        this.m_UserInput = base.Request.QueryString["OpenerText"];
        this.PageSize = 20;
        if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
        {
            this.CurrentPageIndex = 1;
        }
        else
        {
            this.CurrentPageIndex = DataConverter.CLng(base.Request.QueryString["p"]);
        }
        this.CheckKeyword();
    }

    private void BindData(string keyword)
    {
        B_User bll = new B_User();
        IList<string> list = bll.GetUserNameList(this.CurrentPageIndex, this.PageSize, keyword);
        if (list.Count == 0)
        {
            this.DivAdd.Visible = false;
            this.DivUserName.Visible = true;
        }
        else
        {
            this.DivUserName.Visible = false;
        }
        this.RepUser.DataSource = list;
        int RecordCount = bll.GetUserNameListTotal(keyword);
        this.pager1.InnerHtml = function.ShowPage(RecordCount, this.PageSize, this.CurrentPageIndex, true, "个");
        this.RepUser.DataBind();
    }

    private void CheckKeyword()
    {
        string str = DataSecurity.FilterBadChar(base.Request.Form["TxtKeyWord"]);
        this.BindData(str);
    }
    protected void RepUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if (string.IsNullOrEmpty(this.m_allUser))
            {
                this.m_allUser = e.Item.DataItem.ToString();
            }
            else
            {
                this.m_allUser = this.m_allUser + "," + e.Item.DataItem.ToString();
            }
        }
    }
}

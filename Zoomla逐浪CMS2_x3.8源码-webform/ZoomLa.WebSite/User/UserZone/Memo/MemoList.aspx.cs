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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MemoList : Page
{
    Memo_BLL memobll = new Memo_BLL();
    B_User ubll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    public void DataBind(string key="")
    {
        Egv.DataSource = memobll.GetMemoList(ubll.GetLogin().UserID, null);
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del":
                memobll.del(new Guid(e.CommandArgument.ToString()));
                break;
        }
        DataBind();
    }
}

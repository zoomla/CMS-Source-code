using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class Manage_Config_FuncSearch : CustomerPageAction
{
    B_Search b_search = new B_Search();

    public string keyWord
    {
        get { return Request.QueryString["keyWord"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li class=\"active\">导航搜索</li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = b_search.SelByName(keyWord);
        EGV.DataBind();
    }
    public string IsMobile(object o)
    {
        string result = "<i class='{0}' title='{1}' style='color:#FF7A00;'/>";
        if (o.ToString().Equals("1"))
        {
            result = string.Format(result, "fa fa-check", "支持移动");
        }
        else
        {
            result = string.Format(result, "fa fa-close", "不支持");
        }
        return result;
    }
    public string getDate(string date)
    {
        return string.Format("{0:d}", Convert.ToDateTime(date));
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = DataConverter.CLng((e.Row.FindControl("hfId") as HiddenField).Value);
            e.Row.Attributes.Add("ondblclick", "getinfo('" + id + "');");
            M_Search sea = b_search.GetSearchById(id);
            if (sea != null)
            {
                Image imgLinkType = e.Row.FindControl("imgLinkType") as Image;
                Label linkType = e.Row.FindControl("linkType") as Label;
                Label lblState = e.Row.FindControl("lblState") as Label;
                switch (sea.Type)
                {
                    case 0://站内链接  
                        if (sea.LinkState == 1 && sea.State == 1) //存在该文件并且启用
                        {
                            lblState.Text = "启用";
                        }
                        if (sea.LinkState == 2 || sea.State == 2)//不存在该文件
                        {
                            lblState.Text = "停用";
                        }
                        linkType.Text = "<i class='fa fa-folder-open' title='站内应用' style='color:#FF7A00;'></i>";
                        break;
                    case 1://用户中心
                        linkType.Text = "<i class='fa fa-list-alt' title='用户中心' style='color:#FF7A00;'></i>";
                        break;
                    case 2://站外链接
                        linkType.Text = "<i class='fa fa-folder-open' title='站内应用' style='color:#FF7A00;'></i>";
                        break;
                    default:
                        break;
                }
                if (sea.State == 2) //文件未启用
                {
                    lblState.Text = "停用";
                }
                if (sea.State == 1) //文件启用
                {
                    lblState.Text = "启用";
                }
                sea.FlieUrl = sea.FlieUrl.ToLower();
                if (sea.FlieUrl.StartsWith("http:") || sea.FlieUrl.StartsWith("https:"))
                    linkType.Text = "<i class='fa fa-chain' title='站外链接' style='color:#FF7A00;'></i>";
            }
        }
    }
}


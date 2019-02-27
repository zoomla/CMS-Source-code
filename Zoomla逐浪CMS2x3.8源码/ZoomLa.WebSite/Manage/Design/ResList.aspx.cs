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
using ZoomLa.SQLDAL;

public partial class Manage_Design_ResList : CustomerPageAction
{
    B_Design_RES resBll = new B_Design_RES();
    public string ZType
    {
        get
        {
            if (ViewState["ztype"] == null || string.IsNullOrEmpty(ViewState["ztype"].ToString()))
            {
                return Request.QueryString["ztype"] ?? "";
            }
            return ViewState["ztype"].ToString();
        }
        set
        {
            ViewState["ztype"] = value;
        }
    }
    public string Useage
    {
        get
        {
            if (ViewState["useage"] == null || string.IsNullOrEmpty(ViewState["useage"].ToString()))
            {
                return Request.QueryString["useage"] ?? "";
            }
            return ViewState["useage"].ToString();
        }
        set
        {
            ViewState["useage"] = value;
        }
    }
    public string Name
    {
        get
        {
            if (ViewState["name"] == null || string.IsNullOrEmpty(ViewState["name"].ToString()))
            {
                return Request.QueryString["name"] ?? "";
            }
            return ViewState["name"].ToString();
        }
        set
        {
            ViewState["name"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        function.Script(this, "showtab('" + ZType + "');");
        EGV.DataSource = resBll.Search(Name, Useage, ZType, "", "", "");
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddRes.aspx?ID=" + dr["ID"] + "'");
        }
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConvert.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Del":
                {
                    resBll.Del(id);
                }
                break;
        }
        MyBind();
    }
    public string GetUseage(string useage)
    {
        return resBll.GetUseage(useage);
    }
    public string GetType(string ztype)
    {
        return resBll.GetZType(ztype);
    }
    public string GetRes()
    {
        switch (Eval("ZType").ToString())
        {
            case "img":
                return "<img src = \'" + Eval("previewimg") + "' width = '50' height = '50' />";
            case "music":
                return "<a href = 'javascript:;' onclick = \"play('" + Eval("VPath") + "',this);\" ><i class='fa fa-play'></i> 点击播放</a><a href='javascript:;' hidden onclick='pause(this);'><i class='fa fa-pause'></i>停止播放</a>";
            case "shape":
            case "text":
            case "icon":
                return "<img src = \'" + Eval("vpath") + "' width = '50' height = '50' />";
            default:
                return "";
        }
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        Name = Skey_T.Text;
        Useage = Useage_Rad.SelectedValue;
        if (!string.IsNullOrEmpty(Skey_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
        else { sel_box.Attributes.Add("style", "display:none;"); }
        MyBind();
    }

    protected string GetStatus()
    {
        switch (DataConvert.CLng(Eval("ZStatus")))
        {
            case 0: return "<span style='color:blue;'>正常</span>";
            case 1: return "<span style='color:green;'>推荐</span>";
            case -1: return "<span style='color:red;'>停用</span>";
            default: return "";
        }
    }
}
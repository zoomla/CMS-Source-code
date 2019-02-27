using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

public partial class manage_Boss_BossInfo : CustomerPageAction
{
    B_BossInfo boll = new B_BossInfo();
    private B_User bll = new B_User();
    private B_Node bnll = new B_Node();
    private B_Card bc = new B_Card();
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>系统设置</li><li>加盟商管理</li>");
        M_UserInfo uinfo = buser.GetLogin();
        int nodeid = string.IsNullOrEmpty(Request.QueryString["nodeid"]) ? -1 : DataConverter.CLng(Request.QueryString["nodeid"]);
        int pardentid = string.IsNullOrEmpty(Request.QueryString["pardentid"]) ? -1 : DataConverter.CLng(Request.QueryString["pardentid"]);
        if (nodeid == -1)
            function.WriteErrMsg("参数错误");
        this.HiddenNode.Value = nodeid.ToString();
        this.HiddenPnode.Value = pardentid.ToString();
        M_BossInfo MBoss = boll.GetSelect(DataConverter.CLng(nodeid));
        this.tx_cname.Text = MBoss.CName;
        M_BossInfo Mb = boll.GetSelect(nodeid);
        DataTable ddc = bc.SelCarByUserID(Mb.userid);
        Bind(ddc);
        this.fhwunum.Text = "";
        this.Enum.Text = "";

    }
    protected string GetUserName(string uid)
    {
        if (DataConverter.CLng(uid) == 0)
        {
            return "暂无用户";
        }
        else
        {
            return buser.GetUserByUserID(int.Parse(uid)).UserName;
        }
    }
    protected string GetState(string str)
    {
        string state = "";
        switch (str)
        {
            case "1":
                state = "未启用";
                break;
            case "2":
                state = "启用";
                break;
            case "3":
                state = "停用";
                break;
        }
        return state;
    }

    private void Bind(DataTable dd)
    {
        int CPage, temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }

        DataTable Cll = dd;

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 12;
        cc.CurrentPageIndex = CPage - 1;
        gvCard.DataSource = cc;
        gvCard.DataBind();

        Allnum.Text = Cll.DefaultView.Count.ToString();
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;
        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            Downpage.Enabled = false;
        }
        else
        {
            Downpage.Enabled = true;
        }
        if (nextpagenum <= 0)
        {
            nextpagenum = 0;
            Nextpage.Enabled = false;
        }
        else
        {
            Nextpage.Enabled = true;
        }
        Toppage.Text = "<a href=?Currentpage=0&nodeid=" + HiddenNode.Value + "&parentid=" + HiddenPnode.Value + ">首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&nodeid=" + HiddenNode.Value + "&parentid=" + HiddenPnode.Value + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&nodeid=" + HiddenNode.Value + "&parentid=" + HiddenPnode.Value + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&nodeid=" + HiddenNode.Value + "&parentid=" + HiddenPnode.Value + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();

        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;


public partial class Manage_Guest_GuestManage : System.Web.UI.Page
{
    B_GuestBookCate cateBll = new B_GuestBookCate();
    private int CateID { get { return DataConverter.CLng(Request.QueryString["CateID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "GuestManage");
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Del"]))
            {
                int cateid = DataConverter.CLng(Request.QueryString["Del"]);
                if (cateid > 0){ cateBll.Del(cateid); }
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx" + "'>工作台</a></li><li class='active'><a href='/admin/Guest/GuestCateMana.aspx'>留言管理</a></li><li class='active'>[<a href='javascript:;' onclick='ShowCate(0,0);' id='showDiv'>添加留言分类</a>]</li>" + Call.GetHelp(92));
        }
    }
    private void MyBind()
    {
        RPT.DataSource = cateBll.SelByGuest();
        RPT.DataBind();
    }
    public string GetBarStatus(string barInfo)
    {
        string strcolor = "black";
        string restr = "普通";
        if (!string.IsNullOrWhiteSpace(barInfo) && barInfo.Contains("Recommend"))
        {
            strcolor = "blue";
            restr = "推荐";
        }
        return "<span style='color:" + strcolor + "'>" + restr + "</span>";
    }
    public string GetNeedLog(string needlog)
    {
        string result = "";
        switch (needlog)
        {
            case "0":
                result = "允许匿名";
                break;
            case "1":
                result = "登录用户";
                break;
            case "2":
                result = "指定用户";
                break;
            default:
                result = "未知";
                break;
        }
        return result;
    }
    
    public string GetCateName()
    {
        string url = "";
        url = string.Format("ShowCate({0},0)", Eval("CateID"));
        string linkName = string.Format("<a href=\"javascript:;\" onclick=\"{0}\">{1}</a>", url, Eval("CateName"));
        return linkName;
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Del"))
            cateBll.Del(DataConverter.CLng(e.CommandArgument));
        MyBind();
    }
    public string GetIcon()
    {
        return StringHelper.GetItemIcon(Eval("BarImage").ToString());
    }
}
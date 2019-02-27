using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_Site_BountOrder : CustomerPageAction
{

    protected string siteName, appName, webPath, sitePort, siteDomain;
    protected ServerManager iis = new ServerManager();
    protected IISHelper iisHelper = new IISHelper();
    protected IISWebSite siteModel = new IISWebSite();
    protected B_Admin badmin = new B_Admin();
    protected B_OrderList border = new B_OrderList();
    protected B_User buser = new B_User();
    protected EnviorHelper enHelper = new EnviorHelper();
    protected B_Site_SiteList siteBll = new B_Site_SiteList();
    protected M_Site_SiteList siteM = new M_Site_SiteList();
    protected string siteID;
    public string piePercent;
    //long banSize = 1073741824;
    public delegate string AsyncInvokeFunc(string path);//与目标方法必须参数返回均相同.
    public string getFileSize(string path)
    {
        long size = FileSystemObject.getDirectorySize(path);
        string used = FileSystemObject.ConvertSizeToShow(size);
        return used;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //此处控件未实例化，不能对控件进行操作
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            this.MasterPageFile = "~/manage/Site/OptionMaster.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        siteName = Server.UrlDecode(Request.Params["siteName"]);
        siteID = Request.QueryString["siteID"];
        //SiteID无法获取到对应的，只能通过名字来访问了,ID需要通过循环遍历
        EGV.txtFunc = txtPageFunc;

        if ((string.IsNullOrEmpty(siteName) || iis.Sites[siteName] == null) && string.IsNullOrEmpty(siteID))
        {
            function.WriteErrMsg("未选择站点或要访问的站点不存在!!");
        }
        if (string.IsNullOrEmpty(siteID))
            siteID = iis.Sites[siteName].Id.ToString();
        else
        {
            siteName = iisHelper.GetNameBySiteID(siteID);
        }
        if (string.IsNullOrEmpty(siteName))
            function.WriteErrMsg("未选择站 点或要访问的站点不存在!");
        if (!IsPostBack)
        {
            DataBind("");
        }
    }
    //验证权限
    public void AuthCheck()
    {
        //---------------指定管理员权限管理
        if (badmin.CheckLogin())
        {
            //如果是管理员登录则不判断
        }
        else if (buser.CheckLogin())
        {
            //非管理员用户登录,开始判断
            string userID = buser.GetLogin().UserID.ToString();
            siteM = siteBll.SelByUserID(userID);//查找有无为该用户分配权限
            if (siteM == null || !siteBll.AuthCheck(siteID, userID))
                function.WriteErrMsg("你没有管理站点的权限");
        }
        else
        {
            function.WriteErrMsg("你无权限访问该站点!!");
        }
    }
    public void DataBind(string keys)
    {
        EGV.DataSource = border.GetOrderListByOrderType(7);
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
        DataBind("");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind("");
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string url = "SiteDetail.aspx?siteName=" + Request.QueryString["siteName"];
        M_Site_SiteList msite = siteBll.SelectByName(Request.QueryString["siteName"]);
        string[] orders = msite.OrderNum.Split(',');
        switch (e.CommandName)
        {
            case "select":
                if (siteBll.CheckOrderNum(e.CommandArgument.ToString()).Rows.Count == 0)
                {
                    int k = 0;
                    if (msite.OrderNum == "")
                        msite.OrderNum = e.CommandArgument.ToString();
                    else
                    {
                        for (int i = 0; i < orders.Length; i++, k++)
                        {
                            if (orders[i] == e.CommandArgument.ToString())
                                break;
                        }
                        if (k == orders.Length)
                            msite.OrderNum += "," + e.CommandArgument.ToString();
                    }
                    siteBll.UpdateModel(msite);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
                    Response.Write(" <script>window.parent.window.location.href = '" + url + "' </script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('该订单已被绑定，请选择其他订单！');", true);
                break;
            case "delete1":
                string str = "";
                if (msite.OrderNum != "")
                {
                    if (siteBll.CheckOrderNum(Request.QueryString["siteName"], e.CommandArgument.ToString()).Rows.Count == 0)
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('站点未绑定该订单');", true);
                    else
                    {
                        for (int i = 0; i < orders.Length; i++)
                        {
                            if (orders[i] != e.CommandArgument.ToString())
                            {
                                str += orders[i] + ",";
                            }
                        }
                        str = str.TrimEnd(',');
                        msite.OrderNum = str;
                        siteBll.UpdateModel(msite);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
                        Response.Write(" <script>window.parent.window.location.href = '" + url + "' </script>");
                    }
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('站点未绑定任何订单');", true);
                break;
            default:
                break;
        }
    }
    public string GetBound( string ordernum)
    {
        if (siteBll.CheckOrderNum(ordernum).Rows.Count == 0)
            return "<font>未绑定</font>";
        else
            return "<font color=red>已绑定</font>";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int type = DataConverter.CLng(DropDownList1.SelectedValue);
        string key = TxtKeyword.Text;
        EGV.DataSource = border.Getsouchinfo(type,key, 7);
        EGV.DataBind();
    }
}
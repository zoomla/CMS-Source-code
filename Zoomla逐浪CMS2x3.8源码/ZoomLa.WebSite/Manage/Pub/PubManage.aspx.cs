using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class Manage_I_Pub_PubManage : System.Web.UI.Page
{
    private B_Pub pubBll = new B_Pub();
    private B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.pub, "PubManage");
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+Resources.L.工作台+"</a></li><li><a href='" + CustomerPageAction.customPath2 + "Content/ContentManage.aspx'>"+Resources.L.内容管理+"</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.互动模块管理 + "</a></li>" + Call.GetHelp(96) + "<li >[<a style='color:red;' href='PubInfo.aspx'>" + Resources.L.添加互动模块 + "</a>]</li>");
            MyBind();
        }
    }
    protected void MyBind(string key = "")
    {
        M_AdminInfo adminMod = B_Admin.GetLogin();
        DataTable pubtable = pubBll.SelByType();
        if (!B_Admin.IsSuperManage(adminMod.AdminId))
        {
            GetTable(pubtable, B_Role.GetPowerInfoByIDs(adminMod.RoleList));
        }
        Egv.DataSource = pubtable;
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string PubtypeName(string type)
    {

        string typename = "";
        switch (type)
        {
            case "0":
                typename = Resources.L.评论;
                break;
            case "1":
                typename = Resources.L.投票;
                break;
            case "2":
                typename = Resources.L.活动;
                break;
            case "3":
                typename = Resources.L.留言;
                break;
            case "4":
                typename = Resources.L.问券调查;
                break;
            case "5":
                typename = Resources.L.通用统计;
                break;
            case "6":
                typename = Resources.L.竞标;
                break;
        }
        return typename;
    }
    public string GetClassName(string Classs)
    {
        string classname = "";
        switch (Classs)
        {
            case "0":
                classname = Resources.L.内容;
                break;
            case "1":
                classname = Resources.L.商城;
                break;
            case "2":
                classname = Resources.L.黄页;
                break;
            case "3":
                classname = Resources.L.店铺;
                break;
            case "4":
                classname = Resources.L.会员;
                break;
            case "5":
                classname = Resources.L.节点;
                break;
            case "6":
                classname = Resources.L.首页;
                break;
        }
        return classname;
    }
    public string GetLabel(object type, object o, object n)
    {
        string result = "";
        if (type.ToString().Equals("7"))
        {
            //{ZL:StarLabel(ZL.Label id="我是评星7调用标签",1)/}
            result = "{ZL:StarLabel(ZL.Label id=\"" + n.ToString() + "调用标签\",ID)/}";
        }
        else
        {
            result = "{Pub.Load_" + o.ToString() + "/}";
        }
        return result;
    }
    public DataTable GetTable(DataTable dt, string PowerInfo)
    {
        string names = "";
        string[] PowerInfoArr = PowerInfo.Split(',');
        for (int i = 0; i < PowerInfoArr.Length; i++)
        {
            names += "'" + PowerInfoArr[i] + "',";
        }
        names = names.Trim(',');
        dt.DefaultView.RowFilter = "PubTableName in (" + names + ")";
        dt = dt.DefaultView.ToTable();
        return dt;
    }
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='Pubsinfo.aspx?Pubid={0}&type=0'", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
        }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "copy":
                //{
                //    int id = Convert.ToInt32(e.CommandArgument);
                //    M_Pub pubMod = pubBll.SelReturnModel(id);
                //    pubMod.Pubid = 0;
                //    pubBll.inser
                //}
                break;
            case "Del":
                pubBll.Del(DataConverter.CLng(e.CommandArgument));
                break;
        }
        MyBind();
    }

    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            pubBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
}
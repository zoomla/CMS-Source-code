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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_Content_Recyle : System.Web.UI.Page
{
    B_Content bll = new B_Content();
    B_Model bmode = new B_Model();
    B_User buser = new B_User();
    B_ModelField mll = new B_ModelField();
    B_Templata tll = new B_Templata();

    public int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
    public string Type { get { return Request.QueryString["type"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        EGV.DataSource = bll.Page_GetRecycle(TxtSearchTitle.Value.Trim(), mu.UserName);
        EGV.DataKeyNames = new string[] { "GeneralID" };
        EGV.DataBind();
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    public string GetUrl(string infoid)
    {
        int p = DataConverter.CLng(infoid);
        M_CommonData cinfo = bll.GetCommonData(p);
        if (cinfo.IsCreate == 1)
            return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
        else
            return "/Page/PageContent.aspx?ItemID=" + p;
    }

    public string GetModel(string infoid)
    {
        int p = DataConverter.CLng(infoid);
        M_CommonData cinfo = bll.GetCommonData(p);

        if (cinfo.ModelID == 0)
        {
            return "";
        }
        else
        {
            return "[" + bmode.GetModelById(cinfo.ModelID).ItemName + "] ";
        }
    }
    public string GetCteate(string IsCreate)
    {
        int s = DataConverter.CLng(IsCreate);
        if (s != 1)
            return "<font color=red>×</font>";
        else
            return "<font color=green>√</font>";
    }
    public string GetStatus(string status)
    {
        int s = DataConverter.CLng(status);
        if (s == 0)
            return "待审核";
        if (s == 99)
            return "已审核";
        if (s == -1)
            return "退档";
        return "回收站";
    }
    public bool ChkStatus(string status)
    {
        int s = DataConverter.CLng(status);
        if (s == 99)
            return false;
        else
            return true;
       
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName.ToLower())
        {
            case "rec":
                bll.Reset(id);//将状态改回0
                break;
            case "del":
                bll.Del(id);
                break;
        }
        MyBind();
    }
    protected void btnRecAll_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            bll.Reset(Request.Form["idchk"]);
        }
        function.WriteSuccessMsg("还原成功", "Recyle.aspx");
    }

    protected void Bat_Del_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            bll.DelByIDS(Request.Form["idchk"]);
        }
        function.WriteSuccessMsg("删除成功", "Recyle.aspx");
    }
}
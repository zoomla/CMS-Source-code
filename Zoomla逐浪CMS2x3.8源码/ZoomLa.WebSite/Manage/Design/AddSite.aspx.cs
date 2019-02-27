using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

public partial class Manage_Design_AddSite : CustomerPageAction
{
    B_User buser = new B_User();
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_IDC_DomainList domBll = new B_IDC_DomainList();
    B_Design_Page pageBll = new B_Design_Page();
    protected int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected int Tabs { get { return DataConvert.CLng(Request.QueryString["tabs"]) == 0 ? 0 : 1; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='SiteList.aspx'>站点列表</a></li><li class='active'>站点信息</li>");
        }
    }
    private void MyBind()
    {
        M_Design_SiteInfo sfMod = sfBll.SelReturnModel(Mid);
        if (Tabs == 0)
        {
            SiteName_T.Text = sfMod.SiteName;
            M_IDC_DomainList domMod = domBll.SelReturnModel(sfMod.DomainID);
            if (domMod != null)
            {
                domain_a.HRef = "http://" + domMod.DomName;
                domain_a.InnerText = domMod.DomName;
            }
            SiteDir_L.Text = sfMod.SiteDir;
            Logo_UP.FileUrl = sfMod.Logo;
            function.Script(this, "setscore(" + sfMod.Score + ");");
        }
        else
        {
            M_UserInfo mu = buser.GetLogin();
            EGV.DataSource = pageBll.U_Sel(mu.UserID, Mid, M_Design_Page.PageEnum.Page);
            EGV.DataBind();
        }
        function.Script(this, "CheckTabs(" + Tabs + ");");
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Design_SiteInfo sfMod = sfBll.SelReturnModel(Mid);
        sfMod.SiteName = SiteName_T.Text;
        Logo_UP.SaveUrl = sfMod.SiteDir + "/UploadFiles/";
        if (!Logo_UP.FVPath.Equals(sfMod.Logo, StringComparison.CurrentCultureIgnoreCase))
        {
            if (Logo_UP.HasFile) { sfMod.Logo = Logo_UP.SaveFile(); }
            else { sfMod.Logo = Logo_UP.FVPath; }
        }
        sfMod.Score = DataConvert.CDouble(Request.Form["score_num"]);
        //if (Logo_UP.FileContent.Length > 100 && SafeSC.IsImage(Logo_UP.FileName))
        //{
        //    sfMod.Logo = SafeSC.SaveFile(sfMod.SiteDir + "/UploadFiles/", Logo_UP);
        //}
        sfBll.UpdateByID(sfMod);
        function.WriteSuccessMsg("修改成功","SiteList.aspx");
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del":
                M_UserInfo mu = buser.GetLogin();
                int id = Convert.ToInt32(e.CommandArgument);
                pageBll.U_Del(mu.UserID, id);
                break;
        }
        MyBind();
    }
}
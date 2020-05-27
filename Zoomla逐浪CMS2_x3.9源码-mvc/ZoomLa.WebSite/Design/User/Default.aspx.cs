using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace ZoomLaCMS.Design.User
{
    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        B_Design_Page pageBll = new B_Design_Page();
        B_Design_Scence seBll = new B_Design_Scence();
        B_Design_Node sfNodeBll = new B_Design_Node();
        B_Design_Ask askBll = new B_Design_Ask();
        B_Design_Helper desHelper = new B_Design_Helper();
        //避免前端修改,分离
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int SiteID { get { return DataConverter.CLng(ViewState["SiteID"]); } set { ViewState["SiteID"] = value; } }
        public string Domain = "你尚未申请域名";
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
            M_Design_SiteInfo sfMod = GetSite();
            SiteName_L.Text = sfMod.SiteName;
            M_IDC_DomainList domMod = domBll.SelReturnModel(sfMod.DomainID);
            if (domMod != null)
            {
                Domain = "http://" + domMod.DomName;
                domain_a2.HRef = "http://" + domMod.DomName;
                domain_a.HRef = "http://" + domMod.DomName;
                domain_a.InnerHtml = "<i class='fa fa-link'></i> http://" + domMod.DomName;
            }
            //edit_a.HRef = "/Design/?SiteID=" + sfMod.ID;
            sitecfg_a.HRef = "SiteInfo.aspx?ID=" + sfMod.ID;

            //--------------
            Site_RPT.DataSource = sfBll.U_Sel(mu.UserID);
            Site_RPT.DataBind();
            Global_RPT.DataSource = pageBll.U_Sel(mu.UserID, sfMod.ID, M_Design_Page.PageEnum.Global);
            Global_RPT.DataBind();
            Scence_RPT.DataSource = seBll.U_Sel(mu.UserID);
            Scence_RPT.DataBind();
            DataTable askdt = askBll.Sel(mu.UserID);
            askdt.DefaultView.Sort = "CDate DESC";
            askdt = askdt.DefaultView.ToTable();
            Ask_RPT.DataSource = askdt;
            Ask_RPT.DataBind();
            DataTable pageDT = pageBll.U_Sel(mu.UserID, sfMod.ID, M_Design_Page.PageEnum.Page);
            EGV.DataSource = pageDT;
            EGV.DataBind();
            if (pageDT.Rows.Count > 0)
            {
                M_Design_Tlp tlpMod = tlpBll.SelReturnModel(Convert.ToInt32(pageDT.Rows[0]["TlpID"]));
                if (tlpMod != null) { TlpView_img.Src = tlpMod.PreviewImg; }
            }
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
                case "del2":
                    M_UserInfo mu = buser.GetLogin();
                    int id = Convert.ToInt32(e.CommandArgument);
                    pageBll.U_Del(mu.UserID, id);
                    break;
            }
            MyBind();
        }
        //----------
        private M_Design_SiteInfo GetSite()
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin();
            M_Design_SiteInfo sfMod = sfBll.SelModelByUid(mu.UserID);
            if (sfMod == null)
            {
                //自动创建站点
                Response.Redirect("/design/newsite.aspx?tlpid=-1&rurl=/design/user/");
            }
            if (!sfMod.UserID.Equals(mu.UserID.ToString())) { function.WriteErrMsg("你无权管理该站点"); }
            SiteID = sfMod.ID;
            mu.SiteID = sfMod.ID;
            return sfMod;
        }
        protected void DownSite_Btn_Click(object sender, EventArgs e)
        {
            DataTableHelper dtHelper = new DataTableHelper();
            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(SiteID);
            M_UserInfo mu = buser.GetLogin();
            ZipClass zip = new ZipClass();
            //------------------------------
            //整合节点与内容,站点信息,生成XML文件
            string xmlDir = function.VToP(sfMod.SiteDir + "Init/");
            DataSet ds = desHelper.PackSiteToDS(sfMod.ID);
            if (!Directory.Exists(xmlDir)) { Directory.CreateDirectory(xmlDir); }
            ds.WriteXml(xmlDir + "Site.xml");
            //页面信息单独存
            DataTable dt = pageBll.U_Sel(mu.UserID, sfMod.ID, M_Design_Page.PageEnum.All);
            dt.DataSet.WriteXml(xmlDir + "Page.xml");
            //模板与资源打包
            string zipDir = function.VToP("/Temp/SiteDown/");
            string zipPath = zipDir + sfMod.ID + ".zip";
            if (!Directory.Exists(zipDir)) { Directory.CreateDirectory(zipDir); }
            zip.Zip(function.VToP(sfMod.SiteDir), zipPath);
            SafeSC.DownFile(function.PToV(zipPath), sfMod.SiteName + ".zip");//打包成为tlp?
        }
        protected void Global_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    pageBll.U_Del(mu.UserID, id);
                    break;
            }
            Response.Redirect(Request.RawUrl);
        }
        protected void Scence_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            switch (e.CommandName)
            {
                case "del2":
                    seBll.U_Del(mu.UserID, Convert.ToInt32(e.CommandArgument));
                    break;
            }
            MyBind();
        }
        protected void H5_BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["h5_idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                seBll.U_Del(buser.GetLogin().UserID, ids);
            }
            MyBind();
        }
    }
}
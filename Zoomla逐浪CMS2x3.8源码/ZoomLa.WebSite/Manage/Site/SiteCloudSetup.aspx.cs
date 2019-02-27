using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class manage_Site_SiteColudSetup : CustomerPageAction
{
    protected IISHelper iisHelper = new IISHelper();
    
    protected DataTable dt = new DataTable();
    protected EnviorHelper enHelper = new EnviorHelper();
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
        if (!IsPostBack)
        {
            codeSource.Text = StationGroup.CodeSourceUrl;
            zipSavePath.Text = StationGroup.ZipSavePath;
          
            serverIP.Text = enHelper.GetServerIP();

            dt = iisHelper.GetWebSiteList();
            siteListDP.DataSource = dt;
            siteListDP.DataTextField = "siteName";
            siteListDP.DataValueField = "siteName";
            siteListDP.DataBind();
            siteListDP.Items.Insert(0, "请选择站点");
            siteListDP.Items.Insert(1, "新建站点");
            siteListDP.Items[1].Attributes["style"] = "color:red;";
            if (!string.IsNullOrEmpty(Request.QueryString["siteName"]))
            {
                siteListDP.SelectedValue = Request.QueryString["siteName"];
                setupBtn.Style.Remove("display");
            }
        }
        Call.HideBread(Master);
    }
    protected void siteListDP_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (siteListDP.SelectedIndex > 2)//不是默认站点
        {
            setupBtn.Visible = true;
        }
        else if(siteListDP.SelectedIndex==1)//新建站点
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "newSite();", true);
            setupBtn.Visible = false;
        }
        else
        {
            setupBtn.Visible = false;
        }
        #region(Disuse)
        //if (ViewState["siteList"] != null)
        //{
        //    dt = ViewState["siteList"] as DataTable;
        //}
        //else 
        //{ 
        //    dt = iisHelper.GetWebSiteList();
        //}
        //dt.DefaultView.RowFilter = "siteName = '" + siteListDP.SelectedValue + "'";
        //DataTable tempDT = dt.DefaultView.ToTable();
        //if (tempDT != null && tempDT.Rows.Count > 0)
        //{
        //    sitePort.InnerText = "端口："+tempDT.Rows[0]["sitePort"].ToString();
        //    domain.InnerText = "域名：" + tempDT.Rows[0]["Domain"].ToString();
        //}
        #endregion
    }
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        StationGroup.CodeSourceUrl = codeSource.Text.Trim();
        StationGroup.ZipSavePath = zipSavePath.Text.Trim();
        //setupPath.Text = setupPath.Text.Trim();//安装路径,如无/加上
        //StationGroup.SetupPath = setupPath.Text.EndsWith("\\") ? setupPath.Text : setupPath.Text += "\\";
        StationGroup.Update();
        if (siteListDP.SelectedIndex > 2)
        {
            Response.Redirect("SiteCloudSetup.aspx?siteName=" + siteListDP.SelectedValue);
        }
    }
    //Restored to the orginal address
    protected void ResetUrl_Click(object sender, EventArgs e)
    {
        codeSource.Text = StationGroup.BackupUrl;
    }
    protected void setupBtn_Click(object sender, EventArgs e)
    {
        string siteName = siteListDP.SelectedValue;
        Response.Redirect("SiteFileManage.aspx?SiteName="+Server.UrlEncode(siteName)+"&index=0&command=beginSetup");
    }
}
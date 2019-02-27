using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model.Site;

public partial class Manage_I_Content_AddSites : System.Web.UI.Page
{
    M_IDC_Sites mod = new M_IDC_Sites();
    B_IDC_Sites bll = new B_IDC_Sites();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? "0": Request.QueryString["ID"].ToString();
            if (Convert.ToInt32(ID) > 0)
            {
                MyBind(Convert.ToInt32(ID));
                Save_Btn.Text = "修改信息";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Content/CollectionManage.aspx'>信息采集</a></li><li><a href='../Content/CollSite.aspx'>子站采集</a></li><li>添加子站</li>");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        SiteUrl_T.Text =  SiteUrl_T.Text.Substring(SiteUrl_T.Text.Length - 1, 1).Equals("/") ? SiteUrl_T.Text : SiteUrl_T.Text + "/";
        SiteUrl_T.Text = SiteUrl_T.Text.ToLower().Contains("http://") ? SiteUrl_T.Text :"http://" + SiteUrl_T.Text; 

        if (Save_Btn.Text.Equals("修改信息"))
        {
            mod.ID = Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["ID"]) ? "0" : Request.QueryString["ID"].ToString());
            mod.SiteName = SiteName_T.Text.ToString().Trim();
            mod.SiteUrl = SiteUrl_T.Text.ToString().Trim();
            mod.SiteDesc = SiteDesc_T.Text.ToString().Trim();
            mod.SiteKey = SiteKey_T.Text.ToString().Trim();
            mod.Nodes = Nodes_T.Text.ToString().Trim();
            mod.CreateTime = System.DateTime.Now;
            mod.LastTime = System.DateTime.Now;
            if (bll.UpdateByID(mod))
            {
                function.WriteSuccessMsg("修改站点信息成功", "CollSite.aspx");
            }
            else
                function.WriteSuccessMsg("修改失败" + mod.ID + "", "CollSite.aspx");
        }
        else
        {
            mod.SiteName = SiteName_T.Text.ToString().Trim();
            mod.SiteUrl = SiteUrl_T.Text.ToString().Trim();
            mod.SiteDesc = SiteDesc_T.Text.ToString().Trim();
            mod.SiteKey = SiteKey_T.Text.ToString().Trim();
            mod.Nodes = Nodes_T.Text.ToString().Trim();
            mod.CreateTime = System.DateTime.Now;
            mod.LastTime = System.DateTime.Now;
            if (bll.Insert(mod) > 0)
                function.WriteSuccessMsg("添加站点信息成功", "CollSite.aspx");
            else
                function.WriteErrMsg("添加失败！", "CollSite.aspx");
        }
       
    }
    private void MyBind(int ID)
    {
        mod=bll.SelReturnModel(ID);
        SiteName_T.Text = mod.SiteName;
        SiteUrl_T.Text = mod.SiteUrl;
        SiteDesc_T.Text = mod.SiteDesc;
        SiteKey_T.Text = mod.SiteKey;
        Nodes_T.Text = mod.Nodes;
    }
}
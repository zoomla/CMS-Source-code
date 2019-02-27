using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_I_Default : CustomerPageAction
{
    string ascxPath = "~/Manage/I/ASCX/";
    protected void Page_Init(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged(Request.RawUrl);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            ScenceFunc(adminMod.Theme);
            nameL.InnerHtml = "<span style='cursor:pointer;'>" + adminMod.AdminName + "</span>";
            AsyncInvokeFunc preFunc = InvokeFunc;
            preFunc.BeginInvoke(this, null, null);
            //---Logo后期改为拼接
            if (!string.IsNullOrEmpty(SiteConfig.SiteInfo.LogoAdmin))
            {
                zlogo3.Visible = true;
                zlogo4.Visible = true;
                newbody_logo2.Visible = true;
                zlogo3.InnerHtml = ComRE.Img_NoPic(SiteConfig.SiteInfo.LogoAdmin, "");
                zlogo4.InnerHtml = SiteConfig.SiteInfo.LogoPlatName;
            }
            else
            {
                zlogo1.Visible = true;
                zlogo2.Visible = true;
                newbody_logo1.Visible = true;
            }
            if (Call.AppSettValue("ShowedAD").ToLower().Equals("false"))
            {
                function.Script(this, "ShowStartScreen();");
            }
            if (Application["Page_Init"] == null) { Application["Page_Init"] = true; function.Script(this, "PrePageInit()"); }
        }
    }
    //-----场景控制
    public void ScenceFunc(string theme = "")
    {
        if (string.IsNullOrEmpty(theme) || theme.Length < 100)
        {
            theme = ZoomLa.Components.SiteConfig.SiteOption.Desk;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "desk", "ScenceFunc('" + theme + "');", true);
    }
    protected void leftSwitch_Btn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["left_Hid"])) return;
        string ascxName = Request.Form["left_Hid"].ToLower();
        UserControl control = null;
        if (ascxName.IndexOf("?") > 0)
        {
            //NodeTree.ascx?url=Content/ContentRecycle.aspx
            string param = ascxName.Split('?')[1];
            ascxName = ascxName.Split('?')[0];
            control = (UserControl)Page.LoadControl(ascxPath += ascxName);
            control.Attributes["Url"] = param.Split('=')[1];
        }
        else { control = (UserControl)Page.LoadControl(ascxPath += ascxName); }
        left_Div.Controls.Add(control);
        ScriptManager.RegisterStartupScript(LeftPanel, LeftPanel.GetType(), "", "<script>ascxInit('" + ascxName + "');ClearSpin();DivCache('" + ascxName.ToLower() + "');setLayout();</script>", false);
    }
    //----异步预加载
    public delegate void AsyncInvokeFunc(Page p);
    public void InvokeFunc(Page p)
    {
        string[] ascxVPath = { "~/Manage/I/ASCX/NodeTree.ascx", "~/Manage/I/ASCX/UserGuide.ascx"};
        for (int i = 0; i < ascxVPath.Length; i++)
        {
            p.LoadControl(ascxVPath[i]);
        }
    }
    protected void NoShow_Btn_Click(object sender, EventArgs e)
    {
        Call.AppSettUpdate("ShowedAD", "true");
    }
    protected void ShowAD_Btn_Click(object sender, EventArgs e)
    {
        Call.AppSettUpdate("ShowedAD", "false");
        Response.Redirect(Request.RawUrl);
    }
    public string CreateSiteColorCSS() 
    {
        string color = SiteConfig.SiteOption.SiteColor;
        if (color.Equals("#428bca")) { return ""; }
        else 
        {
            string rgb = colorHx16toRGB(color);
            string csswrap = "<style type=\"text/css\">";
            csswrap+="#left .m_left_ul .m_left_ulin li a:hover {background-color:"+color+";}";
            csswrap += "#left .m_left_ulin li p {background-color:" + color + ";}";
            //csswrap += "#left .m_left_ul .m_left_ulin li a {color:" + color + ";}";
            csswrap += "#nodeNav .left_ul ul li a:hover {background-color:" + string.Format(rgb, 0.6) + ";}";
            //csswrap += "#nodeNav .left_ul ul li a {color:" + color + ";}";
            csswrap += ".m_top {background-color:" + color + ";}";
            csswrap += ".tab-strip .current {background-color:" + string.Format(rgb, 0.6) + ";}";
            csswrap += ".SelectedA {background-color:" + string.Format(rgb, 0.6) + ";color:#fff;}";
            csswrap += "a.SelectedA:focus,a.SelectedA:hover {color:#fff;}";
            csswrap += "</style>";
            return csswrap;
        }
    }
    public string colorHx16toRGB(string strHxColor)
    {
        System.Drawing.Color color = System.Drawing.Color.FromArgb(System.Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
        return "rgba(" + color.R + "," + color.G + "," + color.B + ",{0})";
    }
}
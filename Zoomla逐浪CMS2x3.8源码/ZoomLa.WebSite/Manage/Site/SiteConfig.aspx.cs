using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class manage_Site_AdminLogin : CustomerPageAction
{
    B_Admin badmin = new B_Admin();
    IdentityAnalogue ia = new IdentityAnalogue();
    EnviorHelper enHelper = new EnviorHelper();
    DataTableHelper dtHelper = new DataTableHelper();
    URLRewriter.CryptoHelper crHelper = new URLRewriter.CryptoHelper();
    IISHelper iisHelper = new IISHelper();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //此处控件未实例化，不能对控件进行操作
        Call.HideBread(Master); 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        badmin.CheckIsLogin();
        string s = "";
        if (ia.CheckSAIsValid(out s)) //检测,如果当前未配置管理员与用户,则提示让其配置
           remind.InnerText = s;
        else
            remind.InnerText = s;

        if (!IsPostBack)
        {
            //dPageChk.Checked = SiteConfig.SiteOption.SiteManageMode == 1 ? true : false;
            if (!string.IsNullOrEmpty(Request.QueryString["remind"]))
                remind.InnerText = Request.QueryString["remind"];
            defaultIP.Text = string.IsNullOrEmpty(StationGroup.DefaultIP) ? "http://" + Request.Url.Host + ":" + Request.Url.Port + "/" : StationGroup.DefaultIP;
            adminName.Text = crHelper.Decrypt(StationGroup.SAName);
            //adminPasswd.Attributes.Add("Value",crHelper.Decrypt(StationGroup.SAPassWord));
            //defaultIP.Text = sg.DefaultIP;
            //serverIP.Text = enHelper.GetServerIP();
            //------IDC配置
            newNetClientID.Text = StationGroup.newNetClientID;
            newNetApiPasswd.Text = StationGroup.newNetApiPasswd;
            nodeIDT.Text = StationGroup.NodeID;
            modelIDT.Text = StationGroup.ModelID;
            dnsOutputPath.Text = StationGroup.DnsOutputPath;
            autoCreatedbRadio.SelectedValue = StationGroup.AutoCreateDB.ToString();
            dbm_NameT.Text = StationGroup.DBManagerName;
            tDomNameT.Text = StationGroup.TDomName;
            if (!string.IsNullOrEmpty(StationGroup.DnsOption))
            {
                dns1.Text = StationGroup.DnsOption.Split(',')[0];
                dns2.Text = StationGroup.DnsOption.Split(',')[1];
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setRadio(" + 1 + ");", true);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "setDefaultCheck('" + StationGroup.DefaultDisplay + "','Div3');setDefaultCheck('" + StationGroup.DefaultCheck + "','Div4');", true);

        }
    }
    //登录,因为Chrome自动填充问题,所以与IDC保存分开
    protected void logBtn_Click(object sender, EventArgs e)
    {
        string name = adminName.Text.Trim();
        string passwd = adminPasswd.Text.Trim();
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户名和密码不能为空');", true);
            return;
        }
        URLRewriter.CryptoHelper cr = new URLRewriter.CryptoHelper();
        StationGroup.EnableSA = true;
        StationGroup.SAName = cr.Encrypt(name);
        StationGroup.SAPassWord = cr.Encrypt(passwd);
        StationGroup.DefaultIP = defaultIP.Text.Trim();
        StationGroup.Update();

        //-----更新网站列表至数据库,无则插入
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        iisHelper.SyncDB();
        //-----
        Response.Redirect("Default.aspx");
    }
    protected void chkBtn_Click(object sender, EventArgs e)
    {
        string name = adminName.Text.Trim();
        string passwd = adminPasswd.Text.Trim();
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户名和密码不能为空');", true);
        }
        else
        {
            if (ia.ImpersonateValidUser(name, "", passwd))
            {
                function.WriteSuccessMsg("验证成功,可以正常登录");
            }
            else
            {
                function.WriteErrMsg("验证失败,该账户无法成功登录");
            }
        }
    }
    //IDC保存配置
    protected void saveBtn4_Click(object sender, EventArgs e)
    {
        StationGroup.newNetClientID = newNetClientID.Text.Trim();
        StationGroup.newNetApiPasswd = string.IsNullOrEmpty(newNetApiPasswd.Text.Trim()) ? StationGroup.newNetApiPasswd : newNetApiPasswd.Text.Trim();
        StationGroup.DefaultDisplay = Request.Form["ext100"] + "," + Request.Form["ext101"];
        StationGroup.DefaultCheck = Request.Form["ext"] + "," + Request.Form["ext2"];
        StationGroup.ModelID = modelIDT.Text.Trim();
        StationGroup.NodeID = nodeIDT.Text.Trim();
        //自定义Dns
        if (Request.Form["dnsOption"].Equals("1") && !string.IsNullOrEmpty(dns1.Text.Trim() + dns2.Text.Trim()))
        {
            StationGroup.DnsOption = dns1.Text.Trim() + "," + dns2.Text.Trim();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ra", "setRadio(" + 1 + ");", true);
        }
        else
        {
            StationGroup.DnsOption = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ra", "setRadio(" + 0 + ");", true);
        }
        StationGroup.DnsOutputPath=dnsOutputPath.Text.Trim();
        StationGroup.AutoCreateDB = Convert.ToBoolean(autoCreatedbRadio.SelectedValue);
        StationGroup.TDomName = tDomNameT.Text.Trim();
        if (!string.IsNullOrEmpty(dbm_NameT.Text.Trim()) && !string.IsNullOrEmpty(dbm_PasswdT.Text.Trim()))//数据库用户名与密码不为空才更新存入
        {
            StationGroup.DBManagerName = dbm_NameT.Text.Trim();
            StationGroup.DBManagerPasswd = dbm_PasswdT.Text.Trim();
        }
        StationGroup.Update();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "setDefaultCheck('" + StationGroup.DefaultDisplay + "','Div3');setDefaultCheck('" + StationGroup.DefaultCheck + "','Div4');", true);
    }
}
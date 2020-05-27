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
using ZoomLa.Components;
using System.IO;
using ZoomLa.Model;
//本页已经完成

namespace ZoomLaCMS.Manage.Config
{
    public partial class IPLockConfig : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">IP访问限定</li>" + Call.GetHelp(7));
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                this.LockIPType.SelectedValue = SiteConfig.IPLockConfig.LockIPType;
                this.IPLockBlack.Value = SiteConfig.IPLockConfig.LockIPBlack;
                this.IPLockWhite.Value = SiteConfig.IPLockConfig.LockIPWhite;
                this.AdminLockIPType.SelectedValue = SiteConfig.IPLockConfig.AdminLockIPType;
                this.IPLockAdminBlack.Value = SiteConfig.IPLockConfig.AdminLockIPBlack;
                this.IPLockAdminWhite.Value = SiteConfig.IPLockConfig.AdminLockIPWhite;
                this.NeedCheckRefer.Text = SiteConfig.SiteOption.NeedCheckRefer;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SiteConfig.IPLockConfig.LockIPType = this.LockIPType.SelectedValue;
            SiteConfig.IPLockConfig.LockIPWhite = this.IPLockWhite.Value;
            SiteConfig.IPLockConfig.LockIPBlack = this.IPLockBlack.Value;
            SiteConfig.IPLockConfig.AdminLockIPType = this.AdminLockIPType.SelectedValue;
            SiteConfig.IPLockConfig.AdminLockIPWhite = this.IPLockAdminWhite.Value;
            SiteConfig.IPLockConfig.AdminLockIPBlack = this.IPLockAdminBlack.Value;
            SiteConfig.SiteOption.NeedCheckRefer = this.NeedCheckRefer.Text.Replace(" ", "").ToLower();
            Context.Application["needCheckRefer"] = SiteConfig.SiteOption.NeedCheckRefer;
            try
            {
                SiteConfig.Update();
                function.WriteSuccessMsg("保存成功！", customPath2 + "Config/IPLockConfig.aspx");
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到！", "IPLockConfig.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限。", "../Config/IPLockConfig.aspx");
            }
        }
    }
}
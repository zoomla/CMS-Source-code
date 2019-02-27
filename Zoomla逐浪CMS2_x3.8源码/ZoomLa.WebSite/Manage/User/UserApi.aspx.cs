namespace ZoomLa.WebSite.Manage.User
{
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
    using ZoomLa.API;
    using ZoomLa.BLL;
    using ZoomLa.Components;
    using ZoomLa.Common;

    public partial class UserApi : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!this.IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='UserManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li>整合接口配置 [<a href='SiteManage.aspx'>站点管理</a>]</li>");
            }
        }
        public void MyBind()
        {
            ServerIP_T.Text = StationGroup.RemoteUrl;
            Token_T.Text = StationGroup.Token;
            DBName_T.Text = StationGroup.DBName;
            UName_T.Text = StationGroup.DBUName;
            //RBLDZ.Checked = ApiCfg.DisCuzz.Equals("1");
            if (ZoomLa.SQLDAL.DBHelper.View_IsExist("ZL_User"))
            {
                remind.InnerText = "当前已开启站群用户整合,默认访问" + StationGroup.RemoteUrl + "主站的用户数据";
                remind.Attributes.Add("class", "alert alert-danger");
                RemoteEnable.Checked = true;
            }
            else
            {
                remind.InnerText = "当前未开启站群用户整合,默认使用本地用户数据";
                remind.Attributes.Add("class", "alert alert-info");
                RemoteEnable.Checked = false;
            }
            if (string.IsNullOrEmpty(ServerIP_T.Text))
            {
                Response.Redirect("UApiConfig.aspx");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //StationGroup.RemoteUser = this.Remote_Chk.Checked;
            //StationGroup.RemoteUrl = UrlCheck(this.Remote_Url.Text);
            //StationGroup.Token = this.Token_T.Text;
            //this.ApiCfg.ApiEnable = this.RBLEnable.Checked ? "1" : "0";
            //this.ApiCfg.SysKey = this.TxtSysKey.Text.Trim();
            //this.ApiCfg.Urls = this.TxtUrls.Text.Trim();
            //this.ApiCfg.Discuz = this.Discuz.Checked ? "1" : "0";
        }
        //确保返回http://xxx.xxx.xx/格式的url
        public string UrlCheck(string url)
        {
            url = url.ToLower().Replace(" ", "").TrimEnd('/');
            if (string.IsNullOrEmpty(url)) return url;
            if (url.IndexOf("://") < 0)
            {
                url = "http://" + url;
            }
            return url + "/";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManage.aspx");
        }
        //----------------站群整合
        string pre = "<br>";
        string tables = "ZL_User,ZL_UserBase,ZL_UserBaseField,ZL_Group";
        //创建链接服务器,并执行视图语句
        protected void Begin_Btn_Click(object sender, EventArgs e)
        {
            Sql_Div.InnerHtml = "--/第1步***创建数据库链接服务器(同一实例已经运行过此整合可忽略第1步)****/--" + pre;
            Sql_Div.InnerHtml += pre + "--/请先检查你的数据库是否选对/--" + pre;
            //创建链接服务器(如果存在,则先删除)
            string delLinkSql = "if exists(select * from master.dbo.sysservers where isremote=0 and srvname='ZLRemote')exec sp_dropserver 'ZLRemote','droplogins' ";
            Sql_Div.InnerHtml += pre + delLinkSql;
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_addlinkedserver @server = N'ZLREMOTE', @srvproduct=N'ZLREMOTE', @provider=N'SQLNCLI', @datasrc=N'" + ServerIP_T.Text + "'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'ZLREMOTE',@useself=N'False',@locallogin=NULL,@rmtuser=N'" + UName_T.Text + "',@rmtpassword='" + Pwd_T.Text + "'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'collation compatible', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'data access', @optvalue=N'true'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'dist', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'pub', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'rpc', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'rpc out', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'sub', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'connect timeout', @optvalue=N'0'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'collation name', @optvalue=null";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'lazy schema validation', @optvalue=N'false'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'query timeout', @optvalue=N'0'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'use remote collation', @optvalue=N'true'";
            Sql_Div.InnerHtml += pre + "EXEC master.dbo.sp_serveroption @server=N'ZLREMOTE', @optname=N'remote proc transaction promotion', @optvalue=N'true'";
            Sql_Div.InnerHtml += pre + pre + pre + "--/第2步****备份后删除本地用户表进行整合***/--" + pre;
            foreach (string table in tables.Split(','))//改名本地表
            {
                string renameSql = string.Format("if object_id(N'{0}',N'U') is not null EXEC sp_rename '{0}', '{0}_Bak'", table);
                Sql_Div.InnerHtml += pre + renameSql;
            }
            Sql_Div.InnerHtml += pre + pre + pre + "--/第3步****创建整合视图***/--" + pre;
            foreach (string table in tables.Split(','))//创建视图
            {
                Sql_Div.InnerHtml += pre + "GO";
                string viewSql = "Create VIEW " + table + " as SELECT * FROM ZLRemote." + DBName_T.Text + ".dbo." + table;
                Sql_Div.InnerHtml += pre  + viewSql;
            }
        }
        //取消跨站,删除视图,改表名
        protected void Cancel_Btn_Click(object sender, EventArgs e)
        {
            Sql_Div.InnerHtml = "--/第1步**********移除链接服务器*************************/";
            Sql_Div.InnerHtml += pre + "--/请先检查你的数据库是否选对/";
            Sql_Div.InnerHtml += pre + "if exists(select * from master.dbo.sysservers where isremote=0 and srvname='ZLRemote')exec sp_dropserver 'ZLRemote','droplogins' ";

            Sql_Div.InnerHtml += pre + pre + "--/第2步**********移除视图*************************/";
            foreach (string table in tables.Split(','))
            {
                string viewSql = "IF EXISTS(SELECT 1 FROM sys.views WHERE name='" + table + "')DROP VIEW " + table;
                Sql_Div.InnerHtml += pre + viewSql;
            }
            Sql_Div.InnerHtml += pre + pre + pre + "--/第3步**********改名数据表*************************/";
            foreach (string table in tables.Split(','))//改名本地表
            {
                string renameSql = string.Format("if object_id(N'{0}_Bak',N'U') is not null EXEC sp_rename '{0}_Bak', '{0}'", table);
                Sql_Div.InnerHtml += pre + renameSql;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            StationGroup.RemoteUrl = ServerIP_T.Text;
            StationGroup.Token = Token_T.Text;
            StationGroup.DBName = DBName_T.Text;
            StationGroup.DBUName = UName_T.Text;
            StationGroup.RemoteEnable = true;
            StationGroup.Update();
            //ApiCfg.DisCuzz = this.RBLDZ.Checked ? "1" : "0";
            //ApiCfg.SaveConfig();
            function.WriteSuccessMsg("配置保存成功");
        }
}
}
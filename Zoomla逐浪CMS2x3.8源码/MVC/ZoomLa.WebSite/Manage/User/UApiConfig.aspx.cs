using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.ECharts;
using ZoomLa.Common;
using ZoomLa.Components;
using Newtonsoft.Json;
using ZoomLa.BLL;


namespace ZoomLaCMS.Manage.User
{
    public partial class UApiConfig : System.Web.UI.Page
    {
        public int Step { get { return DataConverter.CLng(Request.QueryString["step"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "saveinfo":
                        result = WebForImgCode(Request.Form["name"], Request.Form["demon"], Request.Form["ip"], Request.Form["url"]);
                        break;
                }
                Response.Write(result);
                Response.Flush(); Response.End();
            }
            else if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='UserManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li>整合接口配置</li>");
            }
        }
        //请求并返回图片数据json(格式)
        public string WebForImgCode(string name, string domain, string ip, string url)
        {
            try
            {
                WSHelper wshelper = new WSHelper();
                string result = "";
                object[] objs = new object[3];
                objs[0] = name;
                objs[1] = domain;
                objs[2] = ip;
                result = wshelper.InvokeWS("http://" + url + "/Api/SiteGroupFunc.asmx", "SiteGroup", "SiteGroupFunc", "SaveSiteInfo", objs).ToString();
                return BindImgCode(url, JsonConvert.DeserializeObject<JArray>(result));
            }
            catch (Exception)
            {
                JObject obj = JObject.FromObject(new
                {
                    sname = SiteConfig.SiteInfo.SiteName,
                    domain = Request.Url.Authority,
                    sip = Request.UserHostAddress
                });
                JArray arrays = new JArray();//SiteConfig.SiteInfo.SiteName, Request.Url.Authority, Request.UserHostAddress
                arrays.Add(obj);
                return BindImgCode(url + "(未连接)", arrays);
            }

        }
        //生成图片数据
        public string BindImgCode(string sitename, JArray jarray)
        {
            ChartOption option = new ChartOption();
            option.title.text = "站点关系:用户整合";
            option.title.subtext = "数据来自" + sitename;
            option.legend.data = new string[] { "主站", "子站", "二级子站" };
            option.series.Add(new ChartSeries()
            {
                name = "站点关系",
                categories = new Categories[] { new Categories("主站"), new Categories("子站"), new Categories("二级子站") },
                nodes = GetPicData(sitename, jarray),
                links = GetPicLinks(GetPicData(sitename, jarray), sitename),
                itemStyle = new SeriesItemStyle() { normal = new ChartNormal() { label = new ChartLabel() { textStyle = new TextStyle() { color = "#333" } } } }
            });
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return "option=" + JsonConvert.SerializeObject(option, Formatting.None, setting) + ";";
        }
        public void MyBind()
        {
            ServerIP_T.Text = StationGroup.RemoteUrl;
            sip_t.Text = StationGroup.RemoteUrl;
            //Token_T.Text = StationGroup.Token;
            DBName_T.Text = StationGroup.DBName;
            UName_T.Text = StationGroup.DBUName;
            //RemoteEnable.Checked = StationGroup.RemoteEnable;
            if (Step == 2)
            {

                code.Value = WebForImgCode(SiteConfig.SiteInfo.SiteName, Request.Url.Authority, Request.UserHostAddress, StationGroup.RemoteUrl);
                function.Script(this, "SelMod();");
            }
        }
        //----------------站群整合
        string pre = "<br>";
        string tables = "ZL_User,ZL_UserBase,ZL_UserBaseField,ZL_Group";
        //创建链接服务器,并执行视图语句
        protected void Begin_Btn_Click(object sender, EventArgs e)
        {
            Sql_Div.InnerHtml = "--/第1步**********创建链接服务器*************************/";
            Sql_Div.InnerHtml += pre + "--/请先检查你的数据库是否选对/";
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
            Sql_Div.InnerHtml += pre + pre + pre + "--/第2步**********改名数据表*************************/";
            foreach (string table in tables.Split(','))//改名本地表
            {
                string renameSql = string.Format("if object_id(N'{0}',N'U') is not null EXEC sp_rename '{0}', '{0}_Bak'", table);
                Sql_Div.InnerHtml += pre + renameSql;
            }
            Sql_Div.InnerHtml += pre + pre + pre + "--/第3步**********创建视图(必须逐句执行)*************************/";
            foreach (string table in tables.Split(','))//创建视图
            {
                string viewSql = "Create VIEW " + table + " as SELECT * FROM ZLRemote." + DBName_T.Text + ".dbo." + table;
                Sql_Div.InnerHtml += pre + pre + viewSql;
            }
            function.Script(this, "BindClip();");
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
            function.Script(this, "BindClip();");
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            StationGroup.RemoteUrl = ServerIP_T.Text;
            //StationGroup.Token = Token_T.Text;
            StationGroup.DBName = DBName_T.Text;
            StationGroup.DBUName = UName_T.Text;
            StationGroup.RemoteEnable = true;
            StationGroup.Update();
            Response.Redirect("UserAPI.aspx");
        }

        public List<ChartNode> GetPicData(string sitename, JArray jarray)
        {
            List<ChartNode> nodeList = new List<ChartNode>();
            //SiteModel model = new SiteModel();
            ChartNode node = new ChartNode();
            node.category = 0;
            node.name = sitename;
            node.url = "http://" + sitename;
            node.value = "10";
            node.initial = new int[] { 500, 40 };
            node.fixX = true; node.fixY = true;
            node.draggable = true;
            nodeList.Add(node);

            //添加子站信息
            for (int i = 1; i <= jarray.Count; i++)
            {
                ChartNode childNode = new ChartNode();
                childNode.category = 1;
                childNode.name = jarray.ToArray()[i - 1]["domain"].ToString();
                childNode.value = "5";
                childNode.initial = new int[] { i * 150, 200 };
                childNode.fixX = true; childNode.fixY = true;
                childNode.url = "http://" + childNode.name;
                nodeList.Add(childNode);
            }
            return nodeList;
            //return JsonConvert.SerializeObject(nodeList);
        }
        public List<ChartLink> GetPicLinks(List<ChartNode> nodeList, string sitename)
        {
            List<ChartLink> linkList = new List<ChartLink>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                ChartNode node = nodeList[i];
                ChartLink link = new ChartLink();
                link.name = "链接" + i;
                link.source = node.name;
                link.target = sitename;
                //link..linkStyle.type = "curve";
                linkList.Add(link);
            }
            return linkList;
            //return JsonConvert.SerializeObject(linkList);
        }
    }
}
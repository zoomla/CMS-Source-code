using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
/// <summary>
/// SiteGroupFunc 的摘要说明
/// </summary>
[WebService(Namespace = "SiteGroup")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SiteGroupFunc : System.Web.Services.WebService
{
   private Dictionary<string, string> sqlDic = new Dictionary<string, string>();
   private IISHelper iisHelper = new IISHelper();
   private ServerManager iis = new ServerManager();
   private B_Node nodeBll = new B_Node();
    public SiteGroupFunc()
   {
       //IdentityAnalogue ia = new IdentityAnalogue();
       //ia.CheckEnableSA();
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        sqlDic.Add("GetAllCommonModel", "Select *,b.NodeName,b.NodeID from ZL_CommonModel as a left join ZL_Node as b on a.NodeID=b.NodeID Where a.TableName = 'ZL_C_Article' And (IsCatch = 0 or IsCatch is Null);");
        sqlDic.Add("SelAllArticle", "Select b.* From ZL_CommonModel as a Left Join ZL_C_Article as b  on a.itemID=b.ID Where a.tablename='ZL_C_Article' And (IsCatch = 0 OR IsCatch = null)");
    }
   
    [WebMethod]
    public DataTable GetDataTable(object o)
    {
        string sql = sqlDic[o.ToString()];
        return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
    }
    [WebMethod]
    public void UpdateStatus(string ids)
    {
        if (string.IsNullOrEmpty(ids)) return;
        SafeSC.CheckIDSEx(ids);
        string sql = string.Format("Update ZL_CommonModel set IsCatch =1 where GeneralID in ({0})", ids);//生成SQL开始执行
        SqlHelper.ExecuteTable(CommandType.Text, sql);
    }
    [WebMethod]
    public string ServicesIsOK(string s) 
    {
        /*
         * 1,成功;0,数据库无法连接
         */
        return "1";
    }
    [WebMethod]//返回本站点的SiteName
    public string GetSiteName() 
    {
        return SiteConfig.SiteInfo.SiteName;
    }
    [WebMethod]
    public string GetCustomPath() //返回自定义路径
    {
        return CustomerPageAction.customPath2;
    }
    [WebMethod]
    public string GetToken(string a, string p)//简单Token不使用种子,同时只能有一个Token存在,传管理员与密码(加密)
    {
        //URLRewriter.CryptoHelper cr = new URLRewriter.CryptoHelper();
        //p= cr.Decrypt(p);
        string token = "";
        //if (B_Admin.IsExist(a, p))
        //{
        //    token = function.GetRandomString(12);
        //    Application.Add("SiteToken",token);
        //    Application.Add("SiteUser",a);
        //    Application.Add("SitePasswd",p);
        //}
        return token;
    }
    //-------------DataTable
    [WebMethod]
    public DataTable InvokeNoParamRDataTable(string methodName)//调用无参方法，返回DataTable
    {
        Assembly assembly = Assembly.LoadFrom(@"D:\Web\Test\Bin\App_Code.dll");
        Type T = assembly.GetType("IISHelper");
        MethodInfo mi = T.GetMethod(methodName, new Type[] { });
        object o = Activator.CreateInstance(T);//这里是指定构造方法
        object[] par = new object[] { };//object[] par = new object[] { "E01" };带和不带参
        DataTable dt = (DataTable)mi.Invoke(o, par);
        dt.TableName = SiteConfig.SiteInfo.SiteName;
        return dt;
    }
    [WebMethod]
    public DataTable InvokeRDataTable1(string methodName, string param)//带一个参数返回DataTable
    {
        DataTable dt = new DataTable();
        Assembly assembly = Assembly.LoadFrom(@"D:\Web\Test\Bin\App_Code.dll");
        Type T = assembly.GetType("IISHelper");
        MethodInfo mi = T.GetMethod(methodName, new Type[] {typeof(string) });
        object o = Activator.CreateInstance(T);//这里是指定构造方法
        object[] par = new object[] { param };//object[] par = new object[] { "E01" };带和不带参
        object obj=mi.Invoke(o, par);
        if((obj as DataTable)!=null)
            dt = obj as DataTable;
        dt.TableName = SiteConfig.SiteInfo.SiteName;
        return dt;
    }

    //--------------------内容收集
    /// <summary>
    /// 用于主站获取子站内容列表
    /// </summary>
    /// <param name="key">AES密钥</param>
    /// <param name="nodeids">需获取的NodeID,为空则获取全部</param>
    [WebMethod]//密钥验证
    public DataTable GetContentDT(string key, string nodeids)
    {
        string sql = "Select GeneralID,NodeID,Title,CreateTime,Inputer,TableName,[Status],HtmlLink From ZL_CommonModel Where [Status]=99";
        DataTable dt = new DataTable();
        string siteName = SiteConfig.SiteInfo.SiteName; ;
        dt.TableName = siteName;
        if (!string.IsNullOrEmpty(nodeids))
        {
            SafeSC.CheckIDSEx(nodeids);
            sql += " And NodeID in(" + nodeids + ")";
        }
        if (!string.IsNullOrEmpty(SiteConfig.SiteOption.SiteCollKey) && key.Equals(EncryptHelper.AESEncrypt(SiteConfig.SiteOption.SiteCollKey)))
        {
            string url = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/');
            dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            dt.Columns.Add(new DataColumn("PageUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["SiteName"] = siteName;
                if (!string.IsNullOrEmpty(dr["HtmlLink"].ToString()))
                {
                    dr["PageUrl"] = url + dr["HtmlLink"];
                }
                else//未生成静态页
                {
                    string tbname = dr["TableName"].ToString();
                    if (tbname.Contains("ZL_C_"))
                    {
                        dr["PageUrl"] = url + "/Item/" + dr["GeneralID"] + ".aspx";
                    }
                    else if (tbname.Contains("ZL_S_") || tbname.Contains("ZL_P_"))
                    {
                        dr["PageUrl"] = url + "/Shop/" + dr["GeneralID"] + ".aspx";
                    }
                    //else if (tbname.Contains("ZL_Page_"))///Page/Pagecontent.aspx?Pageid=8&itemid=9686
                    //{
                    //    tr["PageUrl"] = url + "/Item/" + tr["GeneralID"] + ".aspx";
                    //}
                }
            }//for end;
        }
        return dt;
    }
    [WebMethod]//公开
    public DataTable GetNodeList() 
    {
        return nodeBll.SelectNodeHtmlXML();
    }
    [WebMethod]//网络拓扑图（返回网络拓扑数据）
    public string SaveSiteInfo(string name, string domain, string ip)
    {
        string ppath = function.VToP("/Config/ServerInfo.config");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(ppath);
        XmlNode node = xmldoc.SelectSingleNode("/Nodes/sites");
        JArray jarr = new JArray();
        JObject obj = new JObject(); obj["sname"] = name; obj["domain"] = domain; obj["sip"] = ip;
        if (!string.IsNullOrEmpty(node.InnerText))
        {
            jarr = JsonConvert.DeserializeObject<JArray>(node.InnerText);
            if (jarr.Where(p => p["sname"].ToString().Equals(name)&&p["domain"].ToString().Equals(domain)).ToArray().Length < 1)
            {

                jarr.Add(obj);
            }
        }
        else
        {
            jarr.Add(obj);
        }
        string json = JsonConvert.SerializeObject(jarr);
        node.InnerText = json;
        xmldoc.Save(ppath);
        return json;
    }
}

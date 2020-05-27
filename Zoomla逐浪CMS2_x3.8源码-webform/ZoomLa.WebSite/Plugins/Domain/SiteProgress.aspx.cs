using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;


public partial class Plugins_Domain_SiteProgress : System.Web.UI.Page
{
    /*
     * 下载源码,解压源码,下载模板,解压模板,更新配置
     */
    protected B_User buser = new B_User();
    protected IISHelper iisHelper = new IISHelper();
    protected B_IDC_DBList dbBll = new B_IDC_DBList();
    protected M_IDC_DBList dbMod = new M_IDC_DBList();
    public string[] actionArr = new string[] { "getCodeP", "getTempP", "getUnzipP"};
    protected void Page_Load(object sender, EventArgs e)
    {
        function.WriteErrMsg("建站功能默认关闭,请联系管理员开启");
        //B_User.CheckIsLogged(Request.RawUrl);
        ////-------------------AJAX
        //if (function.isAjax())
        //{
        //    string action = Request.Form["action"];
        //    string value=Request.Form["value"];
        //    int userID = buser.GetLogin().UserID;
        //    Response.Clear();
        //    switch (action)
        //    {
        //        case "getCodeP":
        //            Response.Write(ZipClass.GetPercent(Convert.ToInt64(Application[userID + "downCodeT"]), Convert.ToInt64(Application[userID + "downCodeP"])));
        //            break;
        //        case "getUnzipP":
        //            Response.Write(ZipClass.GetPercent(Convert.ToInt64(Application[userID + "uzT"]), Convert.ToInt64(Application[userID + "uzP"])));
        //            break;
        //        case "getTempP":
        //            Response.Write(ZipClass.GetPercent(Convert.ToInt64(Application[userID + "downTempT"]), Convert.ToInt64(Application[userID + "downTempP"])));
        //            break;
        //    }
        //    Response.End();
        //}
        ////-------------------
        //if (!IsPostBack)
        //{
        //    if (Session["siteInfo"] != null)//开始创建站点
        //    {
        //        IISWebSite site = Session["siteInfo"] as IISWebSite;
        //        CurrentSite = site;
        //        domNameL.Text = site.DomainName;
        //        BeginWork(site);
        //        Session.Remove("siteInfo");
        //    }
        //    else
        //    {
        //        function.WriteErrMsg("请按步骤来，点击<a href='/SiteDefault.aspx' style='color:blue'>返回第一步!!</a>");
        //    }
        //}
    }
    public delegate void UnzipFile(int userID, HttpContext ct, string sPath, string tPath);
    public delegate void DownFile(int userID, HttpContext ct, string sUrl, string tPath, IISWebSite site);
    //application中标识,上下文,下载地址,本地物理路径
    public void DownFileWork(int userID, HttpContext ct, string sUrl, string tPath,IISWebSite site)
    {
        try
        {
            DownloadFile(sUrl, tPath, userID + "downCodeP", ct);//下载源码
            UnzipFileWork(userID, ct, @site.PhysicalPath + "SourceCode.zip", @site.PhysicalPath);//解压

            string localTempZipFile = @site.PhysicalPath + "Template\\" + site.TempDir + ".zip"; ;//D:\Web\Site\Template\blue\
            string localTempDir = @site.PhysicalPath + "Template\\";
            DownloadFile(site.TempUrl, localTempZipFile, userID + "downTempP", ct);//下载模板
            UnzipFileWork(userID, ct, localTempZipFile, localTempDir);//解压

            UpdateConfig(site);//更新配置
        }
        catch(Exception) {  }
    }
    //来源文件,目标目录
    public void UnzipFileWork(int userID, HttpContext ct, string sPath, string tPath)
    {
        FileInfo f = new FileInfo(sPath); ;//获取文件大小，用于判断进度
        ZipClass zip = new ZipClass();
        ct.Application[userID + "uzT"] = f.Length;//需要解压的文件总字节数
        zip.UnZipFiles(sPath, tPath, userID + "uzP", ct);
    }
    /// <summary>
    /// 从指定站点下载文件，并保存在指定位置,//下载，迁入ZipClass
    /// </summary>
    /// <param name="strUrl">目标Url</param>
    /// <param name="strFileName">本地物理路径</param>
    /// <param name="flag">appliatoin[flag]用于存进度</param>
    /// <param name="ct">当前上下文HttpContext</param>
    /// <param name="begin">从指定位置开始下载(未实现)</param>
    public void DownloadFile(string strUrl, string strFileName, string flag = "", HttpContext ct = null, int begin = 0)
    {
        //已完成的,1%长度
        long percent = 0; 
        long sPosstion = 0;//磁盘现盘文件的长度
        //long count = 0;// count += sPosstion,从指定位置开始写入字节
        FileStream FStream;
        if (File.Exists(strFileName))//如果文件存在
        {
            FStream = File.OpenWrite(strFileName);//打开继续写入,并从尾部开始,用于断点续传
            sPosstion = FStream.Length;
            FStream.Seek(sPosstion, SeekOrigin.Current);//移动文件流中的当前指针
        }
        else
        {
            FStream = new FileStream(strFileName, FileMode.Create);
            sPosstion = 0;
        }
        //打开网络连接
        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            //if (CompletedLength > 0)//断点续传
            //    myRequest.AddRange((int)CompletedLength);//设置Range值,即头，从指定位置开始接收文件..
            //向服务器请求，获得服务器的回应数据流
            HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
            long FileLength = webResponse.ContentLength;//文件大小
            percent = FileLength / 100;
            Stream myStream = webResponse.GetResponseStream();
            byte[] btContent = new byte[myStream.Length];
            myStream.Read(btContent, 0, btContent.Length);
            SafeSC.SaveFile(Path.GetDirectoryName(strFileName) + "\\", Path.GetFileName(strFileName), btContent);
            //while ((count = myStream.Read(btContent, 0, 1024)) > 0)//返回读了多少字节,为0表示全部读完
            //{
            //    FStream.Write(btContent, 0, count);//知道有多少个数字节后再写入
            //    CompletedLength += count;
            //    if(ct!=null&&!string.IsNullOrEmpty(flag))
            //         ct.Application[flag] = CompletedLength;
            //}
            myStream.Close();
        }
        finally
        {
            FStream.Close();
        }
    }
    //获取目标Url的文件大小，用于计算进度,如无法获取等，返回0
    public long GetFileSize(string strUrl)
    {
        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
            return webResponse.ContentLength;
            
        }
        catch { return -1; }
    }
    //开始工作，下载，解压等
    public void BeginWork(IISWebSite site) 
    {
        if (!string.IsNullOrEmpty(StationGroup.CodeSourceUrl) && GetFileSize(StationGroup.CodeSourceUrl) != -1)//未配置下载地址,则不允许操作
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            if (!ia.CheckEnableSA(false)) 
            {
                dataInfo.InnerText = "错误800,请联系管理员!!!";
                CreateExLog("创建失败,未正确配置管理员信息,请联系管理员!!!");
                return; 
            }
            if (!iisHelper.CreateSite(site))
            {
                dataInfo.InnerText = "错误801,请联系管理员!!!"; 
                CreateExLog("站点创建失败,站点同名或" + site.Port + "端口被占用,请联系管理员!!!");
                return; 
            }
            dbMod.SiteName = site.SiteName;
            dbMod.SiteID = DataConverter.CLng(iisHelper.GetSiteModel(dbMod.SiteName).SiteID);
            dbMod.Status = 1;
            dbMod.UserID = buser.GetLogin().UserID;
            dbMod.UserName = buser.GetLogin().UserName;
            dbMod.CreateTime = DateTime.Now;
            dbMod.Remind = site.DomainName;
            #region 创建数据库
            if (StationGroup.AutoCreateDB)
            {
                if (!string.IsNullOrEmpty(StationGroup.DBManagerName) && !string.IsNullOrEmpty(StationGroup.DBManagerPasswd))//配置了数据库信息才会创建
                {
                    DBModHelper dbHelper = new DBModHelper(StationGroup.DBManagerName, StationGroup.DBManagerPasswd);
                    M_UserInfo userInfo = buser.GetLogin();
                    string dbName = "Zdb_" + site.dbName;
                    string dbUserName = dbName + "_" + userInfo.UserName;
                    string dbPasswd = function.GeneratePasswd();
                    //下载地址不可用，则终止,数据库创建失败，则仍继续,
                    try
                    {
                        if (dbHelper.DBIsExist(dbName) == 0)
                        {
                            dbHelper.CreateDatabase(dbName);
                            dbHelper.CreateDatabaseUser(dbUserName, dbPasswd);
                            dbHelper.CreateUserMap(dbName, dbUserName);
                            dataInfo.InnerText = "创建独立数据库成功:" + dbName + " 用户名:" + dbUserName + " 密码:" + dbPasswd;
                            dbMod.DBName = dbName;
                            dbMod.DBUser = dbUserName;
                            dbMod.DBInitPwd = dbPasswd;
                        }
                        else
                        {
                            dataInfo.InnerText = "错误代码901:数据库未创建，请联系管理员!!!";//"创建独立数据库失败:" + dbName + "原因:数据库已存在，请联系管理员!!!"
                            CreateExLog("创建独立数据库失败:" + dbName + "原因:数据库已存在!!!");
                        }
                    }
                    catch (Exception ex) 
                    { 
                        dataInfo.InnerText = "错误代码902:数据库未创建，请联系管理员!!!";
                        CreateExLog( "创建独立数据库失败:" + dbName + "原因:" + ex.Message);
                    }
                }
            }
            else { 
                dataInfo.InnerText = "管理员未许可自动创建数据库,请联系管理员创建对应数据库!!!";
            }
            #endregion
            DownFile dw = DownFileWork;
            Application[buser.GetLogin().UserID + "downCodeT"] = GetFileSize(StationGroup.CodeSourceUrl);
            Application[buser.GetLogin().UserID + "downTempT"] = GetFileSize(site.TempUrl);
            dw.BeginInvoke(buser.GetLogin().UserID, HttpContext.Current, StationGroup.CodeSourceUrl, @site.PhysicalPath + "SourceCode.zip", site, null, null);
            dbBll.Insert(dbMod);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "beginCheck('getCodeP');", true);
        }
        else
        {
            function.WriteErrMsg("下载地址:" + StationGroup.CodeSourceUrl + "无法访问，请联系管理员修复!!");
        }
    }
    //物理路径
    public bool UpdateConfig(IISWebSite site)
    {
        try
        {
            string ppath = site.PhysicalPath+@"Config\Site.config";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ppath);
            XmlNode tempDirNode = xmlDoc.SelectSingleNode("//SiteConfig/SiteOption/TemplateDir");//  /Template/V3
            XmlNode cssDirNode = xmlDoc.SelectSingleNode("//SiteConfig/SiteOption/CssDir");//        /Template/V3/style
            tempDirNode.InnerText = "/Template/" + site.TempDir;
            cssDirNode.InnerText = "/Template/" + site.TempDir + "/style";
            xmlDoc.Save(ppath);
            return true;
        }
        catch { return false; }
    }
    protected void beginSetupBtn_Click(object sender, EventArgs e)
    {
        string url = "http://"+CurrentSite.DomainName;
        Response.Redirect(url);
    }
    protected void beginDownTempBtn_Click(object sender, EventArgs e)
    {

    }
    protected void CreateExLog(string msg) 
    {
    }
    private IISWebSite CurrentSite 
    {
        get 
        {
            return ViewState["siteInfo"] as IISWebSite;
        }
        set { ViewState["siteInfo"] = value; }
    }
}
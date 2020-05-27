using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;

public partial class Manage_I_Template_DownTemplate : CustomerPageAction
{
    public string prodirName = "";
    public string actionArr = "getTempP";
    B_User buser = new B_User();
    IISWebSite site = new IISWebSite();
    private string serverdomain = SiteConfig.SiteOption.ProjectServer;
    public string ProName { get { return HttpUtility.UrlDecode(Request.QueryString["ProName"] ?? ""); } }
    public string ProDir { get { return Request.QueryString["dir"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //http://update.z01.com/Template/Bear/view.jpg
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            int userID = buser.GetLogin().UserID;
            Response.Clear();
            switch (action)
            {
                case "getTempP":
                    Response.Write(ZipClass.GetPercent(Convert.ToInt64(Application[userID + "downTempT"]), Convert.ToInt64(Application[userID + "downTempP"])));
                    break;
                case "setdefault":
                    string tempdir = @"/Template/" + value;
                    SiteConfig.SiteOption.TemplateDir = tempdir;
                    SiteConfig.SiteOption.CssDir = tempdir + "/style";
                    SiteConfig.Update();
                    Response.Write("");
                    break;
            }
            Response.End();
        }
        //EC.GetRemoteObj remote = new EC.GetRemoteObj();
        //remote.Urlutf8("" + serverdomain + "/api/gettemplate.aspx?menu=getprojectinfodir&proname=" + Server.UrlEncode(proname));
        if (!IsPostBack)
        {
            Panel1.Visible = true;
            LblTitle.Text = ProName + "[作者:" + GetAuthor() + "]";
            tempname.Text = ProName;
            string prodir = "/template/" + ProDir + "/";
            tempimg.Text = "<a class=\"lightbox\" href=\"" + serverdomain + prodir + "view.jpg\"><img src=\"" + serverdomain + prodir + "View.jpg\"></a>";
            DownFile dw = DownFileWork;
            string TempUrl = serverdomain + "/template/" + ProDir + ".zip";
            string TempZipFile = SiteConfig.SiteMapath() + "\\template\\" + ProDir + ".zip";
            TempUrl = TempUrl.Replace(@"\\", @"\");
            TempZipFile = TempZipFile.Replace("/", @"\").Replace(@"\\", @"\");
            //function.WriteErrMsg(TempZipFile + "||||" + TempUrl);
            Application[buser.GetLogin().UserID + "downTempT"] = GetFileSize(TempUrl);
            dw.BeginInvoke(buser.GetLogin().UserID, HttpContext.Current, TempUrl, TempZipFile, null, null, null);
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='TemplateSetOfficial.aspx'>方案设置</a></li><li class=\"active\">下载方案</li>");
        }
    }
    public delegate void UnzipFile(int userID, HttpContext ct, string sPath, string tPath);
    public delegate void DownFile(int userID, HttpContext ct, string sUrl, string tPath, IISWebSite site);
    public void DownFileWork(int userID, HttpContext ct, string TempUrl, string TempZipFile, IISWebSite site)
    {
        string localTempDir = SiteConfig.SiteMapath() + "\\template\\";
        DownloadFile(TempUrl, TempZipFile, userID + "downTempP", ct);//下载模板
        UnzipFileWork(userID, ct, TempZipFile, localTempDir);//解压
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
        if (SafeSC.FileNameCheck(strFileName)) { throw new Exception(strFileName + "取消保存"); }
        //已完成的,1%长度
        int CompletedLength = 0;
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
            byte[] btContent = new byte[1024];

            //开始写入
            int count = 0;
            while ((count = myStream.Read(btContent, 0, 1024)) > 0)//返回读了多少字节,为0表示全部读完
            {
                FStream.Write(btContent, 0, count);//知道有多少个数字节后再写入
                CompletedLength += count;
                if (ct != null && !string.IsNullOrEmpty(flag))
                    ct.Application[flag] = CompletedLength;
            }
            myStream.Close();
        }
        finally
        {
            FStream.Close();
        }
    }
    public void UnzipFileWork(int userID, HttpContext ct, string sPath, string tPath)
    {
        FileInfo f = new FileInfo(sPath);//获取文件大小，用于判断进度
        ZipClass zip = new ZipClass();
        ct.Application[userID + "uzT"] = f.Length;//需要解压的文件总字节数
        zip.UnZipFiles(sPath, tPath, userID + "uzP", ct);
    }
    public void BeginWork()
    {

    }
    public long GetFileSize(string strUrl)
    {
        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
            return webResponse.ContentLength;
        }
        catch
        {
            return -1;
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            string ppath = SiteConfig.SiteMapath() + @"Config\Site.config";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ppath);
            XmlNode tempDirNode = xmlDoc.SelectSingleNode("//SiteConfig/SiteOption/TemplateDir");//  /Template/V3
            XmlNode cssDirNode = xmlDoc.SelectSingleNode("//SiteConfig/SiteOption/CssDir");//        /Template/V3/style
            tempDirNode.InnerText = "/Template/" + prodirName;
            cssDirNode.InnerText = "/Template/" + prodirName + "/style";
            xmlDoc.Save(ppath);
            Response.Redirect("TemplateSet.aspx");
        }
        catch
        {
            function.WriteErrMsg("设置失败请返回重新设置");
        }
    }
    public String GetAuthor()
    {
        string ppath = Server.MapPath(@"/Template/" + ProDir + @"/Info.config");
        if (FileSystemObject.IsExist(ppath, FsoMethod.File))
        {
            DataSet newtempset = new DataSet();
            newtempset.ReadXml(ppath);
            return newtempset.Tables[0].Rows[0]["Author"].ToString();//Project
        }
        else
        {
            return ProDir;
        }
    }
}
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Site
{
    public partial class SiteFileManage :CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        public string siteName;
        public int index;
        public StationGroup sg = new StationGroup();
        public string PPath = "", rootPath;
        public static string progStatus = "";
        public B_User buser = new B_User();
        protected System.Threading.WaitCallback waitCallback = new WaitCallback(MyThreadWork);
        protected ServerManager iis = new ServerManager();
        protected IISHelper iisHelper = new IISHelper();
        /// <summary>
        /// 安装完成,跳转时使用
        /// </summary>
        public string url = "";
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
            //---------------指定管理员权限管理
            M_Site_SiteList siteM = new M_Site_SiteList();
            B_Site_SiteList siteBll = new B_Site_SiteList();
            if (badmin.CheckLogin())
            {
                //如果是管理员登录则不判断
            }
            else if (buser.CheckLogin())
            {
                string siteID = iis.Sites[siteName].Id.ToString();
                //非管理员用户登录,开始判断
                siteM = siteBll.SelByUserID(buser.GetLogin().UserID.ToString());//查找有无为该用户分配权限
                if (siteM == null || !siteBll.AuthCheck(siteID, buser.GetLogin().UserID.ToString())) function.WriteErrMsg("你没有管理站点的权限");
                this.Title = "独立控制台-文件浏览";
            }
            else
            {
                function.WriteErrMsg("无权访问该页面"); return;
            }
            //--------------

            if (function.isAjax())//删除
            {
                string[] siteInfo;
                //站点名，索引,目标文件
                string action = Request.Form["action"];
                if (action.Equals("beginDown"))//开始下载
                {
                    siteInfo = Request.Form["fullPath"].Split(':');
                    Int32.TryParse(siteInfo[1], out index);
                    StationGroup.RootPath = iis.Sites[siteInfo[0]].Applications[0].VirtualDirectories[index].PhysicalPath;
                    if (!Directory.Exists(StationGroup.RootPath + StationGroup.ZipSavePath))
                        Directory.CreateDirectory(StationGroup.RootPath + StationGroup.ZipSavePath);
                    if (File.Exists(StationGroup.RootPath + StationGroup.ZipSavePath + StationGroup.ZipName))
                        File.Delete(StationGroup.RootPath + StationGroup.ZipSavePath + StationGroup.ZipName);
                    //ThreadPool.QueueUserWorkItem(MyThreadWork, tempPath + StationGroup.ZipName);
                    ThreadPool.QueueUserWorkItem(MyThreadWork, sg);
                    Response.End();
                }
                else if (action.Equals("getProgress"))
                {
                    Response.Write(progStatus);
                    Response.End();
                }
                else if (action.Equals("getUnZipProg"))
                {
                    //根据长度 ，计算出百分比值后返回
                    Response.Write(ZipClass.GetPercent(ZipClass.unZipTotal, ZipClass.unZipProgress));
                    Response.End();
                }
                siteInfo = Request.Form["fullPath"].Split(':');
                int i = 0;
                if (siteInfo.Length < 3 || !Int32.TryParse(siteInfo[1], out i))
                {
                    Response.Write("信息错误，无法删除"); Response.Flush(); Response.End();
                }
                string path = GetPath(siteInfo[0], i, siteInfo[2]);//获取全路径
                if (action.Equals("del"))
                {
                    try
                    {
                        if (!DeleteDirAndFile(path))
                        {
                            Response.Write("文件不存在"); Response.Flush(); Response.End();
                        }

                    }
                    catch (Exception ex) { Response.Write(ex.Message); Response.Flush(); Response.End(); }//Response.Write("删除失败,目标文件正在使用或你无权限删除");
                }
                else if (action.Equals("rename"))
                {

                    string newPath = GetPath(siteInfo[0], i, "\\" + siteInfo[3]);
                    //Response.Write(siteInfo[2]+":"+siteInfo[3]);
                    if (!path.Equals(newPath))
                        RenameDirAndFile(path.Trim(), newPath.Trim());
                }
                Response.Clear();
                Response.Write(1);
                Response.Flush(); Response.End();

            }
            //----------------AJAX END; 

            siteName = Server.HtmlEncode(Request.QueryString["siteName"]);
            if (string.IsNullOrEmpty(siteName) || string.IsNullOrEmpty(Request.QueryString["index"]) || iis.Sites[siteName] == null)
            {
                function.WriteErrMsg("未选择要访问的站点名或该站点不存在.");
            }
            Int32.TryParse(Request.QueryString["index"], out index);
            StationGroup.RootPath = iis.Sites[siteName].Applications[0].VirtualDirectories[index].PhysicalPath;
            if (string.IsNullOrEmpty(iis.Sites[siteName].Bindings[index].Host))
            {
                url = "http://LocalHost:" + iis.Sites[siteName].Bindings[index].EndPoint.Port;
            }
            else
            {
                url = "http://" + iis.Sites[siteName].Bindings[index].Host + ":" + iis.Sites[siteName].Bindings[index].EndPoint.Port;
            }

            if (!IsPostBack)
            {
                Call.HideBread(Master);
                Repeater1.DataSource = GetDirAndFileList();
                Repeater1.DataBind();

                string command = Request["command"];//如beginSetup开始安装等,快云安装传过来的
                if (!string.IsNullOrEmpty(command))
                {
                    switch (command)
                    {
                        case "beginSetup":
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "beginDown();", true);//调用前台开始下载方法
                            break;
                        default:
                            break;
                    }
                }//Command End;

            }
        }

        protected string GetShowExtension(string extension)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("jpeg", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("jpe", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("bmp", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("png", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("swf", "<i class='fa fa-file-movie-o'></i>");
            dictionary.Add("dll", "<i class='fa fa-file'></i>");
            dictionary.Add("vbp", "<i class='fa fa-file'></i>");
            dictionary.Add("wmv", "<i class='fa fa-file-movie-o'></i>");
            dictionary.Add("avi", "<i class='fa fa-file-movie-o'></i>");
            dictionary.Add("asf", "<i class='fa fa-file-movie-o'></i>");
            dictionary.Add("mpg", "<i class='fa fa-file-movie-o'></i>");
            dictionary.Add("rm", "<i class='fa fa-file'></i>");
            dictionary.Add("ra", "<i class='fa fa-file'></i>");
            dictionary.Add("ram", "<i class='fa fa-file'></i>");
            dictionary.Add("rar", "<i class='fa fa-file-zip-o'></i>");
            dictionary.Add("zip", "<i class='fa fa-file-zip-o'></i>");
            dictionary.Add("xml", "<i class='fa  fa-file-excel-o'></i>");
            dictionary.Add("config", "<i class='fa  fa-file-excel-o'></i>");
            dictionary.Add("txt", "<i class='fa fa-file-text-o'></i>");
            dictionary.Add("exe", "<i class='fa fa-file-zip-o'></i>");
            dictionary.Add("doc", "<i class='fa fa-file-word-o'>");
            dictionary.Add("docx", "<i class='fa fa-file-word-o'>");
            dictionary.Add("html", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("aspx", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("htm", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("jpg", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("gif", "<i class='fa  fa-file-image-o'></i>");
            dictionary.Add("xls", "<i class='fa  fa-file-excel-o'></i>");
            dictionary.Add("asp", "<i class='fa fa-file-code-o'></i>");
            dictionary.Add("mp3", "<i class='fa fa-file-sound-o'></i>");
            if (dictionary.ContainsKey(extension))
            {
                return dictionary[extension];
            }
            return "<i class='fa fa-file-o'></i>";
        }

        private string GetPath(string siteName, int index, string partPath)
        {
            string path = "";
            if (iis.Sites[siteName] != null && iis.Sites[siteName].Applications[0].VirtualDirectories.Count > index)
            {
                path = iis.Sites[siteName].Applications[0].VirtualDirectories[index].PhysicalPath + partPath;
            }
            return path;
        }

        private bool RenameDirAndFile(string oldPath, string newPath)
        {
            bool flag = false;
            if (Directory.Exists(oldPath) && !Directory.Exists(newPath))//Directorys and Files are not the same name,so do't detemine the type;
            {
                Directory.Move(oldPath, newPath);
                flag = true;
            }
            else if (File.Exists(oldPath) && !File.Exists(newPath))
            {
                File.Move(oldPath, newPath);
                flag = true;
            }
            return flag;
        }
        private bool DeleteDirAndFile(string path)
        {
            bool flag = false;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                flag = true;
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                flag = true;
            }
            return flag;
        }
        private DataTable GetDirAndFileList()
        {
            DataTable dt = new DataTable();
            siteName = Server.HtmlEncode(Request.QueryString["siteName"]);
            if (string.IsNullOrEmpty(Request.QueryString["index"]) || !Int32.TryParse(Request.QueryString["index"], out index)) //如果转换失败
            {
                function.WriteErrMsg("站点不存在<a href='Default.aspx' style='margin-left:10px;'>点击返回</a>");
            }
            try
            {
                rootPath = iis.Sites[siteName].Applications[0].VirtualDirectories[index].PhysicalPath;
                string path = rootPath;
                if (!string.IsNullOrEmpty(Request.QueryString["PPath"])) { PPath = Request.QueryString["PPath"]; path += Request.QueryString["PPath"]; }
                dt = FileSystemObject.GetDirectoryInfos(@path, FsoMethod.All);
                dt.Columns.Add("PartPath", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["PartPath"] = dt.Rows[i]["FullPath"].ToString().Remove(0, path.Length);
                }
            }
            catch { Response.Write("目录不存在<a href='Default.aspx' style='margin-left:10px;'>点击返回</a>"); Response.Flush(); Response.End(); }
            return dt;
        }
        //Batch Delete
        protected void Btn1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["chk"]))
            {
                string[] allSite = Request.Form["chk"].Split(',');
                for (int i = 0; i < allSite.Length; i++)
                {
                    string[] siteInfo = allSite[i].Split(':');
                    int index = 0;
                    if (Int32.TryParse(siteInfo[1], out index))
                    {
                        DeleteDirAndFile(GetPath(siteInfo[0], index, siteInfo[2]));
                    }
                }
            }
            Repeater1.DataSource = GetDirAndFileList();
            Repeater1.DataBind();
        }
        //Search
        protected void searchText_TextChanged(object sender, EventArgs e)
        {
            if (Request.Form["searchOption"].Equals("1"))
            {
                string searchText = Request.Form["searchText"];
                string searchOption = Request.Form["searchOption"];
                DataTable dt = GetDirAndFileList();
                dt.DefaultView.RowFilter = "name like '%" + searchText + "%'";
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            }
        }
        //Compress
        protected void ZipSite_Click(object sender, EventArgs e)
        {

            ZipClass ZC = new ZipClass();
            string ppath = iis.Sites[siteName].Applications[0].VirtualDirectories[index].PhysicalPath;
            string temp = Server.MapPath(SiteConfig.SiteOption.UploadDir + "Site/");
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);
            temp += siteName + ".zip";
            if (ZC.Zip(ppath, temp))
            {
                SafeSC.DownFile(function.PToV(temp), HttpUtility.UrlEncode(siteName + ".zip", System.Text.Encoding.UTF8));
            }
        }
        //Decompress
        protected void UnZipBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(StationGroup.RootPath + StationGroup.ZipSavePath + StationGroup.ZipName))
            {
                long[] ll = FileSystemObject.GetDirInfos((StationGroup.RootPath + StationGroup.ZipSavePath));//获取文件大小，用于判断进度
                ZipClass.unZipTotal = ll[0];
                ThreadPool.QueueUserWorkItem(MyUnZipWork, sg);
                DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "$('.progress-label').text('开始解压缩');$('#progressDiv').show();beginCheck('getUnZipProg');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('源码文件" + (StationGroup.ZipSavePath + StationGroup.ZipName).Replace(@"\", @"\\") + "不存在,请先下载再安装');location=location;", true);
            }
        }
        //跳转安装(Disuse)
        protected void beginSetup_Click(object sender, EventArgs e)
        {
            //----检测是否有动态域名，如无，则提醒其先设置域名或远程localhost连接
            //先检测动态域名,无则检测IP,无则检测自定义的name


            //EnviorHelper en = new EnviorHelper();
            //url = en.GetServerIP().Split(',')[0];
            //foreach (Binding b in iis.Sites[siteName].Bindings)//动态域名
            //{
            //    if (b.Host.Length > 0 && !b.Host.ToLower().Equals("localhost"))
            //    {
            //        url = b.Host;
            //        break;
            //    }
            //}

            //if (string.IsNullOrEmpty(iis.Sites[siteName].Bindings[index].Host))//
            //{
            //    Response.Redirect("http://localhost:" + iis.Sites[siteName].Bindings[index].EndPoint.Port);
            //}
            //else
            //{
            //格式:http://域名:端口号/路径
            //Response.Redirect("http://" + iis.Sites[siteName].Bindings[index].Host + ":" + iis.Sites[siteName].Bindings[index].EndPoint.Port);
            //}
        }
        //移入ZIP类
        /// <summary>
        /// 从指定站点下载文件，并保存在指定位置,开始位置,完成度(用于ProgressBar使用)
        /// </summary>
        /// <param name="strFileName">本地物理路径</param>
        /// <param name="strUrl">目标Url</param>
        public static void DownloadFile(string strFileName, string strUrl, int begin, string flag)
        {
            if (SafeSC.FileNameCheck(strFileName)) { throw new Exception(strFileName + "取消保存"); }
            //已完成的,1%长度
            int CompletedLength = 0;
            long percent = 0; progStatus = "0"; string temp = "0";
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
                //if (CompletedLength > 0)
                //    myRequest.AddRange((int)CompletedLength);//设置Range值,即头，从指定位置开始接收文件..
                //向服务器请求，获得服务器的回应数据流
                HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
                long FileLength = webResponse.ContentLength;//文件大小
                percent = FileLength / 100;
                Stream myStream = webResponse.GetResponseStream();
                byte[] btContent = new byte[1024];
                //if (count <= 0) count += sPosstion;//

                //开始写入
                int count = 0;
                while ((count = myStream.Read(btContent, 0, 1024)) > 0)//返回读了多少字节,为0表示全部读完
                {
                    FStream.Write(btContent, 0, count);//知道有多少个数字节后再写入
                    CompletedLength += count;
                    if (!(CompletedLength / percent).ToString().Equals(temp))
                    {
                        temp = (CompletedLength / percent).ToString();
                        progStatus = temp;
                    }
                }
                myStream.Close();
            }
            finally
            {
                FStream.Close();
            }
        }
        public static void DownloadFile(string strFileName, string strUrl)
        {
            DownloadFile(strFileName, strUrl, 0, "ProgressBar");
        }

        public static void MyThreadWork(object state)//必须要有该参,必须为void;与异步不同的是其无法访问该页控件,异步能访问该页控件 
        {
            StationGroup sg = (StationGroup)state;
            DownloadFile(StationGroup.RootPath + StationGroup.ZipSavePath + StationGroup.ZipName, StationGroup.CodeSourceUrl);//解压完成后删除
        }
        public static void MyUnZipWork(object state)
        {
            StationGroup sg = (StationGroup)state;
            ZipClass zip = new ZipClass();
            //zip.UnZipFiles(rootPath + @"\SourceCode\ZoomlaCMS.zip", rootPath + @"\");
            StationGroup.SetupPath = StationGroup.SetupPath.Replace("/", "\\");//将格式符替换为物理路径格式符\ 
            zip.UnZipFiles(StationGroup.RootPath + StationGroup.ZipSavePath + StationGroup.ZipName, StationGroup.RootPath + StationGroup.SetupPath);
        }
    }
}
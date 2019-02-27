using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Content_TemplateManage : CustomerPageAction
{
    public string TemplateDir
    {
        get
        {
            string _tempdir = (SiteConfig.SiteOption.TemplateDir.TrimEnd('/') + "/").ToLower();
            string setdir = (Request.QueryString["setTemplate"] ?? "").ToLower();//指定要跳转到的目录位置,强制限于Template之下
            if (string.IsNullOrEmpty(setdir))
            {
                return _tempdir;
            }
            else
            {
                //从云端模板直接跳转
                if (setdir.IndexOf("/template/") != 0)
                {
                    return "/template/" + setdir + "/";
                }//点击链接跳转
                else { return setdir; }
            }
        }
    }
    public string CurDir { get { return SafeSC.PathDeal(Request.QueryString["dir"] ?? ""); } }
    public string UrlReferer { get { return ViewState["UrlReferer"] as string; } set { ViewState["UrlReferer"] = value; } }
    private string PPathDir { get { return Server.MapPath(SafeSC.PathDeal((TemplateDir + CurDir+"/").Replace("//","/"))); } }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.label, "TemplateManage"))
        {
            function.WriteErrMsg(Resources.L.没有权限进行此项操作);
        }
        if (!IsPostBack)
        {
            GetBread();
            //UrlReferer = "TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir) + "&Dir=" + Server.UrlEncode(CurDir);
            //if (string.IsNullOrEmpty(CurDir))
            //{
            //    lblDir.Text = "/";
            //    LitParentDirLink.Text = "<a disabled='disabled'><i class='fa fa-arrow-circle-up' style='color:#4586BD;'></i>"+Resources.L.返回上一级 + "</a>";
            //}
            //else
            //{
            //    string pdir = ""; 
            //    lblDir.Text = CurDir;
            //    if (CurDir.LastIndexOf("/") > 0)
            //    {
            //        pdir = CurDir.Remove(CurDir.LastIndexOf("/"), CurDir.Length - CurDir.LastIndexOf("/"));
            //    }
            //    LitParentDirLink.Text = "<a href=\"TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir) + "&Dir=" + pdir + "\"><i class='fa fa-arrow-circle-up' style='color:#4586BD;'></i>" + Resources.L.返回上一级 + "</a>";
            //}
            DataTable fileList = FileSystemObject.GetDirectoryInfos(PPathDir, FsoMethod.All);
            fileList.DefaultView.RowFilter = "name<>'配置库' and name<>'标签库' and name<>'节点库' and name<>'模型库' and name<>'style' and name<>'.svn' and name<>'Thumbs.db'";
            repFile.DataSource = fileList;
            repFile.DataBind();
            Page.DataBind();
            DataTable filedt = fileList.DefaultView.ToTable();
            DataRow[] dr = filedt.Select("name='全站首页.html'");
            if (dr.Length > 0)
            {
                ViewEdit_A.HRef = customPath2 + "Template/Design.aspx?vpath=" + HttpUtility.UrlEncode(function.PToV(dr[0]["FullPath"].ToString()));
            }
            else
            {
                dr = filedt.Select("name LIKE '%.html%'");
                if (dr.Length > 0)
                {
                    Random rd = new Random();
                    ViewEdit_A.HRef = customPath2 + "Template/Design.aspx?vpath=" + HttpUtility.UrlEncode(function.PToV(dr[rd.Next(dr.Length)]["FullPath"].ToString()));
                }
                else { ViewEdit_A.HRef = "#"; }
               }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+Resources.L.工作台 + "</a></li><li><a href='TemplateSet.aspx'>" + Resources.L.模板风格 + "</a></li><li class='active'><a href='TemplateManage.aspx'>模板列表</a></li><li>[<a href='TemplateEdit.aspx' style='color:red'>" + Resources.L.新建模板 + "</a>]</li>" + Call.GetHelp(20));
        }
    }
    public void GetBread()
    {
        string vdir = CurDir;
        if (string.IsNullOrEmpty(vdir)) { lblDir.Text = "全部文件"; }
        else
        {
            string url = "TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir)+"&Dir=";
            string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            lblDir.Text += "<a href='" + url + "'>全部文件</a>";
            for (int i = 0; i < dirArr.Length; i++)
            {
                //上一级目录链接
                url += dirArr[i] + "/";
                lblDir.Text += "<i class='fa fa-angle-right spanr'></i>";
                if (i == (dirArr.Length - 1)) { lblDir.Text += "<span>" + dirArr[i] + "</span>"; }
                else { lblDir.Text += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                //设置返回上一级
                if (dirArr.Length == 1) { lblDir.Text = "<a href='TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir) + "&Dir='>返回上一级</a> | " + lblDir.Text; }
                else if (i == (dirArr.Length - 2))
                {
                    lblDir.Text = "<a href='" + url + "'>返回上一级</a> | " + lblDir.Text;
                }
            }
        }
    }
    protected string GetSize(string size)
    {
        return FileSystemObject.ConvertSizeToShow(DataConverter.CLng(size));
    }
    protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string fname = Path.GetFileNameWithoutExtension(e.CommandArgument.ToString());
        string ext = Path.GetExtension(e.CommandArgument.ToString());
        if (e.CommandName == "DelFiles")
        {
            FileSystemObject.Delete(PPathDir + e.CommandArgument.ToString(), FsoMethod.File);
        }
        if (e.CommandName == "DelDir")
        {
            FileSystemObject.Delete(PPathDir + e.CommandArgument.ToString(), FsoMethod.Folder);
        }
        if (e.CommandName == "CopyFiles")
        {
            FileSystemObject.CopyFile(PPathDir + e.CommandArgument.ToString(), PPathDir + fname + "_复制" + ext);
        }
        if (e.CommandName == "CopyDir")
        {
            FileSystemObject.CopyDirectory(PPathDir + e.CommandArgument.ToString(), PPathDir + fname + "_复制" + ext);
        }
        if (e.CommandName == "DownFiles")
        {
            string filePath =function.PToV(PPathDir)+ e.CommandArgument.ToString();
            SafeSC.DownFile(filePath, HttpUtility.UrlEncode(e.CommandArgument.ToString(), System.Text.Encoding.UTF8));
        }
        Response.Redirect(Request.RawUrl);
    }

    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        //string pattern = "^[a-zA-Z0-9_]+$";
        //if (!Regex.IsMatch(txtForderName.Text.Trim(), pattern, RegexOptions.IgnoreCase))
        //{
        //   function.WriteErrMsg("<li>文件夹名称不能有特殊字符!</li><li><a href='javascript:window.history.back(-1)'>返回</a></li>");
        //}
        string dirPath = PPathDir + txtForderName.Text.Trim();
        if (Directory.Exists(dirPath)) { function.WriteErrMsg(Resources.L.当前目录下已存在同名的文件夹); }
        SafeSC.CreateDir(dirPath);
        function.WriteSuccessMsg(Resources.L.文件夹创建成功+"!");

    }
    protected void btnTemplateUpLoad_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string fileName = fileUploadTemplate.PostedFile.FileName;
        string ext = Path.GetExtension(fileName).ToLower();
        string[] TemplateAllowExtName = ".html|.htm|.txt|.config".Split('|');
        foreach (string allowext in TemplateAllowExtName){if (ext.Equals(allowext)) { flag = true; break; }}
        if (!flag) { function.WriteErrMsg(Resources.L.请上传正确的扩展名的模板文件); }
        else
        {
            string savePath = PPathDir + Path.GetFileName(fileName);
            //SafeSC.SaveFile(savePath, fileUploadTemplate);
            if (!fileUploadTemplate.SaveAs(savePath)) { function.WriteErrMsg(fileUploadTemplate.ErrorMsg); }
            Response.Redirect("TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir) + "&Dir=" + Server.UrlEncode(CurDir));
        }
    }
    public bool isimg(string name)
    {
        return SafeSC.IsImage(name);
    }
    protected void BackGrup_Click(object sender, EventArgs e)
    {
        ZipClass zip = new ZipClass();
        string dir = TemplateDir+Request["Dir"];//文件夹 
        string dir1 = TemplateDir.Substring(TemplateDir.LastIndexOf("/") + 1);
        int index = dir.LastIndexOf("/");
        string fielname = dir.Substring(index+1);
        string LjFile = Request.PhysicalApplicationPath.ToString() + dir;
        string FileToZip = LjFile.Replace("/", @"\");
        string zipdirName = DateTime.Now.ToString("yyyyMMdd") + "_" + dir1 + "_" + fielname;
        string ZipedFile =  FileToZip + @"\" + zipdirName + "模板集备份" + ".rar";
        string path1 = ZipedFile; 
        string sPath = Request.PhysicalApplicationPath.ToString() + @"temp\";
        if (Directory.Exists(sPath))//判断是否存在这个目录
        {
        }
        else
        {
            Directory.CreateDirectory(sPath);//不存在则创建这个目录
        }
        string path2 = sPath + zipdirName + "模板集备份" + ".rar";
        if (zip.Zip(FileToZip, ZipedFile, ""))
        {
            function.WriteSuccessMsg("模版方案(" + fielname + ")备份失败!", "../Template/TemplateManage.aspx");
        }
        else
        {
            File.Delete(path2);//如果不删除则会出现文件已存在,无法创建该文件的错误。
            File.Move(path1, path2);//因为生成的ZIP文件名和文件的存放位置一样，所以要在生成以后移动到temp目录下面（temp目录是用来存放备份文件的）
            function.WriteSuccessMsg("模版方案(" + fielname + ")备份成功!", "../Template/TemplateManage.aspx");
        }
    }
}
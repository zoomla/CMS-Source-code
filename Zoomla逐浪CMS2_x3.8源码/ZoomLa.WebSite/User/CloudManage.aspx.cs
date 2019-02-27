using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;
using ZoomLa.BLL;
using System.IO;
using ZoomLa.Model;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Linq;

public partial class User_Cloud_CloudManage : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Cloud cloudBll = new B_User_Cloud();
    M_UserInfo mu;
    public string  UploadType;
    public string FType { get { return string.IsNullOrEmpty(Request.QueryString["Type"]) ? "file" : Request.QueryString["Type"]; } }
    //当前地址栏指定的目录(不包含根类型与目录)
    public string VDir { get { return Request.QueryString["dir"] ?? ""; } }
    //当前实际所处目录
    public string CurrentDir
    {
        get
        {
            string _dir = (cloudBll.H_GetFolderByFType(FType, buser.GetLogin()) + VDir).Replace("//", "/");
            _dir = _dir.TrimEnd('/') + "/";
            return _dir;
        }
    }
    public int CPage { get { return PageCommon.GetCPage(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.mu = buser.GetLogin();
        if (!IsPostBack)
        {
            if (mu.IsCloud.ToString() == "1")
            {
                MyBind();
                navv.InnerHtml = CreateBread();
                Cloud.Visible = true;
            }
            else
            {
                OpenCloud.Visible = true;
            }
        }
    }
    protected void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = cloudBll.SelByPath(CurrentDir, mu.UserID);
        int icount = 0;
        PagedDataSource cc = (PagedDataSource)PageCommon.GetPageDT(15, CPage, dt, out icount);
        RptFiles.DataSource = cc;
        RptFiles.DataBind();
        MsgPage_L.Text = PageCommon.CreatePageHtml(cc.PageCount, CPage);
    }
    protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
    {
        if (e.CommandName.Equals("DelFile"))
        {
            M_User_Cloud cloudMod = cloudBll.SelReturnModel(e.CommandArgument.ToString());
            if (cloudMod.FileType == 1)
            {
                FileSystemObject.Delete(Server.MapPath(cloudMod.VPath + cloudMod.SFileName), FsoMethod.File);
            }
            else
            {
                FileSystemObject.Delete(Server.MapPath(cloudMod.VPath + cloudMod.FileName), FsoMethod.Folder);
            }
            cloudBll.DelByFile(cloudMod.Guid);
            MyBind();
        }
    }
    //创建指定目录
    protected void CreateDiv_Click(object sender, EventArgs e)
    {
        M_User_Cloud cloudMod = new M_User_Cloud();
        cloudMod.FileName = Request.Form["DirName_T"];
        cloudMod.VPath = CurrentDir;
        cloudMod.UserID = mu.UserID;
        cloudMod.FileType = 2;
        //Directory.CreateDirectory(Server.MapPath(cloudMod.VPath) + "\\" + cloudMod.FileName);
        SafeSC.CreateDir(Server.MapPath(cloudMod.VPath), cloudMod.FileName);
        cloudBll.Insert(cloudMod);
        MyBind();
    }
    //开通云盘
    protected void OpenCloud_Click(object sender, EventArgs e)
    {
        string baseDir = "/UploadFiles/Cloud/" + mu.UserName + mu.UserID + "/";
        string pathfile = baseDir + "我的文档/";
        string pathphoto = baseDir + "我的相册/";
        string pathmusic = baseDir + "我的音乐/";
        string pathvideo = baseDir + "我的视频/";
        Directory.CreateDirectory(function.VToP(pathfile));
        Directory.CreateDirectory(function.VToP(pathphoto));
        Directory.CreateDirectory(function.VToP(pathmusic));
        Directory.CreateDirectory(function.VToP(pathvideo));
        buser.UpdateIsCloud(mu.UserID, 1);
        function.WriteSuccessMsg("云盘开通成功");
    }
    //-----------------------------------
    private string CreateBread()
    {
        return GetBread(Request["dir"], "/User/CloudManage.aspx?type=" + FType + "&dir=");
    }
    /// <summary>
    /// 创建导航菜单,统一使用dir传参
    /// </summary>
    /// <param name="vdir">地址栏的dir传参</param>
    /// <param name="url">根目录地址</param>
    /// <returns></returns>
    private string GetBread(string vdir, string baseUrl)
    {
        string html = "";
        if (string.IsNullOrEmpty(vdir)) { html = "根目录"; }
        else
        {
            string url = baseUrl;
            string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            html += "<a href='" + url + "'>全部文件</a>";
            for (int i = 0; i < dirArr.Length; i++)
            {
                //上一级目录链接
                url += HttpUtility.UrlEncode(dirArr[i] + "/");
                html += "<i class='fa fa-angle-right spanr'></i>";
                if (i == (dirArr.Length - 1)) { html += "<span>" + dirArr[i] + "</span>"; }
                else { html += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                //设置返回上一级
                if (dirArr.Length == 1) { html = "<a href='" + baseUrl + "'>返回上一级</a> | " + html; }
                else if (i == (dirArr.Length - 2))
                {
                    html = "<a href='" + url + "'>返回上一级</a> | " + html;
                }
            }
        }
        return html;
    }
    protected string GetSize(string size)
    {
        if (Eval("FileType").ToString() == "2") { return ""; }
        return FileSystemObject.ConvertSizeToShow(DataConverter.CLng(size));
    }
    public string GetLink(string type, string name)
    {
        string result = "";
        if (type.Equals("2"))
        {
            result = "<a href='CloudManage.aspx?Type=" + FType + "&Dir=" + Server.UrlEncode(VDir + "/" + Eval("FileName")) + "'>" + name + "</a>";
        }
        else
        {
            result = "<a href='javascript:;' onclick='prefile(\"" + Eval("Guid") + "\")'>" + name + "</a>";
        }
        return result;
    }
    public string GetDownUrl()
    {
        string fileType = Eval("FileType").ToString();
        string result = "";
        if (fileType.Equals("1"))//普通文件
        {
            result = "<a href='/Plat/Doc/DownFile.aspx?CloudFile=" + Eval("Guid") + "' target='_down' class='btn btn-sm btn-info'><i class='fa fa-download' title='下载'></i></a>";
        }
        return result;
    }
    //文件类型图标
    public string GetUrl()
    {
        int filetype = Convert.ToInt32(Eval("FileType"));
        if (filetype == 2)
        { return GroupPic.GetExtNameMini("filefolder"); }
        if (SafeSC.IsImage(Eval("FileName").ToString()))
        {
            string imgurl = CurrentDir + "/" + Eval("SFileName");
            return "<img src='" + imgurl + "' style='width:30px;height:30px;display:block;' />";
        }
        return GroupPic.GetExtNameMini(Path.GetExtension(Eval("FileName").ToString()));
    }
}
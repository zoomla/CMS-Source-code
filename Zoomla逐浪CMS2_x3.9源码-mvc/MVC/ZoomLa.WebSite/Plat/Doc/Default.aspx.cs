using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

/*
 * 无权限限制,能被公司的任何成员所访问
 */

namespace ZoomLaCMS.Plat.Doc
{
    public partial class Default : System.Web.UI.Page
    {
        B_Plat_File fileBll = new B_Plat_File();
        B_User buser = new B_User();
        int pageSize = 8;
        public int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        public string MyVPath
        {
            get
            {
                if (Session["CompDoc_VPath"] == null)
                {
                    M_UserInfo mu = buser.GetLogin();
                    Session["CompDoc_VPath"] = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Company);
                }
                return Session["CompDoc_VPath"].ToString();
            }

        }
        //当前所在的路径(虚拟|物理)
        public string CurVPath
        {
            get { return SafeSC.PathDeal(MyVPath + Server.UrlDecode(Request.QueryString["Dir"])); }
        }
        public string CurPPath { get { return Server.MapPath(CurVPath); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string value = Request.Form["value"];
                string result = "";
                switch (action)
                {
                    case "ReName":
                        {
                            int id = Convert.ToInt32(value.Split('|')[0]);
                            string newname = value.Split('|')[1];
                            fileBll.ReName(id, newname);
                        }
                        break;
                    case "Del":
                        {
                            int id = Convert.ToInt32(value);
                            M_Plat_File fileMod = fileBll.SelReturnModel(id);
                            if (SafeSC.DelFile(fileMod.VPath + fileMod.SFileName))
                            {
                                fileBll.Del(fileMod.Guid);
                            }
                            else { result = "0"; }

                        }
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End(); return;
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            CreatePathNav();
            if (!Directory.Exists(CurPPath)) { SafeSC.CreateDir(CurVPath); }
            DataTable dt = fileBll.SelByVPath(CurVPath);
            //DataTable dt = FileSystemObject.GetDirectoryInfos(CurPPath, FsoMethod.All);
            DataTableHelper dtHelper = new DataTableHelper();
            File_Rep.DataSource = dtHelper.PageDT(dt, pageSize, CPage);
            File_Rep.DataBind();
            if (dt.Rows.Count > pageSize) { Page_Lit.Text = PageCommon.CreatePageHtml(PageCommon.GetPageCount(pageSize, dt.Rows.Count), CPage); }
            else { Page_tr.Visible = false; }
        }
        public void CreatePathNav()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Dir"]))//生成路径
            {
                string dir = Server.UrlDecode(Request.QueryString["Dir"]), tempdir = "";
                string[] dirArr = dir.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                string span = "<span><a href='Default.aspx?dir={0}'>{1}</a>></span>";
                foreach (string d in dirArr)
                {
                    tempdir += d + "/";
                    PathNav_L.Text += string.Format(span, Server.UrlEncode(tempdir), d);
                }
            }//路径处理End;
        }
        //保存文章,新建一个Html文档
        protected void ArtSave_Btn_Click(object sender, EventArgs e)
        {
            string tempPath = Server.MapPath("/Plat/Doc/DocTemplate.html");
            ArtTitle_T.Text = ArtTitle_T.Text.Trim();
            using (StreamReader sr = new StreamReader(tempPath))
            {
                string template = sr.ReadToEnd();
                template = template.Replace("{$Title}", ArtTitle_T.Text);
                template = template.Replace("{$Author}", buser.GetLogin().UserName);
                template = template.Replace("{$CreateTime}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm"));
                template = template.Replace("{$Content}", Article_T.Text);
                M_User_Plat upMod = B_User_Plat.GetLogin();
                M_Plat_File fileMod = new M_Plat_File() { CompID = upMod.CompID, UserID = upMod.UserID.ToString(), VPath = CurVPath, FileName = ArtTitle_T.Text + ".html", SFileName = function.GetRandomString(12) + ".html" };
                File.WriteAllText(CurPPath + fileMod.SFileName, template);
                fileBll.Insert(fileMod);
            }
            Response.Redirect(Request.RawUrl);
        }
        //创建新文件夹
        protected void NewFolder_Btn_Click(object sender, EventArgs e)
        {
            DirName_T.Text = DirName_T.Text.Replace(" ", "");
            if (!string.IsNullOrEmpty(DirName_T.Text))
            {
                SafeSC.CreateDir(CurPPath, DirName_T.Text);
                M_User_Plat upMod = B_User_Plat.GetLogin();
                M_Plat_File fileMod = new M_Plat_File() { FileType = 2, CompID = upMod.CompID, UserID = upMod.UserID.ToString(), VPath = CurVPath, FileName = DirName_T.Text };
                fileBll.Insert(fileMod);
                DirName_T.Text = "";
                MyBind();
            }
        }
        //返回图片
        public string GetFileName()
        {
            string result = "";
            string fName = Eval("FileName").ToString();
            string id = Eval("ID").ToString();
            if (Convert.ToInt32(Eval("FileType")) == 2)//文件夹
            {
                string dir = Server.UrlEncode(Request.QueryString["Dir"] + fName + "/");
                result = "<a href='Default.aspx?Dir=" + dir + "' title='打开目录' id='fname_now_" + id + "'>" + fName + "</a>";
            }
            else
            {
                string vpath = Server.UrlEncode(CurVPath + fName);
                result = "<a href='/PreView.aspx?File=" + Eval("Guid") + "' target='_blank' title='点击预览' id='fname_now_" + id + "'>" + fName + "</a>";
            }
            return result;
        }
        public string GetExt()
        {
            switch (Eval("FileType").ToString())
            {
                case "2":
                    return "filefolder";
                default:
                    return Eval("FileName").ToString();
            }
        }
        public string GetSize(string size)
        {
            return FileSystemObject.GetFileSize(size);
        }
        //文件夹不分享与下载
        public string GetOP()
        {
            string result = "";
            string fname = Server.UrlEncode(Eval("FileName").ToString());
            if (Eval("FileType").ToString().Equals("1"))//非文件夹
            {
                result += "<a href='DownFile.aspx?File=" + Eval("Guid") + "' target='_blank'><span class='fa fa-download' title='下载'></span></a>";
                result += "<a><span class='fa fa-cloud-upload' title='分享'></span></a>";
            }
            return result;
        }
        protected void Test_Click(object sender, EventArgs e)
        {
            function.WriteErrMsg("Test is error");
        }
    }
}
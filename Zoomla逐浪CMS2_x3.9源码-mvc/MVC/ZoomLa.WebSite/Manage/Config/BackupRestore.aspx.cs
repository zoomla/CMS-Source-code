
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Common;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Config
{
    public partial class BackupRestore : CustomerPageAction
    {
        ZipClass ZC = new ZipClass();
        string database = string.Empty;
        public string CurrentDir;
        public string DataName = string.Empty;
        public DataTable dt = new DataTable();
        public B_Admin badmin = new B_Admin();

        private string backPath = AppDomain.CurrentDomain.BaseDirectory + "temp\\";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_Admin.IsSuperManage(badmin.GetAdminLogin().AdminId))
            {
                function.WriteErrMsg("只有超级管理员才能访问该页!!");
            }
            if (function.isAjax())
            {
                //返回进度
                Response.Write(ZipClass.GetPercent(ZipClass.zipTotal, ZipClass.zipProgress));
                Response.Flush();
                Response.End();
            }
            database = StrHelper.GetAttrByStr(SqlHelper.ConnectionString, "initial catalog");
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                {
                    SPwd.Visible = true;
                }
                else
                {
                    maindiv.Visible = true;
                }
                this.DatabasePath.Value = DateTime.Now.ToString("MMdd") + "ZoomlaCMS";
                this.SiteText.Value = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "全站备份";
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='BackupRestore.aspx'>备份与还原数据</a></li>" + Call.GetHelp(69));
            }
        }
        protected void MyBind()
        {
            if (!Directory.Exists(backPath)) SafeSC.CreateDir(backPath);
            dt = FileSystemObject.GetDirectoryInfos(backPath, FsoMethod.File);
            if (dt != null && dt.Rows.Count > 0)
            {
                EGV.DataSource = dt;
                EGV.DataBind();
                Creat.Visible = false;
            }
            else
            {
                EGV.Visible = false;
            }
            if (!DBHelper.IsLocalDB(Request.ServerVariables["LOCAl_ADDR"], Request.ServerVariables["SERVER_NAME"]))
            {
                dbnolocal_sp.Visible = true;
                DatabasePath.Value = @"D:\Backup\" + DatabasePath.Value;
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetFName()
        {
            string result = "";
            string fname = Eval("Name").ToString();
            result += GroupPic.GetExtNameMini(fname);
            result += fname;
            //int type = Convert.ToInt32(Eval("Type"));//1为文件夹
            return result;
        }
        //数据库备份
        protected void Backup_Click(object sender, EventArgs e)
        {
            string dbPath = !this.chBackup.Checked ? backPath : AppDomain.CurrentDomain.BaseDirectory;
            if (dbnolocal_sp.Visible)
            {
                dbPath = "";
            }
            string sql = "backup database " + database + " to  disk='" + dbPath + this.DatabasePath.Value + ".bak" + "'  with  init ";
            if (!Directory.Exists(dbPath))
            {
                SafeSC.CreateDir(dbPath);
            }
            if (!SqlHelper.ExecuteSql(sql))
            {

                function.WriteMsgTime("数据库备份成功", "BackupRestore.aspx");
            }
        }
        // 文件大小转换
        protected string GetSize(string size)
        {
            if (string.IsNullOrEmpty(size))
            {
                return string.Empty;
            }
            int num = DataConverter.CLng(size);
            int num2 = num / 0x400;
            if (num2 < 1)
            {
                return (num.ToString() + "B");
            }
            if (num2 < 0x400)
            {
                return (num2.ToString() + "KB");
            }
            int num3 = num2 / 0x400;
            if (num3 < 1)
            {
                return (num2.ToString() + "KB");
            }
            if (num3 >= 0x400)
            {
                num3 /= 0x400;
                return (num3.ToString() + "GB");
            }
            return (num3.ToString() + "MB");
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str2 = (string)e.CommandArgument;
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + "/temp/" + str2.Replace("/", @"\"), FsoMethod.File);
                function.WriteSuccessMsg("文件成功删除", CustomerPageAction.customPath2 + "Config/BackupRestore.aspx");
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + "/temp" + str2.Replace("/", @"\"), FsoMethod.Folder);
                function.WriteSuccessMsg("目录成功删除");
            }
            if (e.CommandName == "Down")
            {
                string ste = e.CommandArgument.ToString();//文件全名
                int index = ste.LastIndexOf(".");
                string filetype = ste.Substring(index, 4);//文件扩展名
                int inde1 = ste.LastIndexOf(".");
                string ttname = ste.Substring(0, inde1);//文件名
                string filename = Server.MapPath("/temp/" + ste);
                if (filetype == ".rar")
                {
                    setRar(ttname);
                }
                else
                {
                    download(ttname);
                }
            }
            MyBind();
        }
        //获取下载链接
        public string GetPath()
        {
            return CurrentDir + "/" + Eval("Name").ToString();
        }
        //还原数据库
        protected void Restore_Click(object sender, EventArgs e)
        {
            string sPath = HttpContext.Current.Request.PhysicalApplicationPath + "/temp";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string sql = "use master restore database " + database + " from disk='" + sPath + "/" + this.DatabasePath.Value + ".bak" + "' with replace";
            try
            {
                if (!SqlHelper.ExecuteSql(sql))
                {
                    function.WriteSuccessMsg("数据库还原成功");
                }
            }

            catch (Exception)
            {
                runResult.Visible = true;
            }

        }
        public void download(string esting)
        {
            string FileToZip = Server.MapPath("/temp/" + esting + ".bak");
            string ZipedFile = Server.MapPath("/temp/" + esting + ".rar");
            //否则没有.bak文件，则先创建.bak文件。再进行压缩
            string sPath = HttpContext.Current.Request.PhysicalApplicationPath + "/temp";
            if (Directory.Exists(sPath))
            {
            }
            else
            {
                Directory.CreateDirectory(sPath);
            }
            string sql = "backup database " + database + " to  disk='" + sPath + "/" + esting + ".bak" + "'  with  init ";
            if (!SqlHelper.ExecuteSql(sql))
            {
                if (ZC.Zip(FileToZip, ZipedFile))
                {
                    SafeSC.DownFile("/temp/" + esting + ".rar");
                }
            }
        }
        public void setRar(string esting)
        {
            SafeSC.DownFile("/temp/" + esting + ".rar");
        }
        //全部备份
        protected void SiteBackup_Click(object sender, EventArgs e)
        {
            string rarName = SiteText.Value.Trim();
            if (string.IsNullOrEmpty(rarName) || rarName.Contains(".")) function.WriteErrMsg("不能为空,或包含特殊字符");
            ZipFileFunc zip = ZipFile;
            ZipClass.ContainTemp = AllSite_Chk.Checked;
            System.IAsyncResult asynResult = zip.BeginInvoke(SiteText.Value.Trim(), null, null);
            function.Script(this, "beginCheck('getProgress');");
        }

        delegate bool ZipFileFunc(string rarName);
        public bool ZipFile(string rarName)
        {
            bool flag = false;
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            string zipPath = dirPath + @"temp\";
            ZipClass ZC = new ZipClass();
            ZC.IgnoreFile = zipPath + rarName + ".rar";
            int dirsCount = 0, filesCount = 0;
            FileSystemObject.GetTotalDF(dirPath, ref dirsCount, ref filesCount);
            ZipClass.zipTotal = dirsCount;//只用目录数就可以了
            ZipClass.zipProgress = 0;
            DirectoryInfo dr = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(zipPath))
                Directory.CreateDirectory(zipPath);
            if (ZC.Zip(dirPath, zipPath + rarName + ".rar"))
            {
                flag = true;
            }
            return flag;
        }
    }
}
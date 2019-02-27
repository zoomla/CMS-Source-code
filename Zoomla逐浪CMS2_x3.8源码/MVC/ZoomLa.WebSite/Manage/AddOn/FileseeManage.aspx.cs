using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.IO;
using System.Data;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class FileseeManage : System.Web.UI.Page
    {
        private int pathdepth = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='" + CustomerPageAction.customPath2 + "iplook/IPManage.aspx'>其他功能</a></li><li><a href='FileseeManage.aspx'>比较所有文件</a></li>");
            }
        }
        protected void bj_Click(object sender, EventArgs e)
        {
            //模板
            this.seestr.InnerHtml = "";
            string outstr = @"<tr class=""tdbg"">
                <td style=""text-align: center; width: 5%;"">
                   {xuhao}
                </td>
                <td style=""text-align: left; width: 55%;"">
                    {wjm}
                </td>
                <td style=""text-align: center; width: 20%;"">
                    {filesize}
                </td>
                <td style=""text-align: center; width: 20%;"">
                    {isbool}
                </td>
            </tr>";

            #region 一级目录
            DataTable alldir = OpenallDir("/");
            int pp = 0;
            pp = callread(outstr, alldir, pp);
            #endregion

            #region 根目录
            DataTable dirtabless = Opendir("./");
            for (int s = 0; s < dirtabless.Rows.Count; s++)
            {
                pp = pp + 1;
                double truesize = (double)DataConverter.CDouble(dirtabless.Rows[s]["size"].ToString()) / ((double)1024);
                string showsize = truesize.ToString("##.##");
                this.seestr.InnerHtml += outstr.Replace("{xuhao}", (pp + 1).ToString()).Replace("{wjm}", dirtabless.Rows[s]["name"].ToString()).Replace("{filesize}", showsize).Replace("{isbool}", "<font color=green><b>安全</b></font>");
            }
            #endregion
        }
        private int callread(string outstr, DataTable alldir, int pp)
        {
            pathdepth = pathdepth + 1;
            for (int p = 0; p < alldir.Rows.Count; p++)
            {
                pp = pp + 1;
                string dirname = alldir.Rows[p]["name"].ToString();
                string truedirname = @"\" + dirname;
                DataTable dirdata = Opendir(truedirname);
                this.seestr.InnerHtml += outstr.Replace("{xuhao}", pp.ToString()).Replace("{wjm}", truedirname).Replace("{filesize}", "").Replace("{isbool}", "");//<font color=green><b>{safe}</b></font>
                pp = Readpath(outstr, pp, truedirname);
            }
            return pp;
        }
        private int Readpath(string outstr, int pp, string truedirname)
        {
            DataTable dirtables = Opendir(truedirname);
            for (int s = 0; s < dirtables.Rows.Count; s++)
            {
                pathdepth = pathdepth + 2;
                pp = pp + 1;
                double truesize = (double)DataConverter.CDouble(dirtables.Rows[s]["size"].ToString()) / ((double)1024);
                string showsize = truesize.ToString("##.##");
                if (truesize < 1) { showsize = "0" + showsize; }
                showsize += " Kb";
                this.seestr.InnerHtml += outstr.Replace("{xuhao}", pp.ToString()).Replace("{wjm}", "　" + truedirname + dirtables.Rows[s]["name"].ToString()).Replace("{filesize}", showsize.ToString()).Replace("{isbool}", "<font color=green><b>安全</b></font>");
            }
            return pp;
        }
        //pathdepth = pathdepth + 1;
        private DataTable Opendir(string path)
        {
            DataTable table = new DataTable();
            DirectoryInfo info = new DirectoryInfo(Server.MapPath(path));
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("content_type");
            table.Columns.Add("createTime", typeof(DateTime));
            table.Columns.Add("lastWriteTime", typeof(DateTime));
            FileInfo[] files = info.GetFiles();

            foreach (FileInfo info2 in files)
            {
                DataRow row = table.NewRow();
                string dirname = info2.FullName.Remove(0, info.FullName.Length);
                row[0] = dirname;// dirname;
                row[1] = 2;
                row[2] = info2.Length;
                row[3] = info2.Extension.Replace(".", "");
                row[4] = info2.CreationTime;
                row[5] = info2.LastWriteTime;
                table.Rows.Add(row);
            }
            return table;
        }
        private DataTable OpenallDir(string dirpath)
        {
            if (dirpath == "") { dirpath = "/"; }
            DirectoryInfo info = new DirectoryInfo(Server.MapPath(dirpath));
            DataTable dirtable = new DataTable();
            dirtable.Columns.Add("name");
            dirtable.Columns.Add("createTime", typeof(DateTime));
            dirtable.Columns.Add("lastWriteTime", typeof(DateTime));
            DirectoryInfo[] dirarr = info.GetDirectories();
            foreach (DirectoryInfo info1 in dirarr)
            {
                DataRow rrow = dirtable.NewRow();
                string dirname = info1.FullName.Remove(0, info.FullName.Length);
                rrow[0] = dirname; // ;
                rrow[1] = info1.CreationTime;
                rrow[2] = info1.LastWriteTime;
                dirtable.Rows.Add(rrow);
            }
            return dirtable;
        }
    }
}
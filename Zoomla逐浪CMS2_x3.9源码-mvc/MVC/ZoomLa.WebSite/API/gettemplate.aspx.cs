using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Data;
using System.Text;
public partial class API_gettemplate : System.Web.UI.Page
{
    //云端模板API,仅update.z01.com使用
    protected void Page_Load(object sender, EventArgs e)
    {
        return;
        //if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "getlist")
        //{
        //    if (Request.QueryString["proname"] != null)
        //    {
        //        string proname = Request.QueryString["proname"].ToString();
        //        string tempurl = "/config/TemplateProject.config";
        //        string truepathurl = Server.MapPath(tempurl);
        //        if (FileSystemObject.IsExist(truepathurl, FsoMethod.File))
        //        {
        //            DataSet newset = new DataSet();
        //            newset.ReadXml(truepathurl);
        //            DataTable temptable = newset.Tables[0];
        //            DataRow[] rowslit = temptable.Select("Project='" + proname + "'");
        //            if (rowslit != null && rowslit.Length > 0)
        //            {
        //                //遍历文件夹
        //                string tempdir = "/template/" + rowslit[0]["TempDirName"];
        //                DataTable filelist = FileSystemObject.GetDirectoryAllInfos(Server.MapPath(tempdir), FsoMethod.Folder);

        //                DataTable writetable = new DataTable();
        //                writetable.Columns.Add("filedir", typeof(string));
        //                writetable.Columns.Add("fileurl", typeof(string));
        //                StringBuilder binfo = new StringBuilder();
        //                for (int i = 0; i < filelist.Rows.Count; i++)
        //                {
        //                    if (filelist.Rows[i]["rname"].ToString().IndexOf(".svn") == -1)
        //                    {
        //                        string fullpath = filelist.Rows[i]["rname"].ToString();
        //                        string[] patharr = fullpath.Split(new string[] { @"\template\" }, StringSplitOptions.None);
        //                        string fileurl = @"\template\" + patharr[1];
        //                        fileurl = fileurl.Replace(@"\", "/");
        //                        binfo.AppendLine(fileurl);
        //                    }
        //                }
        //                binfo.AppendLine("=======");//Environment.NewLine
        //                filelist = FileSystemObject.GetDirectoryAllInfos(Server.MapPath(tempdir), FsoMethod.File);
        //                for (int i = 0; i < filelist.Rows.Count; i++)
        //                {
        //                    if (filelist.Rows[i]["rname"].ToString().IndexOf(".svn") == -1)
        //                    {
        //                        string fullpath = filelist.Rows[i]["rname"].ToString();
        //                        string[] patharr = fullpath.Split(new string[] { @"\template\" }, StringSplitOptions.None);
        //                        string fileurl = @"\template\" + patharr[1];
        //                        fileurl = fileurl.Replace(@"\", "/");
        //                        binfo.AppendLine(fileurl);
        //                    }
        //                }
        //                Response.Write(binfo.ToString());
        //            }
        //        }
        //    }
        //}

        //if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "getfilecontent")
        //{
        //    if (Request.QueryString["proname"] != null)
        //    {
        //        string fileurl = Request.QueryString["proname"];
        //        string filecontent = SafeSC.ReadFileStr(fileurl);
        //        Response.Write(filecontent);
        //    }
        //}
        //if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "getprojectinfo")
        //{
        //    string filecontent = SafeSC.ReadFileStr("/Config/TemplateProject.config");
        //    Response.Write(filecontent);
        //}

        //if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "gethelp")
        //{
        //    string filecontent = SafeSC.ReadFileStr("/help/list.xml");
        //    Response.Write(filecontent);
        //}

        //if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "getprojectinfodir")
        //{
        //    if (Request.QueryString["proname"] != null)
        //    {
        //        string proname = Request.QueryString["proname"];//方案名称
        //        string tempurl = "/config/TemplateProject.config";
        //        string truepathurl = Server.MapPath(tempurl);
        //        if (FileSystemObject.IsExist(truepathurl, FsoMethod.File))
        //        {
        //            DataSet newset = new DataSet();
        //            newset.ReadXml(truepathurl);
        //            DataTable temptable = newset.Tables[0];
        //            DataRow[] rowslit = temptable.Select("Project='" + proname + "'");
        //            if (rowslit != null && rowslit.Length > 0)
        //            {
        //                string TempDirName = rowslit[0]["TempDirName"].ToString();
        //                Response.Write(TempDirName);
        //            }

        //        }
        //    }
        //}
    }
}
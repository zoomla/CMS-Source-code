using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Text;

public partial class Manage_I_Content_AddTemplateSet : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>工作台</li><li>系统设置</li><li>方案管理</li><li class=\"active\">发布当前站点方案</li>");
        if (!IsPostBack)
        {
            string tempdir = SiteConfig.SiteOption.TemplateDir;
            string[] temparr = tempdir.Split(new string[] { @"/" }, StringSplitOptions.None);
            string template = SiteConfig.SiteMapath() + temparr[1];
            DataTable newtable = FileSystemObject.GetDirectorySmall(template);
            if(newtable!=null && newtable.Rows.Count>0)
            {
                for(int i=0;i<newtable.Rows.Count;i++)
                {
                    this.tempdirlist.Items.Add(new ListItem(newtable.Rows[i]["name"].ToString(), newtable.Rows[i]["name"].ToString()));
                }
            }
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        string pronametxt = this.proname.Text;
        string protypetxt = this.protype.Text;
        int ornumtxt = DataConverter.CLng(this.ornum.Text);
        string tempname = tempdirlist.SelectedValue;
        string tempimgtxt = "";
        string author = txtAuthor.Text.Trim()==""?"佚名":txtAuthor.Text.Trim();
        string tempdir = SiteConfig.SiteOption.TemplateDir;
        string[] temparr = tempdir.Split(new string[] { @"/" }, StringSplitOptions.None);
        string template = SiteConfig.SiteMapath() + temparr[1];

        string infofile = template + "\\" + tempname + @"\Info.config";
        if (!FileSystemObject.IsExist(infofile, FsoMethod.File))
        {
            FileSystemObject.Create(infofile, FsoMethod.File);
            DataSet projectSet = new DataSet("NewDataSet");
            DataTable projecttable = new DataTable();
            projecttable.TableName = "Table";
            projecttable.Columns.Add("Project", typeof(string));
            projecttable.Columns.Add("TempDirName", typeof(string));
            projecttable.Columns.Add("Author", typeof(string));
            projecttable.Columns.Add("ProType", typeof(string));
            projecttable.Columns.Add("OrderNum", typeof(string));
            projecttable.Columns.Add("ProjectImg", typeof(string));
            projecttable.Columns.Add("isinstall", typeof(string));
            DataRow rows = projecttable.NewRow();
            rows["Project"] = pronametxt;
            rows["ProType"] = protypetxt;
            rows["OrderNum"] = ornumtxt;
            rows["Author"] = author;
            rows["TempDirName"] = tempname;
            rows["ProjectImg"] = tempimgtxt;
            rows["isinstall"] = "true";
            projecttable.Rows.Add(rows);
            projectSet.Tables.Add(projecttable);
            projectSet.WriteXml(infofile);
        }
        else
        {
            DataSet projectSet = new DataSet("NewDataSet");
            DataTable projecttable = new DataTable();
            projecttable.TableName = "Table";
            projecttable.Columns.Add("Project", typeof(string));
            projecttable.Columns.Add("TempDirName", typeof(string));
            projecttable.Columns.Add("Author", typeof(string));
            projecttable.Columns.Add("ProType", typeof(string));
            projecttable.Columns.Add("OrderNum", typeof(string));
            projecttable.Columns.Add("ProjectImg", typeof(string));
            projecttable.Columns.Add("isinstall", typeof(string));
            DataRow rows = projecttable.NewRow();
            rows["Project"] = pronametxt;
            rows["ProType"] = protypetxt;
            rows["Author"] = author;
            rows["OrderNum"] = ornumtxt;
            rows["TempDirName"] = tempname;
            rows["ProjectImg"] = tempimgtxt;
            rows["isinstall"] = "true";
            projecttable.Rows.Add(rows);
            projectSet.Tables.Add(projecttable);
            projectSet.WriteXml(infofile);
        }
        if (!FileSystemObject.IsExist(SiteConfig.SiteMapath() + "config/TemplateProject.config", FsoMethod.File))
        {
            DataSet projectSet = new DataSet("NewDataSet");
            DataTable projecttable = new DataTable();
            projecttable.TableName = "Table";
            projecttable.Columns.Add("Project", typeof(string));
            projecttable.Columns.Add("TempDirName", typeof(string));
            projecttable.Columns.Add("Author", typeof(string));
            projecttable.Columns.Add("ProType", typeof(string));
            projecttable.Columns.Add("OrderNum", typeof(string));
            projecttable.Columns.Add("ProjectImg", typeof(string));

            DataRow rows = projecttable.NewRow();
            rows["Project"] = pronametxt;
            rows["ProType"] = protypetxt;
            rows["Author"] = author;
            rows["OrderNum"] = ornumtxt;
            rows["TempDirName"] = tempname;
            rows["ProjectImg"] = tempimgtxt;
            projecttable.Rows.Add(rows);
            projectSet.Tables.Add(projecttable);
            projectSet.WriteXml(SiteConfig.SiteMapath() + "config/TemplateProject.config");
        }
        else
        {
            DataSet projectSet = new DataSet("NewDataSet");
            projectSet.ReadXml(SiteConfig.SiteMapath() + "config/TemplateProject.config");
            if (projectSet ==null)
            {
                projectSet = new DataSet("NewDataSet");
            }
            if (projectSet.Tables.Count > 0)
            {
                DataTable tables = projectSet.Tables[0].Copy();
                for (int i = 0; i < tables.Rows.Count; i++)
                {
                    if (tables.Rows[i]["Project"].ToString().Trim() == pronametxt.Trim() )
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('名称已存在,请重新填写！');document.getElementById('" + proname.ClientID + "').focus();", true);
                        return;
                    }
                    else if(tables.Rows[i]["TempDirName"].ToString().Trim() == tempname)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('选中模板已发布,请重新选择！');document.getElementById('" + tempdirlist.ClientID + "').focus();", true);
                        return;
                    }
                }
                tables.TableName = "Table";
                DataRow rows = tables.NewRow();
                rows["Project"] = pronametxt;
                rows["TempDirName"] = tempname;
                rows["Author"] = author;
                rows["ProType"] = protypetxt;
                rows["OrderNum"] = ornumtxt;
                rows["ProjectImg"] = tempimgtxt;
                tables.Rows.Add(rows);
                DataSet projectSetcs = new DataSet("NewDataSet");
                projectSetcs.Tables.Add(tables);
                projectSetcs.WriteXml(SiteConfig.SiteMapath() + "config/TemplateProject.config");
            }
            else
            {
                DataTable tables = new DataTable();
                tables.TableName = "Table";
                tables.Columns.Add(new DataColumn("Project", typeof(string)));
                tables.Columns.Add(new DataColumn("TempDirName", typeof(string)));
                tables.Columns.Add(new DataColumn("Author", typeof(string)));
                tables.Columns.Add(new DataColumn("ProType", typeof(string)));
                tables.Columns.Add(new DataColumn("OrderNum", typeof(string)));
                tables.Columns.Add(new DataColumn("ProjectImg", typeof(string)));
                DataRow rows = tables.NewRow();
                rows["Project"] = pronametxt;
                rows["ProType"] = protypetxt;
                rows["OrderNum"] = ornumtxt;
                rows["Author"] = author;
                rows["TempDirName"] = tempname;
                rows["ProjectImg"] = tempimgtxt;
                tables.Rows.Add(rows);
                DataSet projectSetcs = new DataSet("NewDataSet");
                projectSetcs.Tables.Add(tables);
                projectSetcs.WriteXml(SiteConfig.SiteMapath() + "config/TemplateProject.config");
            }
        }
        function.WriteSuccessMsg("操作成功!", "TemplateSet.aspx");
    }
}
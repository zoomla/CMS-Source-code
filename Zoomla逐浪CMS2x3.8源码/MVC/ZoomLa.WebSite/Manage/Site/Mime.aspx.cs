using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Site
{
    public partial class Mime : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        IISHelper iisHelper = new IISHelper();
        string siteName = "";
        public readonly int systemInt = 370;
        protected void Page_Load(object sender, EventArgs e)
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            ia.CheckEnableSA();
            if (string.IsNullOrEmpty(Request.QueryString["siteName"]))
            {
                function.WriteErrMsg("站点名不能为空");
            }
            siteName = Request.QueryString["siteName"];
            if (!IsPostBack)
            {
                DataBind(displayAll.Checked);
            }
            mimeEGV.txtFunc = txtPageFunc;
        }

        public DataTable ConvertToDataTable(ConfigurationElementCollection ces)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Ext", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            int i = 1;
            foreach (ConfigurationElement ce in ces)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i++;
                dr["Ext"] = ce.Attributes["fileExtension"].Value;
                dr["Type"] = ce.Attributes["mimeType"].Value;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void displayAll_CheckedChanged(object sender, EventArgs e)
        {
            DataBind(displayAll.Checked);
        }
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            string ext = extText.Text.Trim();
            string type = typeText.Text.Trim();
            if (string.IsNullOrEmpty(ext) || string.IsNullOrEmpty(type))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('不能为空!');", true);
            }
            else
            {
                iisHelper.AddMimeType(siteName, ext, type);
                extText.Text = "";
                typeText.Text = "";
                DataBind(displayAll.Checked);
            }
        }
        protected void mimeEGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del2":
                    string ext = e.CommandArgument.ToString().Trim();
                    iisHelper.RemoveMimeElement(siteName, ext);
                    DataBind(displayAll.Checked);
                    break;
                default:
                    break;
            }
        }
        //---------------GridView
        public void DataBind(bool flag, string key = "")//是否显示系统映射
        {
            DataTable dt = new DataTable();
            ConfigurationElementCollection ces = iisHelper.GetMimeTypeBySiteName(siteName);
            dt = ConvertToDataTable(ces);
            if (!flag)
            {
                dt.DefaultView.RowFilter = "ID > " + systemInt;
                dt = dt.DefaultView.ToTable();
            }
            mimeEGV.DataSource = dt;
            mimeEGV.DataBind();
        }
        //处理页码
        public void txtPageFunc(string size)
        {

            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = mimeEGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = mimeEGV.PageSize;
            }
            mimeEGV.PageSize = pageSize;
            mimeEGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind(displayAll.Checked);
        }
        protected void mimeEGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mimeEGV.PageIndex = e.NewPageIndex;
            DataBind(displayAll.Checked);
        }
    }
}
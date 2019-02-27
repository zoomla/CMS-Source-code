using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Site
{
    public partial class ServerClusterConfig : CustomerPageAction
    {
        public string tableName = "ZL_IDC_Server";
        public string user, passwd, ip, cp, sn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.HideBread(Master);
        }
        private void DataBind(string key = "")
        {
            EGV.DataSource = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + tableName);
            EGV.DataBind();
        }
        protected void addBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(siteUrl.Text.Trim()))
            {
                user = siteUser.Text.Trim();
                passwd = EncryptHelper.AESEncrypt(sitePasswd.Text.Trim());
                ip = siteUrl.Text.Trim().TrimEnd('/');
                cp = NewcustomPath.Text.Trim();
                sn = siteName.Text.Trim();
                SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("SiteUser",user),
                new SqlParameter("SitePasswd",passwd),
                 new SqlParameter("SiteUrl",ip),
                   new SqlParameter("SiteName",sn),
                  new SqlParameter("CustomPath",cp)
            };
                SqlHelper.ExecuteScalar(CommandType.Text, "Insert Into " + tableName + " (siteUser,SitePasswd,SiteUrl,SiteName,customPath) values(@SiteUser,@SitePasswd,@SiteUrl,@SiteName,@CustomPath)", sp);
                DataBind();
            }
            this.table_ul.Style.Add("display", "none");
            this.add_table.Style.Add("display", "block");
        }
        protected void GetCustomPath_Click(object sender, EventArgs e)
        {
            Object obj = new object();
            try
            {
                obj = ServicesHelper.InvokeWebSer(siteUrl.Text.Trim() + ServicesHelper.siteGroupService,
               "SiteGroup",  // 欲调用的WebService的命名空间(如你已经引用,则为引用进入的命名空间)
                "SiteGroupFunc",   // 欲调用的WebService的类名（不包括命名空间前缀）
                 "GetCustomPath",    // 欲调用的WebService的方法名
                  new object[] { });
                NewcustomPath.Text = obj.ToString();
                obj = ServicesHelper.InvokeWebSer(siteUrl.Text.Trim() + ServicesHelper.siteGroupService,
              "SiteGroup",  // 欲调用的WebService的命名空间(如你已经引用,则为引用进入的命名空间)
               "SiteGroupFunc",   // 欲调用的WebService的类名（不包括命名空间前缀）
                "GetSiteName",    // 欲调用的WebService的方法名
                 new object[] { });
                siteName.Text = obj.ToString();
                this.table_ul.Style.Add("display", "block");
                this.add_table.Style.Add("display", "none");
                function.Script(this, "ShowDiv();");
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('获取失败，请检查信息是否填写正确!');", true);
                this.table_ul.Style.Add("display", "none");
                this.add_table.Style.Add("display", "block");
                function.Script(this, "ShowDiv();");
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit2":
                    EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                    EGV.Columns[4].Visible = true;
                    break;
                case "Save":
                    string[] s = e.CommandArgument.ToString().Split(':');
                    Update(DataConvert.CLng(s[0]), s[1]);
                    EGV.EditIndex = -1;
                    EGV.Columns[4].Visible = false;
                    break;
                case "Del2":
                    int id = DataConvert.CLng(e.CommandArgument as string);
                    SqlHelper.ExecuteScalar(CommandType.Text, "Delete From " + tableName + " Where ID = " + id);
                    EGV.Columns[4].Visible = false;
                    break;
                default: break;
            }
            DataBind();
        }
        private void Update(int rowNum, string id)
        {
            GridViewRow gr = EGV.Rows[rowNum];
            ip = ((TextBox)gr.FindControl("eSiteUrl")).Text.Trim();
            user = ((TextBox)gr.FindControl("eSiteUser")).Text.Trim();
            passwd = ((TextBox)gr.FindControl("eSitePasswd")).Text.Trim();
            passwd = EncryptHelper.AESEncrypt(passwd);
            cp = ((TextBox)gr.FindControl("eCustomPath")).Text.Trim();
            sn = ((TextBox)gr.FindControl("eSiteName")).Text.Trim();
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("ID",id),
                new SqlParameter("SiteUser",user),
                new SqlParameter("SitePasswd",passwd),
                 new SqlParameter("SiteUrl",ip),
                   new SqlParameter("SiteName",sn),
                  new SqlParameter("CustomPath",cp)
            };
            SqlHelper.ExecuteScalar(CommandType.Text, "Update " + tableName + " set siteUser=@SiteUser,SitePasswd=@SitePasswd,SiteUrl=@SiteUrl,SiteName=@SiteName,customPath=@CustomPath Where ID=@ID", sp);
        }
        protected void EGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            EGV.Columns[4].Visible = false;
            DataBind();
        }
        protected void backBtn_Click(object sender, EventArgs e)
        {
            this.table_ul.Style.Add("display", "none");
            this.add_table.Style.Add("display", "block");
            function.Script(this, "ShowDiv();");
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}
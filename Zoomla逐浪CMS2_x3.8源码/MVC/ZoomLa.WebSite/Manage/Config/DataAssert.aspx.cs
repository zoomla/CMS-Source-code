using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Collections.Generic;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Config
{
    public partial class DataAssert : CustomerPageAction
    {
        private object dbConnectionString;
        private B_Label bll = new B_Label();
        private B_Admin badmin = new B_Admin();
        private string txt_DatabaseList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                    maindiv.Visible = true;
                if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                    function.WriteErrMsg("错误的Url或非法的请求！", "");

                string dataname = DBHelper.GetAttrByStr(SqlHelper.ConnectionString, "Initial Catalog");
                string dataname2 = DBHelper.GetAttrByStr(SqlHelper.PlugConnectionString, "Initial Catalog");

                this.DatabaseList.Items.Add(new ListItem("主数据库", dataname));
                this.DatabaseList.Items.Add(new ListItem("从数据库", dataname2));
                changedbtabledownlist();
            }
            txt_DatabaseList = DatabaseList.SelectedValue;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='DataAssert.aspx'>表内容批处理</a></li>" + Call.GetHelp(66));
        }

        protected void TableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbFieldDownList.Items.Clear();
            string dataname = DBHelper.GetAttrByStr(SqlHelper.ConnectionString, "Initial Catalog");
            string dataname2 = DBHelper.GetAttrByStr(SqlHelper.PlugConnectionString, "Initial Catalog");

            Hashtable tablelist = new Hashtable();
            tablelist.Add(dataname, SqlHelper.ConnectionString);
            tablelist.Add(dataname2, SqlHelper.PlugConnectionString);

            string databaselistvalue = DatabaseList.SelectedValue;

            string Conection1 = tablelist[databaselistvalue].ToString();

            DataTable listtables1 = this.bll.GetTableName(Conection1);

            this.DbTableDownList.DataSource = listtables1;
            this.DbTableDownList.DataTextField = "TABLENAME";
            this.DbTableDownList.DataValueField = "TABLE_NAME";
            this.DbTableDownList.DataBind();

            ListItem item = new ListItem();
            item.Text = "选择一个数据表";
            this.DbTableDownList.Items.Insert(0, item);

            dbConnectionString = Conection1;
        }

        private void changedbtabledownlist()
        {
            ListItem item = new ListItem();
            item.Text = "选择一个数据表";

            if (DatabaseList.SelectedIndex == 0)
            {
                DataTable listtable = this.bll.GetTableName(SqlHelper.ConnectionString);
                listtable.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList.DataSource = listtable;
                this.DbTableDownList.DataTextField = "TABLENAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                this.DbTableDownList.Items.Insert(0, item);
            }
            else if (DatabaseList.SelectedIndex == 1)
            {
                DataTable listtable = this.bll.GetTableName(SqlHelper.PlugConnectionString);
                listtable.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList.DataSource = listtable;
                this.DbTableDownList.DataTextField = "TABLENAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                this.DbTableDownList.Items.Insert(0, item);
            }
        }

        protected void DBTableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DbTableDownList.SelectedIndex == 0)
            {
                DbFieldDownList.Items.Clear();
            }
            string dataname = DBHelper.GetAttrByStr(SqlHelper.ConnectionString, "Initial Catalog");
            string dataname2 = DBHelper.GetAttrByStr(SqlHelper.PlugConnectionString, "Initial Catalog");

            Hashtable tablelist = new Hashtable();
            tablelist.Add(dataname, SqlHelper.ConnectionString);
            tablelist.Add(dataname2, SqlHelper.PlugConnectionString);

            string databaselistvalue = txt_DatabaseList;

            string Conection = tablelist[databaselistvalue].ToString();

            this.dbConnectionString = Conection;
            if (this.DbTableDownList.SelectedIndex != 0)
            {
                DataTable tabledlist = this.bll.GetTableField(this.DbTableDownList.SelectedValue, this.dbConnectionString);
                tabledlist.DefaultView.Sort = "fieldname asc";
                this.DbFieldDownList.DataSource = tabledlist;
                this.DbFieldDownList.DataTextField = "fieldname";
                this.DbFieldDownList.DataValueField = "fieldname";
                this.DbFieldDownList.DataBind();
                this.DbFieldDownList.Items.Insert(0, new ListItem("*", "*"));
                for (int i = 0; i < this.DbFieldDownList.Items.Count; i++)
                {
                    DbFieldDownList.Items[i].Attributes["title"] = bll.GetTablecolumn(this.DbTableDownList.SelectedValue, this.DbFieldDownList.Items[i].Text);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string TargetField = TextBox1.Text.Trim();
            string ShiftField = TextBox2.Text.Trim();
            string DbTable = DbTableDownList.SelectedItem.Text;
            if (DbTable == "选择一个数据表")
            {
                function.WriteErrMsg("选择一个数据表");
                return;
            }
            ArrayList DbFieldlist = new ArrayList();
            for (int i = 0; i < DbFieldDownList.Items.Count; i++)
            {
                if (DbFieldDownList.Items[i].Selected)
                {
                    DbFieldlist.Add(DbFieldDownList.Items[i].Value);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("DataAssert .aspx");
        }
    }
}
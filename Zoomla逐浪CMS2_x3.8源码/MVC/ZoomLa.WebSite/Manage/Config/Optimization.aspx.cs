using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Config
{
    public partial class Optimization : CustomerPageAction
    {
        private B_Label b_Label = new B_Label();
        string strHtml = string.Empty;
        B_Admin badmin = new B_Admin();
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
                ListItem item = new ListItem();
                item.Text = "选择一个数据表";
                item.Value = "";
                DataTable listtable = this.b_Label.GetTableName(SqlHelper.ConnectionString);
                listtable.DefaultView.Sort = "TABLE_NAME asc";
                this.DbTableDownList.DataSource = listtable;
                this.DbTableDownList.DataTextField = "TABLENAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                this.DbTableDownList.Items.Insert(0, item);
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='Optimization.aspx'>数据库优化</a></li>" + Call.GetHelp(70));
            }
        }

        protected void CreateIndex_Click(object sender, EventArgs e)
        {
            string tableName = DbTableDownList.SelectedItem.ToString();
            string field = "";
            string isUnique = "";    //是否唯一索引
            string indextype = "";
            string indexName = this.IndexName.Value;
            if (tableName == "选择一个数据表")
                function.WriteErrMsg("请先选择一个数据表然后再进行查看索引");
            if (string.IsNullOrEmpty(indexName))
                function.WriteErrMsg("请先输入索引名称然后再进行创建索引");
            if (string.IsNullOrEmpty(this.DbFieldDownList.SelectedItem.ToString()))
                function.WriteErrMsg("请选择索引列");
            field = this.DbFieldDownList.SelectedItem.ToString();
            if (this.IsUnique.Checked)
                isUnique = "unique";
            if (!string.IsNullOrEmpty(this.IndexType.SelectedValue.ToString()))
                indextype = this.IndexType.SelectedValue.ToString();
            string sqlstr = "CREATE  " + isUnique + "  " + indextype + "  INDEX  " + indexName + "  ON  " + tableName + " (" + field + ")";
            if (!SqlHelper.ExecuteSql(sqlstr))
                function.WriteSuccessMsg("创建成功！");
            else
                function.WriteErrMsg("创建失败");
            //this.IndexName.Value = "";
        }

        protected void ViewIndex_Click(object sender, EventArgs e)
        {
            string tableName = DbTableDownList.SelectedItem.ToString();
            string sqlstr = "exec sp_helpindex " + tableName;
            this.Top.Visible = true;
            if (tableName == "选择一个数据表")
                function.WriteErrMsg("请先选择一个数据表然后再进行查看索引");
            this.Label1.Text = tableName + "  表现有的索引";

            strHtml += "<table border='0' cellpadding='1' class='border' align='center'>";
            DataTable dt = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, sqlstr);
            if (dt != null && dt.Rows.Count > 0)
            {
                strHtml += "<thead class='gridtitle'>";
                foreach (DataColumn dc in dt.Columns)
                {
                    strHtml += "<th>" + dc.ColumnName + "</th>";
                }
                strHtml += "</thead>";
                foreach (DataRow row in dt.Select())
                {
                    strHtml += "<tr class='tdbg' align='center'>";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtml += "<td >&nbsp;</td>";
                            }
                            else
                            {
                                strHtml += "<td>" + row[i] + "</td>";
                            }
                        }
                    }
                    catch
                    {
                    }
                    strHtml += "</tr>";
                }
                strHtml += "</table>";
                this.RunOK.InnerHtml = strHtml;
            }
            else
            {
                this.RunOK.InnerHtml = "<div style='color:#F00'>查询失败或表中无数据</div>";
            }
        }

        protected void DbTableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DbTableDownList.SelectedIndex != 0)
            {
                DataTable tabledlist = null;
                tabledlist = this.b_Label.GetTableField(this.DbTableDownList.SelectedValue, SqlHelper.ConnectionString);
                tabledlist.DefaultView.Sort = "fieldname asc";
                this.DbFieldDownList.DataSource = tabledlist;
                this.DbFieldDownList.DataTextField = "fieldname";
                this.DbFieldDownList.DataValueField = "fieldname";
                this.DbFieldDownList.DataBind();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Config
{
    public partial class DataListView : CustomerPageAction
    {
        string strHtml = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                B_Admin badmin = new B_Admin();
                if (!badmin.GetAdminLogin().IsSuperAdmin()) function.WriteErrMsg("只有超级管理员才有权限访该页!!");
                B_DataList bll = new B_DataList();
                M_DataList mod = bll.SelReturnModel(Convert.ToInt32(Request["ID"]));
                string tableinfo = "";
                string tablelink = "";
                if (Request["type"] == "data")
                {
                    RunSelsql2(mod.TableName);
                    tableinfo = "表数据";
                    tablelink = "<a href='DataListView.aspx?ID=" + Request.QueryString["ID"] + "' target='_self' >[表结构]</a>";
                }
                else
                {
                    RunSelsql(mod.TableName);
                    tableinfo = "表结构";
                    tablelink = "<a href='DataListView.aspx?ID=" + Request.QueryString["ID"] + "&type=data' target='_self' >[表数据]</a>";
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li><li><a href='RunSql.aspx'>开发中心</a></li><li><a href='DatalistProfile.aspx'>系统全库概况</a></li><li>" + tableinfo + tablelink + "</li>");
            }
        }
        protected void RunSelsql(string tablename)
        {
            string sqlstr = "SELECT  (case when a.colorder=1 then d.name else '' end)表名, "
            + " a.colorder 字段序号,"
            + " a.name 字段名,"
            + " (case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end) 标识,"
           + "  (case when (SELECT count(*)"
           + " FROM sysobjects"
           + " WHERE (name in"
                + " (SELECT name"
                     + " FROM sysindexes"
                     + " WHERE (id = a.id) AND (indid in"
                              + "  (SELECT indid"
                              + "  FROM sysindexkeys"
                              + " WHERE (id = a.id) AND (colid in"
                                     + "   (SELECT colid"
                                      + "  FROM syscolumns"
                                       + " WHERE (id = a.id) AND (name = a.name))))))) AND"
                    + " (xtype = 'PK'))>0 then '√' else '' end) 主键,"
           + " b.name 类型,"
           + " a.length 占用字节数,"
           + " COLUMNPROPERTY(a.id,a.name,'PRECISION') as 长度,"
           + " isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as 小数位数,"
           + " (case when a.isnullable=1 then '√'else '' end) 允许空,"
            + " isnull(e.text,'') 默认值,"
           + " isnull(g.[value],'') AS 字段说明"
    + " FROM    syscolumns    a left join systypes b"
    + " on a.xtype=b.xusertype"
    + " inner join sysobjects d"
    //+ " on a.id=d.id    and    d.xtype='U' and    d.name <>'dtproperties'"
    + " on a.id=d.id  and    d.name <>'dtproperties'"
    + " left join syscomments e"
    + " on a.cdefault=e.id"
    + " left join sys.extended_properties g "
    + " on a.id=g.major_id AND a.colid = g.major_id  "
    + " where d.name='" + tablename + "'"
    + " order by a.id,a.colorder";


            strHtml += "<table width='100%' border='0' cellpadding='1' class='table table-striped table-bordered table-hover' align='left'>";
            DataTable zhi = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, sqlstr);
            if (zhi != null && zhi.Rows.Count > 0)
            {
                strHtml += "<thead class='gridtitle'>";
                foreach (DataColumn dc in zhi.Columns)
                {
                    strHtml += "<th>" + dc.ColumnName + "</th>";
                }
                strHtml += "</thead>";
                foreach (DataRow row in zhi.Select())
                {
                    strHtml += "<tr class='tdbg' align='center'>";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtml += "<td>&nbsp;</td>";
                            }
                            else
                            {
                                strHtml += "<td>" + row[i] + "</td>";
                            }
                            //strHtml += row[i] + " ";
                        }
                    }
                    catch
                    {

                    }
                    strHtml += "</tr>";
                }
                strHtml += "</table>";
                //SaveToXml(st);

                this.RunOK.InnerHtml = strHtml;
            }
        }
        protected void RunSelsql2(string tablename)
        {
            string sqlstr = "SELECT  * from " + tablename;


            strHtml += "<table width='100%' border='0' cellpadding='1' class='table table-striped table-bordered table-hover' align='left'>";
            DataTable zhi = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, sqlstr);

            if (zhi != null)
            {
                strHtml += "<thead class='gridtitle'>";
                foreach (DataColumn dc in zhi.Columns)
                {
                    strHtml += "<th>" + dc.ColumnName + "</th>";
                }
                strHtml += "</thead>";
                foreach (DataRow row in zhi.Select())
                {
                    strHtml += "<tr class='tdbg' align='center'>";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtml += "<td>&nbsp;</td>";
                            }
                            else
                            {
                                strHtml += "<td>" + row[i] + "</td>";
                            }
                            //strHtml += row[i] + " ";
                        }
                    }
                    catch
                    {

                    }
                    strHtml += "</tr>";
                }
                strHtml += "</table>";
                //SaveToXml(st);

                this.RunOK.InnerHtml = strHtml;
            }
        }
        protected void Modify_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditDataList.aspx?ID=" + Request.QueryString["ID"]);
        }
        protected void Data_Click(object sender, EventArgs e)
        {
            Response.Redirect("DataListView.aspx?ID=" + Request.QueryString["ID"] + "&type=data");
        }
    }
}
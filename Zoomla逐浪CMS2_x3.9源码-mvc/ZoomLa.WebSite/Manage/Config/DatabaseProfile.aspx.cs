using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;

namespace ZoomLaCMS.Manage.Config
{
    public partial class DatabaseProfile : CustomerPageAction
    {
        string strHtml = string.Empty;
        private string serverdomain = SiteConfig.SiteOption.ProjectServer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>扩展功能</li><li><a href='DatabaseProfile.aspx'>开发中心</a></li><li>系统全库概况</li>");
                TableTotal();
                Data();
            }
        }

        protected void Data()
        {
            Top.Visible = true;
            string sql = "declare @id int declare @type character(2) declare @pages int declare @dbname sysname declare @dbsize dec(15,0) declare @bytesperpage dec(15,0) declare @pagesperMB dec(15,0)   create table #spt_space(objid int null,rows int null,reserved dec(15) null,data dec(15) null,indexp dec(15) null,unused dec(15) null) set nocount on declare c_tables cursor for select id from sysobjects where xtype='U' open c_tables fetch next from c_tables into @id while @@fetch_status=0 begin insert into #spt_space (objid,reserved) select objid=@id,sum(reserved) from sysindexes where indid in (0,1,255) and id=@id select @pages = sum(dpages) from sysindexes where indid <2 and id = @id select @pages = @pages+ isnull(sum(used), 0) from   sysindexes where indid =  255 and id = @id update #spt_space set data = @pages where objid =  @id update #spt_space set indexp = (select sum(used) from sysindexes where indid in (0,1,255) and   id   =   @id)  - data where   objid   =   @id   update #spt_space set unused = reserved  -  (select sum(used) from sysindexes where indid in (0,1,255) and id= @id) where objid = @id update   #spt_space set rows = i.rows from sysindexes i where i.indid < 2 and i.id = @id and objid = @id fetch next from c_tables into @id end select 数据库名 =(select left(name,60) from sysobjects where id=objid), 总记录 =convert(char(11),rows),预留空间大小=ltrim(str(reserved*d.low / 1024.,15,0) + ' ' +  'KB'), 数据占用的空间 = ltrim(str(data * d.low / 1024.,15,0) + ' ' + 'KB'), 索引占用的空间 = ltrim(str(indexp * d.low / 1024.,15,0) + ' ' + 'KB'), 剩余空间大小 = ltrim(str(unused * d.low / 1024.,15,0) + ' ' + 'KB') from #spt_space, master.dbo.spt_values d  where d.number = 1 and d.type = 'E' order by reserved desc drop   table   #spt_space close c_tables deallocate c_tables";

            strHtml += "<table class='table table-striped table-bordered table-hover' style='text-align:center;'>";
            DataTable zhi = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, sql);
            if (zhi != null && zhi.Rows.Count > 0)
            {
                strHtml += "<thead>";
                foreach (DataColumn dc in zhi.Columns)
                {
                    strHtml += "<th style='text-align:center;'>" + dc.ColumnName + "</th>";
                }
                strHtml += "</thead>";
                foreach (DataRow row in zhi.Select())
                {
                    strHtml += "<tr>";
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

        protected void TableTotal()
        {
            this.tableTotal.Visible = true;
            string str = "SELECT count(TABLE_NAME) as counttable FROM INFORMATION_SCHEMA.TABLES";
            DataTable dt = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, str);
            int total = DataConverter.CLng(dt.Rows[0]["counttable"]);
            this.tableTotal.Text = "当前系统共有[" + total + "]张表，详情如下：";

            string strSql = "SELECT * FROM INFORMATION_SCHEMA.TABLES order by TABLE_NAME";
            DataTable dts = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, strSql);
            string strHtmls = "";
            strHtmls += "<table class='table table-striped table-bordered table-hover' style='text-align:center;'>";
            if (dts != null && dts.Rows.Count > 0)
            {
                strHtmls += "<thead>";
                foreach (DataColumn dc in dts.Columns)
                {
                    strHtmls += "<th style='text-align:center;'>" + dc.ColumnName + "</th>";
                }
                strHtmls += "</thead>";
                foreach (DataRow row in dts.Select())
                {
                    strHtmls += "<tr>";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtmls += "<td>&nbsp;</td>";
                            }
                            else
                            {
                                strHtmls += "<td>" + row[i] + "</td>";
                            }
                        }
                    }
                    catch
                    {
                    }
                    strHtmls += "</tr>";
                }
                strHtmls += "</table>";
                this.Div2.InnerHtml = strHtmls;
            }
            else
            {
                this.Div2.InnerHtml = "<div style='color:#F00'>查询失败或表中无数据</div>";
            }
        }
    }
}
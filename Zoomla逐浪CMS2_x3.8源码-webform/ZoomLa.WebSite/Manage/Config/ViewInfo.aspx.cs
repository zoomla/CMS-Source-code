using System;
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
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using ZoomLa.Web;
using ZoomLa.Components;
using System.Xml;


public partial class manage_Config_ViewInfo : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li><li><a href='DatalistProfile.aspx'>开发中心</a></li><li class='active'>视图信息</li>");
            if (!string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
                string ViewName = Request.QueryString["ViewName"];
                SqlParameter para = new SqlParameter("@ViewName", ViewName);
                DataTable dtView = SqlHelper.ExecuteTable(CommandType.Text, "select TABLE_NAME,VIEW_DEFINITION  from information_schema.views where TABLE_NAME=@ViewName", para);//获取视图的名字和定义语句
                if (dtView.Rows.Count == 1)
                {
                    txtVName.Value = ViewName.Substring(5);
                    DataTable dtVT = SqlHelper.ExecuteTable(CommandType.Text, "select object_name(depid) as tableName from sys.sysdepends where object_name(id) = @ViewName group by object_name(depid)", para);//获取视图所有的基表名
                    taSQL.Value = dtView.Rows[0]["VIEW_DEFINITION"].ToString();
                }
                else
                {
                    function.WriteErrMsg("信息有误!", "CreateView.aspx");
                }
            }
        }
    }
}


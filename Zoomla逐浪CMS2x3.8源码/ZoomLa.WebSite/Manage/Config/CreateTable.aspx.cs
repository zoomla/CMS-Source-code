using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using Newtonsoft.Json;

public partial class manage_Config_Default2 : CustomerPageAction
{
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='CreateTable.aspx'>快速建表</a></li>");
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
            if (!badmin.CheckSPwd(Session["Spwd"] as string))
            {
                SPwd.Visible = true;
            }
            else
            {
                maindiv.Visible = true;
            }
            if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                function.WriteErrMsg("错误的Url或非法的请求！", "");
        }
    }
    protected void CreateBtn_Click(object sender, EventArgs e)
    {
        String tabName = "ZL_My_" + txtTabName.Text.Replace(" ", "");
        if (DBCenter.DB.Table_Exist(tabName)) { function.WriteErrMsg("数据表" + tabName + "已存在!"); }
        String jsonStr = Data_Hid.Value;
        List<M_SQL_Field> list = JsonConvert.DeserializeObject<List<M_SQL_Field>>(jsonStr);
        string sql = "CREATE TABLE [" + tabName + "] ({0})";
        string fieldstr = "";
        foreach (var m in list)
        {
            m.fieldName = m.fieldName.Replace(" ", "");
            m.fieldType = m.fieldType.Replace(" ", "").ToLower();
            m.defval = m.defval.Replace(" ", "");
            fieldstr += " [" + m.fieldName + "] ";
            switch (m.fieldType.Trim().ToLower())
            {
                case "int":
                case "money":
                case "ntext":
                case "datetime":
                case "float":
                case "image":
                case "uniqueidentifier":
                case "timestamp":
                    fieldstr += "[" + m.fieldType + "]";
                    break;
                case "decimal":
                    fieldstr += "[decimal] (18, 0)";
                    break;
                case "nvarchar":
                    fieldstr += "[nvarchar](2000)";
                    break;
                case "varbinary":
                    fieldstr += "[varbinary](2000)";
                    break;
                default:
                    break;
            }
            if (m.ispk && m.fieldType.Equals("int"))
            {
                fieldstr += " PRIMARY KEY"; 
                if (m.fieldType.Equals("int")) { fieldstr += " IDENTITY(1,1) NOT NULL"; }
            }
            if (!m.isnull) fieldstr += " NOT NULL ";
            if (!string.IsNullOrEmpty(m.defval)) { fieldstr += "DEFAULT ('" + m.defval + "')"; }
            fieldstr += ",";
        }
        sql=string.Format(sql,fieldstr);
        string result = DBHelper.ExecuteSQL(DBCenter.DB.ConnectionString,sql );
        if (string.IsNullOrEmpty(result))
        {
            remind_div.InnerHtml = "创建[" + tabName + "]成功";
        }
        else 
        {
            remind_div.Attributes["class"] = "alert alert-danger";
            remind_div.InnerHtml = result;
            remind_div.InnerHtml += "<div>" + sql + "</div>";
        }
        remind_div.Visible = true;
    }
}
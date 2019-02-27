using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Content_Addon_ModelContract : System.Web.UI.Page
{
    public int smid { get { return Convert.ToInt32(Request.QueryString["smid"]); } }
    public string smname { get { return Server.UrlDecode(Request.QueryString["smname"]); } }
    public int tmid { get { return Convert.ToInt32(Request.QueryString["tmid"]); } }
    public string conn { get { return Server.UrlDecode(Request.QueryString["conn"]); } }
    B_Model modBll = new B_Model();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.HideBread(Master);
            MyBind();
        }
    }
    public void MyBind()
    {
        M_ModelInfo model = modBll.GetModelById(tmid);
        SModel_L.Text = smname;
        TModel_L.Text = model.ModelName;
        string fieldsql = "SELECT FieldID,FieldName,FieldType,FieldAlias FROM ZL_ModelField Where Sys_type=0 AND ModelID={0} ";
        DataTable smfieldDT = SqlHelper.ExecuteTable(conn, CommandType.Text, string.Format(fieldsql, smid));
        DataTable tmfieldDT = SqlHelper.ExecuteTable(CommandType.Text, string.Format(fieldsql, tmid));
        smfieldDT.Columns.Add(new DataColumn("op",typeof(string)));//add,空值,change
        smfieldDT.Columns.Add(new DataColumn("tfield", typeof(string)));//目标字段名
        for (int i = 0; i < smfieldDT.Rows.Count; i++)
        {
            DataRow dr = smfieldDT.Rows[i];
            if (tmfieldDT.Select("FieldName='" + dr["FieldName"]+"'").Length < 1)
            {
                dr["op"] = "新增";
            }
            else { dr["tfield"] = dr["FieldName"]; }
        }
        RPT.DataSource = smfieldDT;
        RPT.DataBind();
    }
}
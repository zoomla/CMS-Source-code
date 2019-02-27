using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

public partial class App_APPTlp_TlpList : CustomerPageAction
{
    private string xmlPath = function.VToP("/APP/APPTlp/APPTlp.config");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(xmlPath);
        DataTable dt = ds.Tables[0];
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    private void InitXML() 
    {
        DataTable dt = new DataTable();
        dt.TableName = "Tlp";
        dt.Columns.Add(new DataColumn("TlpName", typeof(string)));
        dt.Columns.Add(new DataColumn("VPath", typeof(string)));//虚拟目录名称
        dt.Columns.Add(new DataColumn("Desc", typeof(string)));
        //dt.Columns.Add(new DataColumn("Thumbnail", typeof(string)));//固定为view
        DataRow dr = dt.NewRow();
        dr["TlpName"] = "新闻模板";
        dr["VPath"] = "/APP/APPTlp/news/";
        dr["Desc"] = "主用于新闻资迅网站";
        //dr["Thumbnail"] = "/APP/APPTlp/news/index.jpg";
        dt.Rows.Add(dr);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt); 
        ds.DataSetName = "TlpSets";
        ds.WriteXml(xmlPath);
    }
}
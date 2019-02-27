using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Content_ECharts_ChartCite : CustomerPageAction
{
    M_Content_Chart chartMod = new M_Content_Chart();
    B_Content_Chart chartBll = new B_Content_Chart();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Mid < 1) { function.WriteErrMsg("未传入图表ID"); }
        if (!IsPostBack)
        {
            chartMod = chartBll.SelReturnModel(Mid);
            TName_L.Text = chartMod.TName;
            SType_L.Text = chartMod.GetTypeStr();
            Cite_T.Text = "<iframe src='http://"+Request.Url.Authority+"/Plugins/ECharts/ShowU.aspx?ID="+Mid+"' style='width:700px;height:500px;'></iframe>";
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>智慧图表</a></li><li class='active'><a href='" + Request.RawUrl + "'>图表引用</a></li>");
        }
    }
}
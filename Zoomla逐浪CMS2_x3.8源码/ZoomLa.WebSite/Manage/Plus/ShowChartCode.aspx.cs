using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class manage_Plus_ShowChartCode : CustomerPageAction
{
    private ChartCall chart = new ChartCall();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ADManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            string ChartID = base.Request.QueryString["ChartID"];
            chart = B_ADZone.getChartByChartID(DataConverter.CLng(ChartID));
            int ChartWidth = chart.ChartWidth;
            int ChartHeight = chart.ChartHeight;
            string ChartTitle = chart.ChartTitle;
            string ChartUnit = chart.ChartUnit;
            string strUrl = HttpContext.Current.Request.Url.Authority.ToString();
            //string strRaw = HttpContext.Current.Request.Url.PathAndQuery.ToString();
            string ChartType = Request["ChartType"];
            strUrl = "http://" + strUrl;
            string str = "";
            str += "<iframe src=\"" + strUrl;

            str += "/Plugins/Chart/Show.aspx?Did=" + ChartID;

            str += "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\"  scrolling=\"no\" ";
            str += "width=\"" + (ChartWidth + 80) + "\"" + " height=\"" + (ChartHeight + 80) + "\"" + ">";
            str += "</iframe>";

            TxtChartCode.Text = str;
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Plus/ADManage.aspx'>广告管理</a></li><li><a href='../Plus/ChartManage.aspx'>图表管理</a></li><li class='active'>获取图表代码</li>");
    }
}
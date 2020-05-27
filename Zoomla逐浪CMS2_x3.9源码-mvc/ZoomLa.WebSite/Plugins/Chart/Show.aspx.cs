namespace ZoomLaCMS.Plugins.Chart
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.BLL;
    public partial class Show : System.Web.UI.Page
    {
        //扩展功能--广告管理--图表
        private ChartCall chart = new ChartCall();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string src = "";
                chart = B_ADZone.getChartByChartID(DataConverter.CLng(Request.QueryString["Did"]));
                try
                {
                    if (chart.ChartType == "线状图")//曲线
                    {
                        if (Request.QueryString["width"] == null || Request.QueryString["height"] == null)
                        {
                            src = "Line.aspx?Cid=" + Request.QueryString["Did"];
                            Bbiao.Attributes.Add("width", chart.ChartWidth.ToString()); Bbiao.Attributes.Add("height", chart.ChartHeight.ToString());
                        }
                        else
                        {
                            src = "Line.aspx?Cid=" + Request.QueryString["Did"] + "&width=" + Request.QueryString["width"] + "&height=" + Request.QueryString["height"];
                            Bbiao.Attributes.Add("width", Request.QueryString["width"]); Bbiao.Attributes.Add("height", Request.QueryString["height"]);
                        }
                    }
                    else if (chart.ChartType == "柱状图")//柱状
                    {
                        if (Request.QueryString["width"] == null || Request.QueryString["height"] == null)
                        {
                            src = "colum.aspx?Cid=" + Request.QueryString["Did"];
                            Bbiao.Attributes.Add("width", chart.ChartWidth.ToString()); Bbiao.Attributes.Add("height", chart.ChartHeight.ToString());
                        }
                        else
                        {
                            src = "colum.aspx?Cid=" + Request.QueryString["Did"] + "&width=" + Request.QueryString["width"] + "&height=" + Request.QueryString["height"];
                            Bbiao.Attributes.Add("width", Request.QueryString["width"]); Bbiao.Attributes.Add("height", Request.QueryString["height"]);
                        }
                    }
                    else if (chart.ChartType == "饼状图")//饼状
                    {
                        if (Request.QueryString["width"] == null || Request.QueryString["height"] == null)
                        {
                            src = "pie-basic.aspx?Cid=" + Request.QueryString["Did"];
                            Bbiao.Attributes.Add("width", chart.ChartWidth.ToString()); Bbiao.Attributes.Add("height", chart.ChartHeight.ToString());
                        }
                        else
                        {
                            src = "pie-basic.aspx?Cid=" + Request.QueryString["Did"] + "&width=" + Request.QueryString["width"] + "&height=" + Request.QueryString["height"];
                            Bbiao.Attributes.Add("width", Request.QueryString["width"]); Bbiao.Attributes.Add("height", Request.QueryString["height"]);
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("数据不存在！");
                }
                Bbiao.Attributes.Add("src", src);

            }
        }
    }
}
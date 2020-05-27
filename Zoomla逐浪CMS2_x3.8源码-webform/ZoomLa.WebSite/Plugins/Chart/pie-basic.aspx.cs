using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class Skin_pie_basic : CustomerPageAction
{
    private ChartCall chart = new ChartCall();
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        { 
            if (Request.QueryString["Did"] != null)
            {
                string src = "";
                chart = B_ADZone.getChartByChartID(DataConverter.CLng(Request.QueryString["Did"]));
                if (chart.ChartType == "线状图")//曲线
                {
                    src = "Line.aspx?Cid=" + Request.QueryString["Did"];
                }
                else if (chart.ChartType == "柱状图")//柱状
                {
                    src = "colum.aspx?Cid=" + Request.QueryString["Did"];
                }
                else if (chart.ChartType == "饼状图")//饼状
                {
                    src = "pie-basic.aspx?Cid=" + Request.QueryString["Did"];
                }
                Response.Write(chart.ChartWidth+"|"+chart.ChartHeight+"|"+src);
                Response.End();
            }

            if (Request.QueryString["Datas"] == null)
            {
                if (Request.QueryString["Cid"] == "0")
                {
                    string[] wihi = Request.QueryString["wihi"] == null ? "300|200".Split('|') : Request.QueryString["wihi"].Split('|');
                    try
                    {
                        BiaoS.Width = wihi[0];
                        BiaoS.Height = wihi[1];
                        BiaoS.Title = "示例";
                        BiaoS.unit = "";
                        BiaoS.X = "['a',1],['b',2],['c',3]";
                    }
                    catch (Exception)
                    {
                        Response.Write("<font color='red'>数据错误！</font>");
                    }
                }
                else
                {
                    chart = B_ADZone.getChartByChartID(DataConverter.CLng(Request.QueryString["Cid"]));
                    if (Request.QueryString["width"] == null || Request.QueryString["height"] == null)
                    {
                        BiaoS.Width = chart.ChartWidth.ToString();
                        BiaoS.Height = chart.ChartHeight.ToString();
                    }
                    else
                    {
                        BiaoS.Width = Request.QueryString["width"];
                        BiaoS.Height = Request.QueryString["height"];
                    }
                    BiaoS.Title = chart.ChartTitle;
                    BiaoS.unit = chart.ChartUnit;
                    string[] Datas = chart.CharData.Split('|');
                    string[] y = Datas[1].Split(',');
                    string Xy = "";
                    string[] x = Datas[0].Split(',');
                    for (int i = 0; i < y.Length; i++)
                    {
                        Xy += "['" + y[i] + "'," + x[i] + "],";
                    }

                    BiaoS.Y = "";
                    BiaoS.X = Xy.TrimEnd(',');
                }
            }
            else
            {
               string [] Bases = Request.QueryString["Bases"].Split('|');
               string [] Coordinate = Request.QueryString["Datas"].Split('|');
               BiaoS.Width = Bases[0];
               BiaoS.Height = Bases[1];
               BiaoS.Title = Bases[2];
               BiaoS.unit = Bases[3];
               string[] y = Coordinate[1].Split(',');
               string Xy = "";
               string[] x = Coordinate[0].Split(',');
               for (int i = 0; i < y.Length; i++)
               {
                   Xy += "['" + y[i] + "'," + x[i] + "],";
               }

               BiaoS.Y = "";
               BiaoS.X = Xy.TrimEnd(',');

            }
        }
    }
}
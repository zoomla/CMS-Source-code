using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class Skin_line : CustomerPageAction
{
    private string[] Bases = { };
    private string[] Coordinate = { };
    private string type = "";
    private ChartCall chart = new ChartCall();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            if (Request.QueryString["Datas"] != null)
            {
                type = Request.QueryString["type"] == null ? "1" : Request.QueryString["type"];
                Bases = Request.QueryString["Bases"].Split('|');
                //控件hid不为空时读取
                Coordinate =  Request.QueryString["Datas"].Split('^');
                try
                {
                    string[] ys = { };
                    string x = "";
                    string y = "";
                    bool yis = false;
                    for (int i = 0; i < Coordinate.Length; i++)
                    {
                        ys = Coordinate[i].Trim().TrimStart('{').TrimEnd('}').Split('|');

                        x += "{name:'" + ys[0] + "',marker:{symbol: 'square' }, data: [" + ys[1] + "]},";

                        if (!yis)
                        {
                            foreach (string item in ys[2].Split(','))
                            {
                                y += "'" + item + "',";
                                yis = true;
                            }
                        }
                    }

                    if (type == "1")
                    {
                        BiaoS.Width = Bases[0];
                        BiaoS.Height = Bases[1];
                        BiaoS.Title = Bases[2];
                        BiaoS.unit = Bases[3];
                        BiaoS.Y = y;
                        BiaoS.X = x.TrimEnd(',');
                    }
                }
                catch (Exception)
                {
                    Response.Write("<font color='red'>数据错误！</font>");
                }
            }
            else
            {
                if (Request.QueryString["hid"] != null)//控件hid不为空时，读取父框架数据
                {
                    type = Request.QueryString["type"] == null ? "1" : Request.QueryString["type"];
                    Bases = Request.QueryString["Bases"].Split('|');
                    if (type == "1")
                    {
                        BiaoS.Width = Bases[0];
                        BiaoS.Height = Bases[1];
                        BiaoS.Title = Bases[2];
                        BiaoS.unit = Bases[3];
                    }
                    return;
                }
                if (Request.QueryString["Cid"] == "0")
                {
                    string[] wiHi = Request.QueryString["wihi"] == null ? "300|200".Split('|') : Request.QueryString["wihi"].Split('|');
                    try
                    {

                        BiaoS.Width = wiHi[0];
                        BiaoS.Height = wiHi[1];
                        BiaoS.Title = "示例";
                        BiaoS.unit = "位";
                        BiaoS.Y = "'a','b','c'";
                        BiaoS.X = "{name:'2011', data: [1,2,3]},{name:'2012', data: [2,4,7]}";

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
                    string[] Datas = chart.CharData.Split('^');
                    string[] Cys = { };
                    string Cx = "";
                    string Cy = "";
                    bool Cyis = false;
                    for (int i = 0; i < Datas.Length; i++)
                    {
                        Cys = Datas[i].Trim().TrimStart('{').TrimEnd('}').Split('|');
                        Cx += "{name:'" + Cys[0] + "',marker:{symbol: 'square' }, data: [" + Cys[1] + "]},";
                        if (!Cyis)
                        {
                            foreach (string item in Cys[2].Split(','))
                            {
                                Cy += "'" + item + "',";
                                Cyis = true;
                            }
                        }
                    }
                    BiaoS.Y = Cy;
                    BiaoS.X = Cx.TrimEnd(',');
                }
            }
        }
    }
}

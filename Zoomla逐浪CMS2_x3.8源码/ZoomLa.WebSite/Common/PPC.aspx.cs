using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using ZoomLa.SQLDAL;

public partial class Common_Ppc: System.Web.UI.Page
{
    public int Direction { get { return DataConvert.CLng(Request.QueryString["Direction"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["FValue"] != null && Request["FValue"] != "")
        {
            string addstr = Request["FValue"]; //"江西省|南昌市|西湖区";//
            address.Value = addstr;
        }
        if (!string.IsNullOrEmpty(Request.QueryString["dptype"]))
        {
            three_div.Visible = false;
            four_div.Visible = true;
        }
        if (Direction == 1)
        {
            string css = "  <style type=\"text/css\">"
        + ".text_200_auto {display:block;margin-bottom:3px;width:200px;}</style>";
            CSS_L.Text = css;
        }
    }

}
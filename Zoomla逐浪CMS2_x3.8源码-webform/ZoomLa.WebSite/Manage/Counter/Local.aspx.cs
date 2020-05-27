using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;

public partial class manage_Counter_Local : CustomerPageAction
{
    //public B_Counter counterabout = new B_Counter();
    public int pcount1;
    public DataSet dsadmin;
    public DataSet dsadmin1;
    public int SumCount;
    public int MaxCount;
    public int LeftPx;
    public int getlength(int o)
    {
        if (MaxCount == 0)
        {
            LeftPx = 0;
        }
        else
        {
            LeftPx = Convert.ToInt32((250 * o) / MaxCount);
        }
        return 1 + LeftPx;
    }
    public int getlength1(int o)
    {
        if (MaxCount == 0)
        {
            LeftPx = 0;
        }
        else
        {
            LeftPx = Convert.ToInt32((250 * o) / MaxCount);
        }
        return 250 - LeftPx;
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        //pcount1 = (int)counterabout.SelectCountBOL("ZL_Count_Local");
        if (pcount1 != 0)
        {
            dsadmin = new DataSet();
            //dsadmin = counterabout.CountBOLList("ZL_Count_Local");
            //MaxCount = (int)counterabout.SelectMaxCountBOL("ZL_Count_Local");
            //dsadmin1 = counterabout.SelectSumCountBOL("ZL_Count_Local");
            SumCount = Convert.ToInt32(dsadmin1.Tables[0].Rows[0][0].ToString());
            Repeater1.DataSource = dsadmin.Tables[0];
            Repeater1.DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Local.aspx'>地区统计报表</a></li>");
    }
}
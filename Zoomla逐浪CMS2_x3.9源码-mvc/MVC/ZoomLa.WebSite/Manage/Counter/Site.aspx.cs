using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Counter
{
    public partial class Site : CustomerPageAction
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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            //pcount1 = (int)counterabout.SelectCountBOL("ZL_Count_Site");
            if (pcount1 != 0)
            {
                dsadmin = new DataSet();
                //dsadmin = counterabout.CountBOLList("ZL_Count_Site");
                //MaxCount = (int)counterabout.SelectMaxCountBOL("ZL_Count_Site");
                //dsadmin1 = counterabout.SelectSumCountBOL("ZL_Count_Site");
                SumCount = Convert.ToInt32(dsadmin1.Tables[0].Rows[0][0].ToString());
                Repeater1.DataSource = dsadmin.Tables[0];
                Repeater1.DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>访问统计</a></li><li class='active'>访问渠道统计报表</li>");
        }
    }
}
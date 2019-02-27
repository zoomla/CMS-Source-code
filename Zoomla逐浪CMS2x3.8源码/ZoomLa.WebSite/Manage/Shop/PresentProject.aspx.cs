namespace Zoomla.Website.manage.Shop
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Text;

    public partial class PresentProject : CustomerPageAction
    {
        private B_Promotions bll = new B_Promotions();
        private B_Model bmode = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            string menu = Request.QueryString["menu"];
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                DataBind();
                if (menu == "del")
                {
                    bll.DeleteByID(id);
                    Response.Redirect("PresentProject.aspx");
                }
            }
           
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销管理</a></li><li class='active'>促销方案管理<a href='AddPresentProject.aspx'>[添加促销方案]</a></li>");
        }
        public void DataBind(string key = "")
        {
            Egv.DataSource = bll.GetPromotionsAll();
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }

        public string rePresentmoney(string GetPresent, string Presentmoney)
        {
            string outstr;

            if (DataConverter.CLng(GetPresent) == 1)
            {
                double dd;
                string tempdd;

                dd = System.Math.Round(DataConverter.CDouble(Presentmoney), 2);

                tempdd = dd.ToString();

                if (tempdd.IndexOf(".") == -1) { tempdd = tempdd + ".00"; }
                outstr = "可以以 " + tempdd.ToString() + " 元超值换购礼品";

            }
            else
            {
                outstr = "";
            }
            return outstr;
        }

        public string remoney(string money1, string money2)
        {
            double dd, dd2;
            string ss, ss2, outstr;
            dd = System.Math.Round(DataConverter.CDouble(money1), 2);
            dd2 = System.Math.Round(DataConverter.CDouble(money2), 2);
            ss = dd.ToString();
            ss2 = dd2.ToString();
            if (ss.IndexOf(".") == -1) { ss = ss + ".00"; }
            if (ss2.IndexOf(".") == -1) { ss2 = ss2 + ".00"; }
            outstr = ss + " ≤金额＜" + ss2;
            return outstr;
        }
        public string ontimes(string toptime, string endtime)
        {
            string tempstr;
            DateTime temptime = DataConverter.CDate(toptime);
            DateTime temptime2 = DataConverter.CDate(endtime);
            tempstr = temptime.ToShortDateString() + " 至 " + temptime2.ToShortDateString();

            return tempstr;
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    bll.DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));
                    break;
            }
            DataBind();
        }
    }
}
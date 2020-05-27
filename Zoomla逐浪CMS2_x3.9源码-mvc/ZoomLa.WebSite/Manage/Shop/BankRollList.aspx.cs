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
using ZoomLa.Components;
using System.Text.RegularExpressions;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class BankRollList : CustomerPageAction
    {
        protected B_Payment payll = new B_Payment();
        protected B_User ull = new B_User();
        protected B_PayPlat prell = new B_PayPlat();
        protected B_ModelField mll = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='BankRollList.aspx'>明细记录</a></li><li class='active'>资金明细</li>");
            }
        }
        protected void MyBind()
        {
            this.Egv.DataSource = SelAll();
            this.Egv.DataBind();
        }
        public DataTable SelAll()
        {
            return SqlHelper.JoinQuery("A.*,B.PayPlatName", "ZL_Payment", "ZL_PayPlat", "A.PayPlatID=B.PayPlatID");
        }
        public string GetContent(string payPlat)
        {
            string content = getPayPlat(payPlat);
            return "  <a href='BankRollList.aspx?PayID=" + payPlat + "&souchtables=" + Request.QueryString["souchtables"] + "'>" + content + "</a>";
        }
        protected string getusername(string userid)
        {
            M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(userid));
            return uinfo.UserName;
        }

        protected string GetStatus(string status)
        {
            if (status == "3" || status == "5")
                return "<font color=green>√</font>";
            else
                return "<font color=red>×</font>";
        }
        protected string GetCStatus(string cstatus)
        {
            if (DataConverter.CBool(cstatus))
                return "<font color=green>√</font>";
            else
                return "<font color=red>×</font>";
        }
        protected string getPayPlat(string id)
        {
            if ((DataConverter.CLng(id) <= 0))
            { return ""; }
            return prell.GetPayPlatByid(DataConverter.CLng(id)).PayPlatName;
        }
        protected void souchok_Click(object sender, EventArgs e)
        {
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.MyBind();
        }
    }
}
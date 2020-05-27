using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class home : System.Web.UI.Page
    {
        public int Mid { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        B_WX_APPID wxBll = new B_WX_APPID();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string alias = "";
                if (Mid > 0)
                {
                    M_WX_APPID model = wxBll.SelReturnModel(Mid);
                    alias = " [公众号:" + model.Alias + "]";
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>微信应用" + alias + "</li>");
            }

        }
    }
}
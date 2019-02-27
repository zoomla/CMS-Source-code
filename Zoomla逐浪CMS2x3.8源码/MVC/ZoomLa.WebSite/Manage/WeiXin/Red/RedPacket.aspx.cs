using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Other;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class RedPacket : System.Web.UI.Page
    {
        private int AppID { get { return DataConvert.CLng(Request.QueryString["appid"]); } }
        B_WX_RedPacket redBll=new B_WX_RedPacket();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='Home.aspx'>移动微信</a></li><li class='active'><a href='RedPacket.aspx'>红包列表</a> [<a href='RedPacketAdd.aspx?appid="+AppID+"'>添加红包</a>]</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = redBll.Sel(AppID);
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    break;
            }
            MyBind();
        }
    }
}
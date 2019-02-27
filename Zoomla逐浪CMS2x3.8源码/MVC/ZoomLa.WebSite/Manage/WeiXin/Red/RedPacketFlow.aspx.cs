using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Other;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class RedPacketFlow : System.Web.UI.Page
    {
        B_WX_RedPacket redBll=new B_WX_RedPacket();
        B_WX_RedDetail detBll=new B_WX_RedDetail();
        private int MainID {get{return DataConvert.CLng(Request.QueryString["MainID"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            EGV.DataSource = detBll.Sel(MainID, "");
            EGV.DataBind();
            M_WX_RedPacket redMod = redBll.SelReturnModel(MainID);
            Call.SetBreadCrumb(Master, "<li><a href='Home.aspx'>移动微信</a></li><li class='active'><a href='RedPacket.aspx?appid=" + redMod.AppID + "'>红包列表</a></li><li>红包详情列表</li>");
            RedPacket_L.Text = string.Format("红包：{0},起始时间：{1},到期时间：{2},金额范围：{3}", redMod.Name, redMod.SDate.ToString("yyyy-MM-dd"), redMod.EDate.ToString("yyyy-MM-dd"), redMod.AmountRange);
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetStatus()
        {
            switch (Convert.ToInt32(Eval("zstatus")))
            {
                case 1:
                    return "未领取";
                case 99:
                    return "<span style='color:red;'>已领取</span>";
                default:
                    return "状态[" + Eval("zstatus") + "]";
            }
        }

        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                detBll.Del(ids);
            }
            MyBind();
        }
    }
}
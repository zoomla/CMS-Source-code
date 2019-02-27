using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Other;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WeiXin.Red
{
    public partial class RedDetail : System.Web.UI.Page
    {
        B_WX_APPID appBll = new B_WX_APPID();
        B_WX_RedPacket redBll = new B_WX_RedPacket();
        B_WX_RedDetail detBll = new B_WX_RedDetail();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_WX_RedDetail detMod = detBll.SelReturnModel(Mid);
            M_WX_RedPacket redMod = redBll.SelReturnModel(detMod.MainID);
            M_WX_APPID appMod = appBll.SelReturnModel(redMod.AppID);
            Alias_L.Text = "[" + appMod.Alias + "]的红包[" + redMod.Name + "]";
            RedCode_L.Text = detMod.RedCode;
            UserName_L.Text = detMod.UserName;
            function.Script(this, "SetRadVal('zstatus_rad','" + detMod.ZStatus + "');");
            Call.SetBreadCrumb(Master, "<li><a href='Home.aspx'>移动微信</a></li><li class='active'><a href='RedPacketFlow.aspx?mainid=" + detMod.MainID + "'>红包列表</a></li><li>红包详情</li>");
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_WX_RedDetail detMod = detBll.SelReturnModel(Mid);
            detMod.Amount = DataConvert.CDouble(Amount_T.Text);
            if (detMod.Amount < 1) { function.WriteErrMsg("红包金额不能小于1"); }
            if (detMod.Amount > 200) { function.WriteErrMsg("红包金额不能大于200"); }
            detMod.ZStatus = DataConvert.CLng(Request.Form["zstatus_rad"]);
            detBll.UpdateByID(detMod);
            function.WriteSuccessMsg("修改成功", "RedPacketFlow.aspx?mainid="+detMod.MainID);
        }
    }
}
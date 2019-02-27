using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using ZoomLa.Components;

public partial class User_CashCoupon_ArriveJihuo : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        //#region 获得提交过来的参数
        string UserName = buser.GetLogin().UserName;
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        string yt = Server.HtmlEncode(ANo.Text.Trim());
        string yp = Server.HtmlEncode(APwd.Text.Trim());
        if (yt == "" || yp == "")
        {
            Response.Write("<script>alert('请输入完整的优惠券信息！')</script>");
            ANo.Text = null;
            APwd.Text = null;
        }
        else if (!(yt == "") && !(yp == ""))
        {
            B_Arrive b = new B_Arrive();

            //获得用户商城信息
            M_UserInfo muser = buser.GetLogin();

            //获得优惠券的所属UserID
            int uid = b.GetUserid(yt);//uid=0
            decimal mianzhi = b.GetOtherArrive(muser.UserID, yt, yp);//muser.UserID=1

            //throw new Exception(uid.ToString());
            //获得用户基本信息
            M_Uinfo muinfo = buser.GetUserBaseByuserid(uid);
            //if (mianzhi == 0)
            //{
            //    Response.Write("<script>alert('优惠券信息输入有误！（请勿输入自己的优惠券信息）');</script>");
            //    ANo.Text = null;
            //    APwd.Text = null;
            //}
            //else
            //{
            b.UpdateState(yt);
            b.UpdateUseTime(yt);

            //优惠券的实例
            M_Arrive m = new M_Arrive();
            m = b.SelReturnModel(yt, yp);
            int type = m.Type;
            string Type = string.Empty;
            double Amount = m.Amount;
            string tt = "优惠券激活成功" + "！此优惠券的面值为[" + Type + "]" + Amount + "。";

            string url = "#";
            Response.Write("<script>alert('" + tt + "'); location='" + url + "';</script>");
            ANo.Text = null;
            APwd.Text = null;
            //}
        }
    }
}
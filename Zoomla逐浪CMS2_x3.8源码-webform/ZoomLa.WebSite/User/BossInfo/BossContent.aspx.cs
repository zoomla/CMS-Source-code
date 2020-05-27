using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;
public partial class User_BossInfo_BossContent : System.Web.UI.Page
{
    B_BossInfo boll = new B_BossInfo();
    private B_User bll = new B_User();
    private B_Node bnll = new B_Node();
    private B_Card bc = new B_Card();
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo uinfo = buser.GetLogin();
        int nodeid = string.IsNullOrEmpty(Request.QueryString["nodeid"]) ? -1 : DataConverter.CLng(Request.QueryString["nodeid"]);
        int pardentid = string.IsNullOrEmpty(Request.QueryString["pardentid"]) ? -1 : DataConverter.CLng(Request.QueryString["pardentid"]);
        if (nodeid == -1)
            function.WriteErrMsg("参数错误");
        M_BossInfo MBoss = boll.GetSelect(DataConverter.CLng(nodeid));
        this.tx_cname.Text = MBoss.CName;
        txtTel.Text = MBoss.CTel;

        this.fhwunum.Text = "";
        DataTable dtCount = null;
        if (dtCount.Rows.Count > 0)
        {
            this.Enum.Text = dtCount.Rows.Count.ToString();
        }
        else
        {
            this.Enum.Text = "0";
        }
    }
    private double GetNum(string zong1, int userid)
    {
        double zong = 0;
        //删除最后一个逗号
        if (zong1.LastIndexOf(",", zong1.Length - 1, 1) > -1)
            zong1 = zong1.Substring(0, zong1.Length - 1);

        DataTable dtt;
        if (string.IsNullOrEmpty(zong1))
            dtt = bc.SelCarByUserIDs(userid.ToString(), 1);//该代理商发出的VIP卡产生的订单
        else
        {
            dtt = bc.SelCarByUserIDs(zong1 + "," + userid.ToString(), 1);//该代理商发出的VIP卡产生的订单
        }
        tx_num.Text = dtt.Rows.Count.ToString();
        foreach (DataRow rr in dtt.Rows)
        {
            if (DataConverter.CLng(rr["OrderStatus"]) == 1)//订单状态
            {
                zong += DataConverter.CDouble(rr["Ordersamount"]);//累加订单金额
            }
        }

        tx_zong.Text = zong.ToString();
        return zong;
    }
}

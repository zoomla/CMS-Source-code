using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class PayOnline_BaoFaoShow : System.Web.UI.Page
{
    Pay_BaoFa baofa = new Pay_BaoFa();
    B_Payment payBll = new B_Payment();
    B_PayPlat platBll = new B_PayPlat();
    //宝付用户回调显示页
    protected void Page_Load(object sender, EventArgs e)
    {
        string MemberID = Request.Params["MemberID"];//商户号
        string TerminalID = Request.Params["TerminalID"];//商户终端号
        string TransID = Request.Params["TransID"];//商户流水号
        string Result = Request.Params["Result"];//支付结果(1:成功,0:失败)
        string ResultDesc = Request.Params["ResultDesc"];//支付结果描述
        string FactMoney = Request.Params["FactMoney"];//实际成交金额
        string AdditionalInfo = Request.Params["AdditionalInfo"];//订单附加消息
        string SuccTime = Request.Params["SuccTime"];//交易成功时间
        string Md5Sign = Request.Params["Md5Sign"].ToLower();//md5签名
        M_Payment payMod = payBll.SelModelByPayNo(TransID);
        M_PayPlat platMod = platBll.SelReturnModel(payMod.PayPlatID);
        if (platMod.PayClass != (int)M_PayPlat.Plat.BaoFo) { function.WriteErrMsg("回调页面错误"); }
        //string Md5Key = ConfigurationManager.AppSettings["Md5key"];//密钥 双方约定
        //String mark = "~|~";//分隔符



        //string _Md5Key = WebConfigurationManager.AppSettings["Md5key"];

        //string _WaitSign = "MemberID=" + MemberID + mark + "TerminalID=" + TerminalID + mark + "TransID=" + TransID + mark + "Result=" + Result + mark + "ResultDesc=" + ResultDesc + mark
        //     + "FactMoney=" + FactMoney + mark + "AdditionalInfo=" + AdditionalInfo + mark + "SuccTime=" + SuccTime
        //     + mark + "Md5Sign=" + Md5Key;
        lbMoney.Text = (float.Parse(FactMoney) / 100).ToString() + " 元";
        lbDate.Text = SuccTime;
        lbFlag.Text = baofa.GetErrorInfo(Result, ResultDesc) + "-====";
        lbOrderID.Text = TransID;

        //if (Md5Sign.ToLower() == StringHelper.MD5(_WaitSign).ToLower())
        //{
            
        //}
        //else
        //{
        //    Response.Write("校验失败");
        //}
    }
}
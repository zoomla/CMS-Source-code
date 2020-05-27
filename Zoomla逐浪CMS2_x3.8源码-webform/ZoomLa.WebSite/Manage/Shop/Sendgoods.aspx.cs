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
using System.Xml;
using System.Text;
using System.IO;
using System.Net;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

/// <summary>
/// 各家快递公司都属于EXPRESS（快递）的范畴
/// </summary>
public partial class sendgoods : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li>店铺订单</li><li>订单详情</li>");
        B_PayPlat paybll = new B_PayPlat();
        DataTable pay = paybll.GetPayPlatByClassid(1);
        ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的////////////////////////////
        ZoomLa.Model.M_Alipay_config con = new ZoomLa.Model.M_Alipay_config();
        string input_charset = con.Input_charset;
        string sign_type = con.Sign_type;
        ///////////////////////请求参数/////////////////////////////////////////////////////////////////////
        //--------------必填参数--------------
        //支付宝交易号。它是登录支付宝网站在交易管理中查询得到，一般以8位日期开头的纯数字（如：20100419XXXXXXXXXX） 
        string trade_no = Request.QueryString["trade_no"];
        //物流公司名称
        string logistics_name = Request.QueryString["logistics_name"];
        //物流发货单号
        string invoice_no = Request.QueryString["invoice_no"];
        //物流发货时的运输类型，三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）
        string transport_type = Request.QueryString["transport_type"];
        int id = Convert.ToInt32(Request.QueryString["id"]+"");
        String Email = Request.QueryString["email"];
        //卖家本地电脑IP地址
        string seller_ip = "";
        //构造请求函数
        B_Alipay_shipments_service aliService=new B_Alipay_shipments_service(
        pay.Rows[0]["AccountID"].ToString(),
        trade_no,
        logistics_name,
        invoice_no,
        transport_type,
        seller_ip,
        pay.Rows[0]["MD5Key"].ToString(),
        input_charset,
        sign_type);
        /***********************含XML远程解析***********************/
        //注意：远程解析XML出错，与IIS服务器配置有关
        string url = aliService.Create_url();

        XmlTextReader Reader = new XmlTextReader(url);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Reader);

        //解析XML，获取XML返回的数据，如：请求处理是否成功、商家网站唯一订单号、支付宝交易号、发货时间等
        string nodeIs_success = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
        string nodeOut_trade_no = "";
        string nodeTrade_no = "";
        string nodeTrade_status = "";
        string nodeLast_modified_time = "";
        string nodeError = "";

        if (nodeIs_success == "T")
        {
            nodeOut_trade_no = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/out_trade_no").InnerText;
            nodeTrade_no = xmlDoc.SelectSingleNode("/alipay/request").ChildNodes[2].InnerText;
            nodeTrade_status = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/trade_status").InnerText;
            nodeLast_modified_time = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/last_modified_time").InnerText;
            fahuo(id, Email);

        }
        else
        {
            nodeError = xmlDoc.SelectSingleNode("/alipay/error").InnerText;
        }

        //打印页面
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<table boder='2' class=border  width=350 cellpadding=5 cellspacing=0>");
        sbHtml.Append("<tr><td align=center  colspan=2>发货结果</td></tr>");
        String successInfo=nodeIs_success=="T"?"成功":"失败";
        sbHtml.Append("<tr><td  align=right>请求处理是否成功：</td><td = align=left>" + successInfo + "</td></tr>");
        if (nodeIs_success == "T")
        {
            sbHtml.Append("<tr><td  align=right>商户网站订单号：</td><td class=font_content align=left>" + nodeOut_trade_no + "</td></tr>");
            sbHtml.Append("<tr><td  align=right>交易状态：</td><td = align=left>" + nodeTrade_status + "</td></tr>");
            sbHtml.Append("<tr><td  align=right>发货时间等：</td><td = align=left>" + nodeLast_modified_time + "</td></tr>");
        }
        else
        {
            String errorInfo = String.Empty;
            switch (nodeError) 
            {
                case "ILLEGAL_ARGUMENT": errorInfo = @"参数不正确：<br>
                            1）、承运公司名称(logistics_name)长度1到64，承运单号码(invoice_no)长度1到32。<br>
                            2）、IP不正确。<br>
                            3）、货运方式(transport_type),交易创建时的货运方式(create_transport_type)错误。<br>
                            4）、交易号(trade_no)不能为空<br>。
                            5）、物流名称(logistics_name)错误。"; break;
                case "TRADE_NOT_EXIST": errorInfo = @"外部交易不存在：<br>
                            1）、交易号不正确。<br>
                            2）、非平台商的交易。"; break;
                case "GENERIC_FAILURE": errorInfo = "执行命令错误"; break;
                default: errorInfo = "未知错误"; break;
            }
            sbHtml.Append("<tr><td align=right valign=top>错误：</td><td  align=left>" + errorInfo + "</td></tr>");
        }
        sbHtml.Append("</table>");

        LblXml.Text = sbHtml.ToString();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //请在此处编写商户发货成功后的业务逻辑程序代码，以便把商户网站里的该笔订单与支付宝的订单信息同步。

        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }
    public void fahuo(int id,String Email)
    {
        B_CartPro cpl = new B_CartPro();
        B_OrderList oll = new B_OrderList();
        B_Product pll = new B_Product();
        B_Stock Sll = new B_Stock();

        //历遍清单所有商品数量，查找库存///////////////////
        DataTable Unew = cpl.GetCartProOrderID(id); //获得详细清单列表
        M_OrderList orlist = oll.GetOrderListByid(id);
        for (int s = 0; s < Unew.Rows.Count; s++)
        {
            int Onum = ZoomLa.Common.DataConverter.CLng(Unew.Rows[s]["Pronum"]);
            int Opid = ZoomLa.Common.DataConverter.CLng(Unew.Rows[s]["ProID"]);

            M_Product pdin = pll.GetproductByid(Opid);//获得商品信息

            if (pdin.JisuanFs == 0)
            {
                int pstock = pdin.Stock - Onum;//库存结果,返回的商品数量
                pll.ProUpStock(Opid, pstock);
            }
            M_Stock SData = new M_Stock();
            SData.id = 0;
            SData.proid = Opid;
            SData.stocktype = 1;
            SData.proname = pdin.Proname;
            SData.danju = "CK" + orlist.OrderNo.ToString();
            SData.adduser = orlist.Reuser.ToString();
            SData.addtime = DateTime.Now;
            SData.content = "订单:" + orlist.Reuser.ToString() + "发货";
            SData.Pronum = ZoomLa.Common.DataConverter.CLng(Unew.Rows[0]["Pronum"]);
            Sll.AddStock(SData);
        }

        string str = "StateLogistics=1";
        if (!string.IsNullOrEmpty(Email))
        {
            str += ",ExpressDelivery='" + Email + "'";
        }
        oll.UpOrderinfo(str, id);
        PromotionComfirm(orlist);
    }
    /// <summary>
    /// 如果是推广商品就添加推广信息
    /// </summary>
    /// <param name="orlist"></param>
    public void PromotionComfirm(M_OrderList orlist)
    {
        B_ArticlePromotion bap = new B_ArticlePromotion();
        B_CartPro cpl = new B_CartPro();
        DataTable mcp;
        mcp = cpl.GetCartProOrderID(orlist.id);
        if (mcp != null && mcp.Rows.Count > 0)
        {
            if (orlist.Settle == 1)
            {
                for (int i = 0; i < mcp.Rows.Count; i++)
                {
                    Response.Write(mcp.Rows[i]["id"].ToString());
                    M_ArticlePromotion map = bap.GetSelectBySqlParams("select * from ZL_ArticlePromotion where cartproid=" + mcp.Rows[i]["id"].ToString(), null);
                    if (map.Id > 0)
                    {
                        map.IsEnable = true;
                        bap.GetUpdate(map);
                    }
                }
            }
        }
    }
}

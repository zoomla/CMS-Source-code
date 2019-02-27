using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

//支付成功后跳转回该页面
public partial class Plugins_Domain_DomReg2 : System.Web.UI.Page
{
    //订单
    protected B_OrderList OCl = new B_OrderList();
    protected M_OrderList Odata = new M_OrderList();
    //购物车
    protected B_CartPro bcart = new B_CartPro();
    
    protected B_IDC_DomainLog dlogBll = new B_IDC_DomainLog();
    protected B_IDC_DomainList listBll = new B_IDC_DomainList();
    protected string orderNo,result;
    protected string clientID, apiPasswd;
    //将TempValue转为HashMap
    protected void Page_Load(object sender, EventArgs e)
    {
       orderNo=Request.QueryString["OrderNo"];
       if (!IsPaySuccess(orderNo, ref result)) { function.WriteErrMsg(result); }

       clientID = StationGroup.newNetClientID;
       apiPasswd = StationGroup.newNetApiPasswd;
       if (!IsPostBack) 
       {
           RegisterDomain(GetInfoByOrderCode(orderNo));
       }

    }
    //检测是否存在该订单，是否支付成功
    public bool IsPaySuccess(string orderNo,ref string result) 
    {
        bool flag = false;
        DataTable dt=GetInfoByOrderCode(orderNo);
        if (dt == null || dt.Rows.Count < 1)
            result = "提示:订单不存在";
        else if (OrderHelper.IsHasPayed(dt.Rows[0]))//orderstatus在处理完后再加1吧
            result = "提示:订单未付款";
        else if (Convert.ToInt32(dt.Rows[0]["orderstatus"]) == (int)M_OrderList.StatusEnum.OrderFinish)
            result = "提示:已处理完成的订单,请勿重复访问";
        else if (Convert.ToInt32(dt.Rows[0]["ordertype"]) != 5)
            result = "提示:不是域名订单";
        else//检测成功
        {
            flag = true;
        }
        return flag;
    }

    //支付成功后,根据订单号,获取订单中的商品详情
    public DataTable GetInfoByOrderCode(string orderNo)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("orderNo", orderNo) };
        return SqlHelper.ExecuteTable(CommandType.Text, "Select a.id as oid,a.paymentstatus,a.userid,a.orderstatus,a.ordertype,b.* From ZL_OrderInfo as a  Left Join ZL_CartPro as b on a.id=b.orderlistid Where OrderNo=@orderNo", sp);
    }
    //注册,并写入日志
    public void RegisterDomain(DataTable dt)
    {
        DataTable resultDT = new DataTable();
        resultDT.Columns.Add(new DataColumn("Index", typeof(int)));
        resultDT.Columns.Add(new DataColumn("DomName", typeof(string)));
        resultDT.Columns.Add(new DataColumn("Year", typeof(string)));
        resultDT.Columns.Add(new DataColumn("Money", typeof(double)));
        resultDT.Columns.Add(new DataColumn("Result", typeof(string)));
        double money = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Dictionary<string,string>dic= DomNameHelper.ConvertToHashMap(dt.Rows[i]["Attribute"].ToString());
            string url = dt.Rows[i]["ProName"].ToString();
            string uname1 = dic["uname1"];
            string uname2 = dic["uname2"];
            string ucity1 = dic["prvinceDP"] + dic["cityText"];
            string aemail = dic["aemail"];
            string checksum = DomNameHelper.MD5("Register" + clientID + apiPasswd + url + aemail + uname2, 32);//以32位
            List<QueryParam> param = new List<QueryParam>();
            param.Add(new QueryParam("dn", url));//域名
            param.Add(new QueryParam("enc", "E"));
            param.Add(new QueryParam("client", clientID));
            param.Add(new QueryParam("period",dt.Rows[i]["ProNum"].ToString()));//1-10年,不填默认1年
            //param.Add(new QueryParam("checksum", checksum));//***MD5加密摘要,,
            //英文必须有空格
            //----注册信信息
            param.Add(new QueryParam("uname1", uname1));//注册人|单位名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uname2", uname2));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname1", dic["rname1"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname2", dic["rname2"]));
            param.Add(new QueryParam("aname1", dic["rname1"]));//管理联系人 中|英名称   [国内域名必填]|[国际域名必须],与上方用同一信息
            param.Add(new QueryParam("aname2", dic["rname2"]));
            param.Add(new QueryParam("aemail", aemail));//管理联系人电子邮件地址                [必须]
            param.Add(new QueryParam("ucity1", ucity1));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("ucity2", "Cheng shi"));
            param.Add(new QueryParam("uaddr1", dic["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uaddr2", dic["uaddr2"]));//dic["uaddr2"]
            param.Add(new QueryParam("uzip", dic["uzip"]));//注册人邮政编码                    [必须]
            param.Add(new QueryParam("uteln", dic["uteln"]));//注册人电话号码
            param.Add(new QueryParam("ateln",""));//
            param.Add(new QueryParam("ufaxa", ""));  //传真区号 dic["ufaxa"] 
            param.Add(new QueryParam("ufaxn", ""));//不能超过8位,与API的不能超过12位不同 dic["ufaxn"]

            //param.Add(new QueryParam("dns1", StationGroup.DnsOption.Split(',')[0]));//自定义主DNS，辅DNS
            //param.Add(new QueryParam("dns2", StationGroup.DnsOption.Split(',')[1]));

            DomNameHelper _XinNet = new DomNameHelper(ApiType.Register, param);

            //汇总前台显示数据
            DataRow dr = resultDT.NewRow();
            dr["Index"] = i;
            dr["DomName"] = url;
            dr["Year"] = dt.Rows[i]["ProNum"].ToString();
            dr["Money"] = dt.Rows[i]["AllMoney"];
            dr["Result"] = _XinNet.IsSuccess() ? "注册成功" : "失败:" + _XinNet.HashtableResult["err"];
            resultDT.Rows.Add(dr);
            money += Convert.ToDouble(dr["Money"]);

            //更新订单状态，写入日志，写入DomainList表
            listBll.Insert(url, DateTime.Now, DateTime.Now, Convert.ToInt32(dt.Rows[i]["userid"]), dt.Rows[i]["Attribute"].ToString(), Convert.ToInt32(dr["Year"]));
            if (_XinNet.IsSuccess())
            {
                OCl.UpOrderinfo("OrderStatus=99", Convert.ToInt32(dt.Rows[i]["oid"]));//成功的订单
            }
            else
            {
                OCl.UpOrderinfo("OrderStatus=98", Convert.ToInt32(dt.Rows[i]["oid"]));//支付成功，但未成功续费，注册的订单
            }
            dlogBll.Insert(dt.Rows[i]["ID"].ToString(), url, B_IDC_DomainLog.OPType.BuyDomain, dt.Rows[i]["userid"].ToString(), dr["Result"].ToString());
        }
        allMoneyL.Text = money.ToString();
        finalRepeater.DataSource = resultDT;
        finalRepeater.DataBind();
    }
}
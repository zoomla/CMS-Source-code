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
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
using ZoomLa.BLL.Content;
using System.Linq;
using System.Collections.Generic;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;

public partial class ZoomLa_WebSite_Manages_Worktable : CustomerPageAction
{
    B_Node nodeBll = new B_Node();
    B_Content_ScheTask scheBll = new B_Content_ScheTask();
    B_User buser = new B_User();
    B_PayPlat payBll = new B_PayPlat();
    B_OrderList orderBll = new B_OrderList();
    OrderCommon orderCom = new OrderCommon();
    B_ModelField modelBll = new B_ModelField();
    public string data1, data2, data3_1, data3_2;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        if (!IsPostBack)
        {
            //内容,商品,会员

            //只显示有数据的第一级父节点
            DataTable condt = nodeBll.SelForShowAll(0);
            condt.DefaultView.RowFilter = "ItemCount>0 AND NodeBySite<1 AND NodeType>0";
            condt = condt.DefaultView.ToTable();
            condt = condt.DefaultView.ToTable(false, "ItemCount", "NodeName");
            data1 = JsonConvert.SerializeObject(condt);
            //商品
            //DataTable prodt = SqlHelper.JoinQuery("A.*,B.NodeName", "(SELECT COUNT(ID) AS ICount,NodeID FROM ZL_Commodities Group BY NodeID)", "ZL_Node", "A.NodeID=B.NodeID");
            //data2 = JsonConvert.SerializeObject(prodt);
            //商品,数量与销量
            string prosql = "SELECT Pro.*,Cart.SellCount FROM "
                          + " (SELECT A.*,B.NodeName FROM (SELECT COUNT(ID) AS ProCount,Nodeid FROM ZL_Commodities GROUP BY Nodeid) A LEFT JOIN ZL_Node B ON A.Nodeid=B.NodeID WHERE B.NodeName IS NOT NULL) Pro"
                          + " LEFT JOIN"
                          + " (SELECT COUNT(A.ID) AS SellCount,B.Nodeid FROM ZL_CartPro A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID GROUP BY B.Nodeid) Cart"
                          + " ON Pro.Nodeid=Cart.Nodeid";
            DataTable prodt = SqlHelper.ExecuteTable(prosql);
            DataRow dr = prodt.NewRow();
            dr["NodeName"] = "总计";
            dr["ProCount"] = prodt.Compute("SUM(ProCount)", "");
            dr["SellCount"] = prodt.Compute("SUM(SellCount)", "");
            prodt.Rows.Add(dr);
            data2 = JsonConvert.SerializeObject(prodt);
            //内芯
            DataTable userdt1 = SqlHelper.ExecuteTable("SELECT COUNT(UserID) AS count1,(SELECT COUNT(UserID) FROM ZL_User Where ParentUserID>0) AS count2 FROM ZL_User");
            data3_1 = JsonConvert.SerializeObject(userdt1);
            DataTable userdt2 = SqlHelper.JoinQuery("A.*,B.GroupName", "(SELECT COUNT(UserID) AS ICount,GroupID FROM ZL_User Group BY GroupID)", "ZL_Group", "A.GroupID=B.GroupID");
            data3_2 = JsonConvert.SerializeObject(userdt2);
            this.litUserName.Text = B_Admin.GetLogin().AdminName;
            this.litDate.Text = DateTime.Now.ToShortDateString() + " " + Resources.L.农历 + Season.GetLunarCalendar(DateTime.Now) + " " + Season.GetWeekCHA() + " " + Season.ChineseTwentyFourDay(DateTime.Now);
            this.Version.Text = "当前版本：CMS2 X3.6 版，数据引擎：SQL Server 2005及更高版本";
            //支付明细
            DataTable paydt = modelBll.SelectTableName("ZL_Payment", "PayPlatID>0 AND PaymentNum like 'DD%' ");
            var payquery = (from pay in paydt.AsEnumerable() select pay).Take(5);
            payquery.Count();
            Pay_RPT.DataSource = payquery.Count() > 0 ? payquery.CopyToDataTable() : null;
            Pay_RPT.DataBind();
            //订单列表
            DataTable orderdt = orderBll.SearchByQuickAndSkey("0,4", "", "", 0, 0, "");
            var query = (from order in orderdt.AsEnumerable() select order).Take(5);
            Order_RPT.DataSource = query.Count() > 0 ? query.CopyToDataTable() : null;
            Order_RPT.DataBind();
            Sche_RPT.DataSource = new B_Content_ScheLog().Sel(5);
            Sche_RPT.DataBind();
        }
    }
    public string GetExecuteType()
    {
        return scheBll.GetExecuteType(Convert.ToInt32(Eval("ExecuteType")));
    }
    public string GetResult()
    {
        if (Eval("Result", "").Equals("1")) { return ComRE.Icon_OK; }
        else { return ComRE.Icon_Error; }
    }
    public string formatzt(string zt, string selects)
    {
        string result = "";
        int status = DataConverter.CLng(zt);
        int type = DataConverter.CLng(selects);
        switch (type)
        {
            case 0:
                result = OrderHelper.GetOrderStatus(status);
                break;
            case 1:
                result = OrderHelper.GetPayStatus(status);
                break;
            case 2:
                result = OrderHelper.GetExpStatus(status);
                break;
            default:
                result = "未知请求";
                break;
        }
        return result;
    }
    protected string getusername(string userid)
    {
        M_UserInfo uinfo = buser.GetUserByUserID(DataConverter.CLng(userid));
        return uinfo.UserName;
    }
    protected string getPayPlat(string id)
    {
        string result = "";
        M_PayPlat payMod = payBll.GetPayPlatByid(DataConverter.CLng(id));
        if (payMod != null)
            result = payMod.PayPlatName;
        return result;
    }
}
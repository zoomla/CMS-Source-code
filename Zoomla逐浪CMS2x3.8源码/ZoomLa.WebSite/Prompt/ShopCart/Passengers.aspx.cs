using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
/*
 * 新购物流程,用于提交旅游,机票订单
 * 使用:
 * 1,前台页面填充信息,或购买商品,后进入此页,添加联系方式
 * 2,提交此页入CartInfo.aspx进行购买
 * 提交:
 * 支持提交表单或IDS的方式
 */ 
public partial class Prompt_ShopCart_Passengers : System.Web.UI.Page
{
    B_Product proBll = new B_Product();
    B_Cart cartBll = new B_Cart();
    B_User buser = new B_User();
    //提交的商品表单信息Json
    public string Pros
    {
        get
        {
            if (ViewState["Pros"] == null)
            {
                ViewState["Pros"] = Request["Pros"]; 
            }
            return ViewState["Pros"].ToString();
        }
        set { ViewState["Pros"] = value; }
    }
    //旅游商品IDS
    public string ids
    {
        get
        {
            if (string.IsNullOrEmpty(IDS_Hid.Value))
            {
                IDS_Hid.Value = Request.QueryString["IDS"];
            }
            return IDS_Hid.Value;
        }
        set { IDS_Hid.Value = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.CheckLogin())
        {
            function.Script(this, "AjaxLogin();");
        }
        if (!IsPostBack)
        {
            Form.Action = "/Cart/CartInfo.aspx?SType=2";
            MyBind();
        }
    }
    private void MyBind() 
    {
        int guestnum = 0, totalnum = 0;
        if (!string.IsNullOrEmpty(ids))
        {
            M_Cart cartMod = cartBll.SelReturnModel(DataConvert.CLng(ids.Split(',')[0]));
            M_Cart_Travel traveMod = JsonConvert.DeserializeObject<M_Cart_Travel>(cartMod.Additional);
            Pros = JsonConvert.SerializeObject(traveMod.ProList);
            Guest_Hid2.Value = JsonConvert.SerializeObject(traveMod.Guest);
            Contract_Hid2.Value = JsonConvert.SerializeObject(traveMod.Contract);
            guestnum = traveMod.Guest.Count;
        }
        //绑定显示商品
        if (!string.IsNullOrEmpty(Pros))
        {
            JArray proArr = (JArray)JsonConvert.DeserializeObject(Pros);
            DataTable proDT = CreateProDT();//仅用于展示,不参与逻辑
            double proAllMoney = 0;
            for (int i = 0; i < proArr.Count; i++)
            {
                DataRow proDR = proDT.NewRow();
                M_Product proMod = proBll.GetproductByid(Convert.ToInt32(proArr[i]["ProID"].ToString()));
                switch ((M_Product.ClassType)proMod.ProClass)
                {
                    case M_Product.ClassType.LY:
                    case M_Product.ClassType.JD:
                        break;
                    default:
                        function.WriteErrMsg("[" + proMod.Proname + "]商品类型不正确");
                        break;
                }
                if (proMod.ID > 0)
                {
                    proDR["ID"] = proMod.ID;
                    proDR["ProName"] = proMod.Proname;
                    proDR["Pronum"] = proArr[i]["Pronum"].ToString();
                    //----如果传递了编号,则读取多价格信息
                    double price = proMod.LinPrice;
                    DataRow priceDR = proBll.GetPriceByCode(proArr[i]["code"], proMod.Wholesalesinfo, ref price);
                    proDR["Price"] = price;
                    if (priceDR != null) { proDR["ProName"] += "(" + priceDR["Proname"] + ")"; }
                    //----
                    proDR["AllMoney"] = Convert.ToInt32(proDR["Pronum"]) * Convert.ToDouble(proDR["Price"]);
                    proDR["GoDate"] = DataConvert.CDate(proArr[i]["GoDate"]);
                    proDR["Remind"] = HttpUtility.UrlDecode(proArr[i]["Remind"].ToString());
                    proDT.Rows.Add(proDR);
                    proAllMoney += Convert.ToDouble(proDR["AllMoney"]);
                    totalnum += Convert.ToInt32(proDR["Pronum"]);
                }
            }
            AllMoney_sp.InnerText = proAllMoney.ToString("0.00");
            ProList_RPT.DataSource = proDT;
            ProList_RPT.DataBind();
            function.Script(this, "AddGuests(" + (string.IsNullOrEmpty(ids) ? totalnum : guestnum) + ");");
        }
    }
    //----------------Tools
    //创建格式
    private DataTable CreateProDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("ProName", typeof(string));
        dt.Columns.Add("Pronum", typeof(int));
        dt.Columns.Add("Price", typeof(double));
        dt.Columns.Add("AllMoney", typeof(double));
        dt.Columns.Add("GoDate", typeof(DateTime));//出发时间
        dt.Columns.Add("Remind", typeof(string));
        return dt;
    }
}
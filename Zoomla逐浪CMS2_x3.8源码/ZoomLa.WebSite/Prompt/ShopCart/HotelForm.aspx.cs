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
 * 用法同旅游订单,仅用于酒店
 */ 
public partial class Prompt_ShopCart_HotelForm : System.Web.UI.Page
{
    B_Product proBll = new B_Product();
    B_Cart cartBll = new B_Cart();
    B_User buser = new B_User();
    //商品模型列表
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
            Form.Action = "/Cart/CartInfo.aspx?SType=1";
            MyBind();
        }
    }
    public DataTable CreateProDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProID", typeof(int));
        dt.Columns.Add("ProName", typeof(string));
        dt.Columns.Add("Pronum", typeof(int));
        dt.Columns.Add("Price", typeof(double));
        dt.Columns.Add("AllMoney", typeof(double));
        dt.Columns.Add("GoDate", typeof(DateTime));
        dt.Columns.Add("OutDate", typeof(DateTime));
        dt.Columns.Add("Remind", typeof(string));
        return dt;
    }
    private void MyBind()
    {
        int guestnum = 0, totalnum = 0;
        if (!string.IsNullOrEmpty(ids))
        {
            M_Cart cartMod = cartBll.SelReturnModel(DataConvert.CLng(ids.Split(',')[0]));
            M_Cart_Hotel hotelMod = JsonConvert.DeserializeObject<M_Cart_Hotel>(cartMod.Additional);
            Pros = JsonConvert.SerializeObject(hotelMod.ProList);
            Guest_Hid2.Value = JsonConvert.SerializeObject(hotelMod.Guest);
            Contract_Hid2.Value = JsonConvert.SerializeObject(hotelMod.Contract);
            guestnum = hotelMod.Guest.Count;
        }
        //绑定显示商品
        if (!string.IsNullOrEmpty(Pros))
        {
            JArray proArr = (JArray)JsonConvert.DeserializeObject(Pros);//将其转化为DataTable?
            DataTable proDT = CreateProDT();
            double proAllMoney = 0;
            for (int i = 0; i < proArr.Count; i++)
            {
               
                M_Product proMod = proBll.GetproductByid(Convert.ToInt32(proArr[i]["ProID"].ToString()));
                if (proMod.ID > 0)
                {
                    for (int j = 0; j < DataConvert.CLng(proArr[i]["Pronum"].ToString()); j++)
                    {
                        DataRow proDR = proDT.NewRow();
                        proDR["ProID"] = proMod.ID;
                        proDR["ProName"] = proMod.Proname;
                        proDR["Pronum"] = GetDiffDay(DataConvert.CDate(proArr[i]["GoDate"].ToString()), DataConvert.CDate(proArr[i]["OutDate"].ToString()));
                        proDR["Price"] = proMod.LinPrice;
                        proDR["AllMoney"] = GetDiffDay(DataConvert.CDate(proArr[i]["GoDate"].ToString()), DataConvert.CDate(proArr[i]["OutDate"].ToString())) * Convert.ToDouble(proDR["Price"]);
                        proDR["GoDate"] = DataConvert.CDate(proArr[i]["GoDate"]);
                        proDR["OutDate"] = proArr[i]["OutDate"].ToString();
                        proDR["Remind"] = HttpUtility.UrlDecode(proArr[i]["Remind"].ToString());
                        proDT.Rows.Add(proDR);
                        proAllMoney += Convert.ToDouble(proDR["AllMoney"]);
                        totalnum += 2;
                    }
                }
            }
            AllMoney_sp.InnerText = proAllMoney.ToString("0.00");
            ProList_RPT.DataSource = proDT;
            ProList_RPT.DataBind();
            Pros = JsonHelper.JsonSerialDataTable(proDT);
            function.Script(this, "AddGuests(" + (string.IsNullOrEmpty(ids) ? totalnum : guestnum) + ");");
        }
    }
    public int GetDiffDay(DateTime sdate, DateTime edate)
    {
        int hour = 12;
        TimeSpan diffdate = edate - sdate;
        int day = diffdate.Days-1;
        if (sdate.Hour <= hour)//入住时间小于12点则算一天
            day++;
        if (edate.Hour >= hour)//离店时间大于12点则算一天
            day++;
        return day;
    }
}
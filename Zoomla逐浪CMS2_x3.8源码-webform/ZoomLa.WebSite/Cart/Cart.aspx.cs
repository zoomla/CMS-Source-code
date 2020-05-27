using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data.SqlClient;
using ZoomLa.Components;
using Newtonsoft.Json;
using ZoomLa.BLL.Shop;

/*
 * 购物车页面根据所传来的ItemID,SkuID,PCount将指定商品加入购物车,按店铺分栏显示
 * 自营商品的店铺StoreID为0
 */
public partial class test_Cart : System.Web.UI.Page
{
    B_OrderBaseField fieldBll = new B_OrderBaseField();
    B_Product proBll = new B_Product();
    B_Cart cartBll = new B_Cart();
    B_User buser = new B_User();
    B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
    OrderCommon orderCom = new OrderCommon();
    //仅用于标识显示积分商品,或普通商品,不参与其他逻辑
    public int ProClass { get { return DataConvert.CLng(Request.QueryString["Proclass"]); } }
    //Cookies中的购物车ID
    public string CartCookID
    {
        get
        {
            return B_Cart.GetCartID();
        }
    }
    //用户注册时的区域
    private string Region { get { return ViewState["Region"] as string; } set { ViewState["Region"] = value; } }
    private DataTable CartDT
    {
        get
        {
            return (DataTable)ViewState["CartDT"];
        }
        set { ViewState["CartDT"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo mu = GetLogin();
        if (!IsPostBack)
        {
            ProModel model = new ProModel() {ProID = DataConvert.CLng(Request["ID"]), Pronum = DataConvert.CLng(Request["Pronum"]) };
            model.Pronum = model.Pronum < 1 ? 1 : model.Pronum;
            if (model.ProID > 0)//将商品加入购物车
            {
                M_Product proMod = proBll.GetproductByid(model.ProID);
                if (proMod == null || proMod.ID < 1) { function.WriteErrMsg("商品不存在"); }
                if (proMod.ProClass == (int)M_Product.ClassType.IDC) { Response.Redirect("/Cart/FillIDCInfo.aspx?proid=" + proMod.ID); }
                OrderCommon.ProductCheck(proMod);
                //-----------------检测完成加入购物车
                M_Cart cartMod = new M_Cart();
                cartMod.Cartid = CartCookID;
                cartMod.StoreID = proMod.UserShopID;
                cartMod.ProID = model.ProID;
                cartMod.Pronum = model.Pronum;
                cartMod.userid = mu.UserID;
                cartMod.Username = mu.UserName;
                cartMod.Getip = EnviorHelper.GetUserIP();
                cartMod.Addtime = DateTime.Now;
                cartMod.FarePrice = proMod.LinPrice.ToString();
                cartMod.Proname = proMod.Proname;
                int id = cartBll.AddModel(cartMod);
                ImportExtField(id,Request.Form["ext_hid"]);//模型字段
                Response.Redirect(Request.Url.AbsolutePath + "?ProClass=" + proMod.ProClass);
            }
            MyBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        CartDT = null;
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        CartDT = cartBll.SelByCartID(CartCookID, mu.UserID, ProClass);//从数据库中获取
        RPT.DataSource = orderCom.SelStoreDT(CartDT);
        RPT.DataBind();
        totalmoney.InnerText = TotalPrice.ToString("f2");
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            Repeater rpt = e.Item.FindControl("ProRPT") as Repeater;
            CartDT.DefaultView.RowFilter = "StoreID=" + dr["ID"];
            DataTable dt = CartDT.DefaultView.ToTable();
            if (dt.Rows.Count < 1) { e.Item.Visible = false; }
            rpt.DataSource = dt;
            rpt.DataBind();
        }
    }
    protected void ProRPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "del":
                int id = Convert.ToInt32(e.CommandArgument);
                cartBll.DelByID(CartCookID, id);
                break;
        }
        MyBind();
    }
    //将计算出的单价缓存,用于避免重复计算
    private double TempPrice = 0;
    private double TotalPrice = 0;
    //单项商品小计
    public string GetPrice()
    {
        int pronum = Convert.ToInt32(Eval("Pronum"));
        double total = TempPrice * pronum;
        TotalPrice += total;
        return total.ToString("0.00");
    }
    //单价
    public string GetMyPrice()
    {
        int proID = Convert.ToInt32(Eval("ProID"));
        double linPrice = Convert.ToDouble(Eval("LinPrice"));
        M_UserInfo mu = buser.GetLogin();
        //多区域价格
        if (string.IsNullOrEmpty(Region))
        {
            Region = buser.GetRegion(mu.UserID);
        }
        TempPrice = regionBll.GetRegionPrice(proID, linPrice, Region, mu.GroupID);
        //string price = proBll.P_GetByUserType(proBll.GetproductByid(proID, buser.GetLogin()).ToString("f2");

        string html = "<i class='fa fa-rmb'></i><span id='price_" + Eval("ID") + "'>" + TempPrice.ToString("f2") + "</span>";
        //if (orderCom.HasPrice(Eval("LinPrice_Json")))
        //{
        //    string json = DataConvert.CStr(Eval("LinPrice_Json"));
        //    M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(json);
        //    if (priceMod.Purse > 0)
        //    {
        //        html += "余额:<span id='price_purse_" + Eval("ID") + "'>" + priceMod.Purse.ToString("f2") + "</span>";
        //    }
        //    if (priceMod.Sicon > 0)
        //    {
        //        html += "|银币:<span id='price_sicon_" + Eval("ID") + "'>" + priceMod.Sicon.ToString("f0") + "</span>";
        //    }
        //    if (priceMod.Point > 0)
        //    {
        //        html += "|积分:<span id='price_point_" + Eval("ID") + "'>" + priceMod.Point.ToString("f0") + "</span>";
        //    }
        //}
        return html;
    }
    //------------------------------Tools
    // 生成购物车编号("Shopby OrderNo"的value) 
    //返回当前登录用户,如未登录,则返回0
    private M_UserInfo GetLogin()
    {
        M_UserInfo mu = buser.GetLogin(false);
        if (mu == null)
        {
            mu = new M_UserInfo();
            mu.UserID = 0;
            mu.UserName = "未登录";
        }
        return mu;
    }
    //批量删除
    protected void BatDel_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["prochk"];
        if (string.IsNullOrEmpty(ids))
        { function.Script(this, "alert('未选择商品');"); }
        else
        {
            cartBll.U_DelByIDS(ids,buser.GetLogin().UserID);
            MyBind();
        }
    }
    //结算,到订单页再生成AllMoney
    protected void NextStep_Click(object sender, EventArgs e)
    {
        //AJAX就先检测一遍,未登录则弹窗
        B_User.CheckIsLogged(Request.RawUrl);
        string ids = Request.Form["prochk"];
        Response.Redirect("GetOrderInfo.aspx?ids=" + ids + "&ProClass=" + ProClass);//"#none"
    }
    //-----后期整合
    public string GetShopUrl()
    {
        return orderCom.GetShopUrl(DataConvert.CLng(Eval("StoreID")), Convert.ToInt32(Eval("ProID")));
    }
    public string GetImgUrl(object o)
    {
        return function.GetImgUrl(o);
    }
    public string GetStockStatus()
    {
        if (Eval("Allowed").ToString().Equals("0"))
        {
            int pronum = Convert.ToInt32(Eval("Pronum"));
            int stock = Convert.ToInt32(Eval("Stock"));
            if (stock <pronum )
            {
                return "<span class='r_red_x'>缺货</span>";
            }
        }
        return "<span class='r_green_x'>有货</span>";
    }
    //根据用户提交的Json数据,提交用户表单,后台调用
    public void ImportExtField(int id, string json)
    {
        DataTable cartFieldDT = fieldBll.Select_Type(1);
        if (!string.IsNullOrEmpty(json) && cartFieldDT.Rows.Count > 0)
        {
            string sql = "Update ZL_Cart Set ", fieldsql = "";
            SqlParameter[] sp = new SqlParameter[cartFieldDT.Rows.Count];
            for (int i = 0; i < cartFieldDT.Rows.Count; i++)
            {
                DataRow dr = cartFieldDT.Rows[i];
                string field = dr["FieldName"].ToString();
                string vname = "@val" + i;
                string value = JsonHelper.GetVal(json, field);
                if (string.IsNullOrEmpty(value)) continue;
                sp[i] = new SqlParameter(vname, value);
                fieldsql += field + "=" + vname + ",";
            }
            fieldsql = fieldsql.TrimEnd(',');
            sql = sql + fieldsql + " Where ID=" + id;
            if (!string.IsNullOrEmpty(fieldsql))
                SqlHelper.ExecuteSql(sql, sp);
        }
    }
}
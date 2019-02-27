using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

/*
 * $不提供修改功能
 */ 

public partial class User_Order_AddShare : System.Web.UI.Page
{
    B_CartPro cartProBll = new B_CartPro();
    B_Order_Share shareBll = new B_Order_Share();
    B_OrderList orderBll = new B_OrderList();
    OrderCommon orderCom = new OrderCommon();
    B_User buser = new B_User();
    ////订单与商品ID
    public int OrderID { get { return DataConverter.CLng(Request.QueryString["OrderID"]); } }
    //public int ProID { get { return DataConverter.CLng(Request.QueryString["ProID"]); } }
    // int CartID { get { return DataConverter.CLng(Request.QueryString["CartID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu=buser.GetLogin();
        M_OrderList orderMod = orderBll.SelReturnModel(OrderID);
        if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("你无权访问该订单"); }
        if (orderMod.OrderStatus < (int)M_OrderList.StatusEnum.OrderFinish) { function.WriteErrMsg("订单未完结,不允许评价"); }
        DataTable dt = cartProBll.SelForRPT(OrderID, "comment");
        RPT.DataSource = dt;
        RPT.DataBind();
        if (dt.Rows.Count == 1) { Save_Btn.CommandName = "go"; Save_Btn.Text = "评价并跳转"; }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        int cartid = DataConverter.CLng(Request.Form["cart_rad"]);
        //购买时间与商品信息也需要写入
        M_UserInfo mu = buser.GetLogin();
        M_CartPro cartProMod = cartProBll.SelReturnModel(cartid);
        cartProMod.AddStatus = StrHelper.AddToIDS(cartProMod.AddStatus, "comment");//换为枚举
        M_Order_Share shareMod = new M_Order_Share();
        shareMod.Title = Title_T.Text;
        shareMod.MsgContent = MsgContent_T.Text;
        shareMod.UserID = mu.UserID;
        shareMod.IsAnonymous = IsHideName.Checked ? 1 : 0;
        shareMod.Score = DataConverter.CLng(star_hid.Value);
        if (shareMod.Score > 5) { shareMod.Score = 5; }
        shareMod.Imgs = Attach_Hid.Value;
        shareMod.Labels = "";
        shareMod.OrderID = cartProMod.Orderlistid;
        shareMod.ProID = cartProMod.ProID;
        shareMod.OrderDate = cartProMod.Addtime;
        shareBll.Insert(shareMod);
        cartProBll.UpdateByID(cartProMod);
        switch (Save_Btn.CommandName)
        {
            case "go":
                function.WriteSuccessMsg("评价成功,将跳转至商品页", orderCom.GetShopUrl(cartProMod.StoreID,cartProMod.ProID));//返回商品页,或对应的商品页
                break;
            default:
                function.WriteSuccessMsg("评价成功");
                break;
        }
    }
    public string GetImg() 
    {
        return function.GetImgUrl(Eval("Thumbnails"));
    }
    public string GetShopUrl()
    {
        return orderCom.GetShopUrl(DataConverter.CLng(Eval("StoreID")), Convert.ToInt32(Eval("ProID")));
    }
}
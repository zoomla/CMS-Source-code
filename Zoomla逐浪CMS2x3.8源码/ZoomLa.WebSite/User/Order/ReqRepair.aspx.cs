using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;

public partial class User_Order_ReqRepair : System.Web.UI.Page
{
    B_Order_Repair repairBll = new B_Order_Repair();
    B_CartPro cartBll = new B_CartPro();
    B_Product proBll = new B_Product();
    B_OrderList orderBll = new B_OrderList();
    B_User buser = new B_User();

    public int Cid { get { return DataConverter.CLng(Request.QueryString["cid"]); } }

    public int RepairID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }

    public void MyBind()
    {
        M_CartPro cartMod = new M_CartPro();
        M_OrderList orderMod = new M_OrderList();
        M_Product proMod = new M_Product();
        if (RepairID > 0)//修改
        {
            M_Order_Repair repairMod = repairBll.SelReturnModel(RepairID);
            proMod = proBll.GetproductByid(repairMod.ProID);
            cartMod = cartBll.SelReturnModel(repairMod.CartID);
            take_hid.Value = repairMod.TakeProCounty;
            reurn_hid.Value = repairMod.ReProCounty;
            TakeAddress_T.Text = repairMod.TakeProAddress ;
            ReqAddRess_T.Text = repairMod.ReProAddress;
            UserName_T.Text = repairMod.UserName;
            Phone_T.Text = repairMod.Phone;
            Attach_Hid.Value = repairMod.ProImgs.Trim('|');
            ServicesType_Hid.Value = repairMod.ServiceType.ToString();
            ReProType_Hid.Value = repairMod.ReProType.ToString();
            Save_Btn.Visible = false;
            IsReadOnly_Hid.Value = "1";
        }
        else//申请
        {
            cartMod = cartBll.SelReturnModel(Cid);
            proMod = proBll.GetproductByid(cartMod.ProID);
            orderMod = orderBll.GetOrderListByid(cartMod.Orderlistid);
            take_hid.Value = orderMod.Shengfen;
            reurn_hid.Value = orderMod.Shengfen;
            TakeAddress_T.Text = orderMod.Jiedao;
            ReqAddRess_T.Text = orderMod.Jiedao;
            UserName_T.Text = orderMod.Rename;
            Phone_T.Text = orderMod.MobileNum;
        }
        //该商品支持的服务类型
        string[] services = proMod.GuessXML.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries);
        if (services.Length == 0) { function.WriteErrMsg("该商品不能返修或退货!","OrderList.aspx"); }
        for (int i = 0; i < services.Length; i++)
        {
            switch (services[i])
            {
                case "drawback":
                    ServiceType_Li.Text += "<div data-value='drawback' class='type_div'>退货</div>";
                    break;
                case "exchange":
                    ServiceType_Li.Text += "<div data-value='exchange' class='type_div'>换货</div>";
                    break;
                case "repair":
                    ServiceType_Li.Text += "<div data-value='repair' class='type_div'>维修</div>";
                    break;
                default:
                    function.WriteErrMsg("该商品不能返修或退货!", "OrderList.aspx");
                    break;
            }
        }
        ServicesType_Hid.Value = services[0];
        ProImgSrc.Src = "/" + proMod.Thumbnails;
        ProName_L.Text = proMod.Proname;
        ProNum_Li.Text = cartMod.Pronum.ToString();
        ProPrice_L.Text = proMod.LinPrice.ToString();
        ProDate.Text = cartMod.Addtime.ToString();
        DateTime takedate = DateTime.Now.Hour > 19 ? DateTime.Now.AddDays(1) : DateTime.Now;
        TakeDate_T.Text = takedate.ToString("yyyy-MM-dd  15:00-19:00");
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Order_Repair repairMod = new M_Order_Repair();
        repairMod.ProNum = DataConverter.CLng(Num_T.Text);
        repairMod.Deailt = Deatil_T.Text;
        repairMod.CretType = Request.Form["cret"];
        repairMod.ReProType = DataConverter.CLng(ReProType_Hid.Value);
        repairMod.TakeProCounty = Request.Form["province_dp"] + " " + Request.Form["city_dp"] + " " + Request.Form["county_dp"];
        repairMod.TakeProAddress = TakeAddress_T.Text;
        repairMod.ReProCounty = Request.Form["reprovince_dp"] + " " + Request.Form["recity_dp"] + " " + Request.Form["recounty_dp"];
        repairMod.ReProAddress = ReqAddRess_T.Text;
        repairMod.UserName = UserName_T.Text;
        repairMod.Phone = Phone_T.Text;
        repairMod.ProImgs = Attach_Hid.Value;
        M_CartPro cartMod = cartBll.SelReturnModel(Cid);
        repairMod.ProID = cartMod.ProID;
        repairMod.OrderNO = orderBll.SelReturnModel(cartMod.Orderlistid).OrderNo;
        repairMod.ServiceType =ServicesType_Hid.Value;
        repairMod.UserID = buser.GetLogin().UserID;
        repairMod.CartID = Cid;
        repairBll.Insert(repairMod);
        cartMod.AddStatus = StrHelper.AddToIDS(cartMod.AddStatus, ServicesType_Hid.Value);
        cartBll.UpdateByID(cartMod);
        function.WriteSuccessMsg("添加成功!","OrderList.aspx");
    }
}
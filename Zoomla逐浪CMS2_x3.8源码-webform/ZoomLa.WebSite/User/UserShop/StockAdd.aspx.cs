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
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_UserShop_StockAdd : System.Web.UI.Page
{
    //库存不可修改,只能另外添加库存冲单
    B_Stock stockBll = new B_Stock();
    B_Product proBll=new B_Product();
    B_Content conBll=new B_Content();
    B_User buser = new B_User();
    public int ProID { get { return DataConverter.CLng(Request.QueryString["ProID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            M_CommonData storeMod = conBll.SelMyStore_Ex();
            RV1.MinimumValue = Convert.ToString(Int32.MinValue);
            RV1.MaximumValue = Convert.ToString(Int32.MaxValue);
            M_Product proMod = GetProByID(mu);
            ProName_L.Text = proMod.Proname;
            danjuhao_T.Text = storeMod.GeneralID + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore_Ex();
        M_Product proMod = GetProByID(mu);
        M_Stock stockMod = new M_Stock();
        stockMod.proid = ProID;
        stockMod.proname = proMod.Proname;
        stockMod.Pronum = DataConverter.CLng(Pronum.Text);
        stockMod.stocktype = DataConverter.CLng(Request.Form["stocktype_rad"]);
        string code = storeMod.GeneralID + DateTime.Now.ToString("yyyyMMddHHmmss");
        stockMod.danju = (stockMod.stocktype == 1 ? "RK" : "CK") + code;
        stockMod.UserID = mu.UserID;
        stockMod.adduser = mu.UserName;
        stockMod.StoreID = storeMod.GeneralID;
        if (stockMod.Pronum < 1) { function.WriteErrMsg("出入库数量不能小于1"); }
        switch (stockMod.stocktype)
        {
            case 0:
                proMod.Stock += stockMod.Pronum;
                break;
            case 1:
                proMod.Stock -= stockMod.Pronum;
                break;
            default:
                throw new Exception("出入库操作错误");
        }
        stockBll.insert(stockMod);
        proBll.updateinfo(proMod);
        function.WriteSuccessMsg("库存操作成功", "StockList.aspx");
    }
    private M_Product GetProByID(M_UserInfo mu)
    {
        M_Product proMod = proBll.GetproductByid(ProID);
        if (proMod == null) { function.WriteErrMsg("商品不存在"); }
        if (proMod.UserID != mu.UserID) { function.WriteErrMsg("你无权操作该商品库存"); }
        return proMod;
    }
}

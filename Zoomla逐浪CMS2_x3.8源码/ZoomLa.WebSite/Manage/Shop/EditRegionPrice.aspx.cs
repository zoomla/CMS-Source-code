using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model.Shop;
//用于修改单个区域价格
public partial class test_EditRegionPrice : System.Web.UI.Page
{
    B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
    B_Group gpBll = new B_Group();
    private string Guid { get { return Request.QueryString["guid"]; } }
    //地区信息,必须完全匹配
    private string Region { get { return Request.QueryString["region"]; } }
    private DataTable PriceDT { get { return ViewState["PriceDT"] as DataTable; } set { ViewState["PriceDT"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Guid)) { function.WriteErrMsg("未指定商品信息"); }
            if (string.IsNullOrEmpty(Region)) { function.WriteErrMsg("未指定区域"); }
            MyBind();
            Call.HideBread(Master);
        }
    }
    private void MyBind()
    {
        M_Shop_RegionPrice regionMod = regionBll.SelModelByGuid(Guid);
        if (regionMod == null) { function.WriteErrMsg("无匹配的区域价格"); }
        //ja regionDT = JsonConvert.DeserializeObject<DataTable>(dt.Rows[0]["Info"].ToString());
        List<M_RegionPrice_Price> list = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(regionMod.Info);
        M_RegionPrice_Price model = list.FirstOrDefault(p => p.region.Equals(Region));
        Region_L.Text = model.region;
        PriceDT = model.price;
        Group_RPT.DataSource = gpBll.Sel();
        Group_RPT.DataBind();
    }
    public double GetPrice()
    {
        int gid = Convert.ToInt32(Eval("GroupID"));
        return regionBll.GetPrice(PriceDT, 0, gid);
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Shop_RegionPrice regionMod = regionBll.SelModelByGuid(Guid);
        List<M_RegionPrice_Price> list = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(regionMod.Info);
        M_RegionPrice_Price model = list.FirstOrDefault(p => p.region.Equals(Region));
        model.price = JsonConvert.DeserializeObject<DataTable>(Price_Hid.Value);
        regionMod.Info = JsonConvert.SerializeObject(list);
        regionBll.UpdateByID(regionMod);
        function.Script(this, "parent.region.fill(" + regionMod.Info + ");parent.CloseDiag();");
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model.Shop;

public partial class test_RegionalPrice : System.Web.UI.Page
{
    M_Shop_RegionPrice regionMod = new M_Shop_RegionPrice();
    B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
    B_Group groupBll = new B_Group();
    public string Guid { get { return Request.QueryString["guid"]; } }
    public string Region { get { return HttpUtility.UrlDecode(Request.QueryString["region"] ?? ""); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request["action"];
            M_APIResult retMod = new M_APIResult();
            retMod.retcode = M_APIResult.Failed;
            switch (action)
            {
                case "del":
                    regionMod = regionBll.SelModelByGuid(Guid);
                    regionBll.P_Remove(regionMod, Region);
                    retMod.retcode = regionBll.UpdateByID(regionMod) ? M_APIResult.Success : M_APIResult.Failed;
                    break;
            }
            Response.Write(retMod.ToString()); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            MyBind();
            Call.HideBread(Master);
        }
    }
    private void MyBind() 
    {
        Group_RPT.DataSource = groupBll.Sel();
        Group_RPT.DataBind();
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        regionMod = regionBll.SelModelByGuid(Guid);
        if (regionMod == null) { regionMod = new M_Shop_RegionPrice(); }
        regionMod.Remind = "";
        regionMod.Info = UpdatePrice(regionMod.Info, Save_Hid.Value);
        if (regionMod.ID > 0)
        {
            regionBll.UpdateByID(regionMod);
        }
        else
        {
            regionMod.Guid = Guid; regionBll.Insert(regionMod);
        }
        function.Script(this, "parent.region.fill(" + regionMod.Info + ");parent.CloseDiag();");
    }
    //如果存在,则无则添加,有则更新
    private string UpdatePrice(string info, string save)
    {
        //无数据则不需要更新
        if (string.IsNullOrEmpty(info)) { return save; }
        if (string.IsNullOrEmpty(save)) { return info; }
        List<M_RegionPrice_Price> infoList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(info);//原有数据
        List<M_RegionPrice_Price> saveList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(save);//需要更新或新加入的数据
        for (int i = 0; i < saveList.Count; i++)
        {
            var saveMod = saveList[i];
            var infoMod = infoList.FirstOrDefault(p => p.region.Equals(saveMod.region));
            if (infoMod == null) { infoList.Add(saveMod); }
            else {
                infoMod.price = saveMod.price;
            }
        }
        return JsonConvert.SerializeObject(infoList);
    }
}
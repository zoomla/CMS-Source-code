using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class Manage_I_Guest_SetCateOrder : System.Web.UI.Page
{
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public int CatePid
    {
        get
        {
            return DataConverter.CLng(Request.QueryString["id"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result = "0";
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            switch (action)
            {
                case "UpdateOrder"://mid1:oid2,mid2:oid1
                    if (value.Split(',').Length > 1)
                    {
                        string id1 = value.Split(',')[0];
                        string id2 = value.Split(',')[1];
                        cateBll.SwitchOrderID(id1, id2);
                        result = "1";
                    }
                    break;
                default:
                    break;
            }
            Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li classs='active'>栏目排序管理</li>");
            MyBind();
        }
    }
    void MyBind()
    {
        RepCate_rp.DataSource = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar,CatePid);
        RepCate_rp.DataBind();
    }
    protected void SaveOrder_B_Click(object sender, EventArgs e)
    {
        string[] ids = changeids.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (ids.Length>0)
        {
            string[] orderIds = changeorders.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ids.Length; i++)
            {
                M_GuestBookCate CateMod = cateBll.SelReturnModel(Convert.ToInt32(ids[i]));
                CateMod.OrderID = Convert.ToInt32(orderIds[i]);
                cateBll.UpdateByID(CateMod);
            }
            MyBind();
        }
        function.Script(this, "Refresh();");
    }
}
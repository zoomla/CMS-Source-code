using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;

public partial class User_UserShop_Classmanage : System.Web.UI.Page
{
    //B_UserShopClass cll = new B_UserShopClass();
    B_User ull = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ull.GetLogin().State != 2)
        {
            function.WriteErrMsg("您未通过商业会员认证，添加证书并通过审核后才能进行自由分类");
            return;
        }

        M_UserInfo info = ull.GetLogin();
        if (Request.QueryString["type"] == "0")
        {
            Page.Title = "我的店铺";
        }
        if (Request.QueryString["type"] == "1")
        {
            Page.Title = "内容管理";
        }
        if (Request.QueryString["menu"] != null)
        {
            string menu = Request.QueryString["menu"];
            if (menu == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                //cll.DeleteByGroupID(id);
            }
        }
        //DataTable tas = cll.SelectByUserName(ull.GetLogin().UserName);
        //tas.DefaultView.Sort = "Orderid desc";
        //Page_list(tas);

    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        RPT.DataSource = Cll;
        RPT.DataBind();
    }
    #endregion 
}

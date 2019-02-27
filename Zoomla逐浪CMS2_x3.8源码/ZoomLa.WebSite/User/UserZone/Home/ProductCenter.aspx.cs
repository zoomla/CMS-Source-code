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
using System.Collections.Generic;
using FHBLL;
using FHModel;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_UserZone_Home_ProductCenter : Page
{
    #region 调用业务逻辑
    ProductTableBLL ptbll = new ProductTableBLL();
    B_User ubll = new B_User();
    #endregion
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        if (!IsPostBack)
        {
            ViewState["ptid"] = Request.QueryString["ptid"];
            GetInit();
        }
    }
    #region 页面方法

    protected string uname;
    private Guid ptid
    {
        get
        {
            if (ViewState["ptid"] != null)
                return new Guid(ViewState["ptid"].ToString());
            else
                return Guid.Empty;
        }
        set
        {
            ViewState["ptid"] = value;
        }
    }

    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> dt = new Dictionary<string, string>();
            dt.Add("Addtime", "0");
            return dt;
        }
    }

    private void GetInit()
    {
        uname = SiteConfig.ShopConfig.Unit;
        M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
        //绑定商品
        List<ProductTable> list2 = ptbll.GetProductTableBytID(Guid.Empty, null);
        DataList2.DataSource = list2;
        DataList2.DataBind();


    }
    protected string getpic(string pic)
    {
        return SiteConfig.SiteOption.UploadDir + pic;
    }

    #endregion
}

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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

namespace ZoomLaCMS.Store
{
    public partial class ProductShow : System.Web.UI.Page
    {
        B_Product upbll = new B_Product();
        B_User bubll = new B_User();
        B_CreateHtml bll = new B_CreateHtml();
        B_Content cbll = new B_Content();
        B_StoreStyleTable sstbll = new B_StoreStyleTable();
        B_CreateShopHtml sll = new B_CreateShopHtml();

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["storeid"] = Request.QueryString["storeid"];
            GetInit();
        }

        #region 页面方法
        private int sid
        {
            get
            {
                if (ViewState["storeid"] != null)
                    return int.Parse(ViewState["storeid"].ToString());
                else
                    return 0;
            }
            set
            {
                sid = value;
            }
        }
        //初始化
        private void GetInit()
        {
            M_Product up = upbll.GetproductByid(sid);
        }
        #endregion
    }
}
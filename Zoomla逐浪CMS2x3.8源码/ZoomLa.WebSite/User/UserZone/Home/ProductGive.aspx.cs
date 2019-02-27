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
using FHModel;
using FHBLL;
using System.Collections.Generic;
using FreeHome.common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

namespace FreeHome.Shop
{
    public partial class ProductGive :Page
    {

        #region 调用业务逻辑
        ProductTableBLL ptbll = new ProductTableBLL();
        B_User ubll = new B_User();
        #endregion
        int currentUser = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = ubll.GetLogin().UserID;
            if (currentUser == 0)
                Response.Redirect(@"~/user/Login.aspx");
            if (!IsPostBack)
            {
                ViewState["pid"] = Request.QueryString["pID"];
                GetInit();
            }
        }
        #region 页面调用方法

        private Guid pid
        {
            get
            {
                if (ViewState["pid"] != null)
                    return new Guid(ViewState["pid"].ToString());
                else
                    return Guid.Empty;
            }
            set
            {
                ViewState["pid"] = value;
            }
        }

        //初始化
        private void GetInit()
        {
            //读取商品信息
            ProductTable pt = ptbll.GetPtByID(pid);
            this.PicImage.ImageUrl = SiteConfig.SiteOption.UploadDir + pt.ProductPic;
            this.Namelabel.Text = pt.ProductName;
            this.pricelabel.Text = pt.Price + SiteConfig.ShopConfig.Unit + "币";


        }

        protected void BuyBtn_Click(object sender, EventArgs e)
        {
            M_UserInfo ui = ubll.GetUserByUserID(currentUser);
            ProductTable pt = ptbll.GetPtByID(pid);
            if (ui.Purse < pt.Price)
            {
                System.Web.HttpContext.Current.Response.Write(@"<script>alert('余额不足,购买失败');window.parent.hidePopWin(true);</script>");
            }
        }
        #endregion
    }
}

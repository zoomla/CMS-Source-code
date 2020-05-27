using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model.Chat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS._3D
{
    public partial class AddShop : System.Web.UI.Page
    {
        private B_3DShop shopBll = new B_3DShop();
        private M_3DShop shopMod = new M_3DShop();
        //private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "ProductManage.aspx'>商城管理</a></li><li><a href='3DManage.aspx'>3D店铺管理</a></li><li>店铺管理</li>");
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    DPBind();
                }
                else//修改
                {
                    shopMod = shopBll.SelReturnModel(Convert.ToInt32(Request.QueryString["ID"]));
                    DPBind(shopMod);
                    shopName_T.Text = shopMod.ShopName;
                    shopImg_T.Text = shopMod.ShopImg;
                    posX_T.Text = shopMod.posX;
                    posY_T.Text = shopMod.posY;
                }
            }
        }
        private void DPBind(M_3DShop shopMod = null)
        {
            user_DP.DataSource = DBCenter.Sel("ZL_User");
            user_DP.DataTextField = "UserName";
            user_DP.DataValueField = "UserID";
            user_DP.DataBind();
            if (shopMod != null)
            {
                user_DP.SelectedValue = shopMod.UserID.ToString();
            }
        }
        protected void sure_Btn_Click(object sender, EventArgs e)
        {
            bool isUpdate = false;
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                shopMod = shopBll.SelReturnModel(Convert.ToInt32(Request.QueryString["ID"]));
                isUpdate = true;
            }
            shopMod.UserID = Convert.ToInt32(user_DP.SelectedItem.Value);
            shopMod.UserName = user_DP.SelectedItem.Text;
            shopMod.ShopName = shopName_T.Text.Trim();
            shopMod.ShopImg = shopImg_T.Text.Trim();
            shopMod.posX = posX_T.Text.Trim();
            shopMod.posY = posY_T.Text.Trim();
            shopMod.ShopStatus = 1;
            shopMod.Remind = "";
            if (isUpdate)
            {
                shopBll.UpdateByID(shopMod);
            }
            else
            {
                shopBll.Insert(shopMod);
            }
            Response.Redirect("3DManage.aspx");
        }
    }
}
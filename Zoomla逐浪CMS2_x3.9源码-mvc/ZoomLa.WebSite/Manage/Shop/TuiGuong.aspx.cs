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
using ZoomLa.SQLDAL;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class TuiGuong :CustomerPageAction
    {
        protected B_MtrlsMrktng bmm = new B_MtrlsMrktng();
        protected B_Product bp = new B_Product();
        protected B_User bu = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='PresentProject.aspx'>促销优惠</a></li><li><a href='MtrlsMrktng.aspx'>商品推广</a></li>");
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_MtrlsMrktng mp = bmm.GetSelect(id);
            this.ShopID.Text = mp.ShopID.ToString();
            this.ShopName.Text = GetShopName(mp.ShopID.ToString()).ToString();
            this.ShopPrice.Text = GetPrice(mp.ShopID.ToString()).ToString();
            this.lblNum.Text = Getprope(mp.ShopID.ToString()).ToString();
            this.lblgu.Text = Getstock(mp.ShopID.ToString()).ToString();
            this.txtJiexi.Text = Request.Url.ToString();
            this.ExWorker.Text = GetUserName(mp.UserID.ToString()).ToString();
        }
        #region 读取商品名
        public string GetShopName(string studentname)
        {
            int id = DataConverter.CLng(studentname);
            M_Product muser = bp.GetproductByid(id);
            if (muser != null && muser.ID > 0)
            {
                return muser.Proname;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 读取商品价格
        public string GetPrice(string course)
        {
            int id = DataConverter.CLng(course);
            M_Product ms = bp.GetproductByid(id);
            if (ms != null && ms.ID > 0)
            {
                return DataConverter.CDouble(ms.ShiPrice).ToString();
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region 读取User
        public string GetUserName(string Name)
        {
            int id = DataConverter.CLng(Name);
            M_UserInfo muin = bu.GetUserByUserID(id);
            if (muin != null && muin.UserID > 0)
            {
                return muin.UserName;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 读取库存
        public string Getstock(string image)
        {
            int id = DataConverter.CLng(image);
            M_Product mp = bp.GetproductByid(id);
            if (mp != null && mp.ID > 0)
            {
                return mp.Stock.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 数量
        public string Getprope(string prope)
        {
            int id = DataConverter.CLng(prope);
            M_Product mp = bp.GetproductByid(id);
            if (mp != null && mp.ID > 0)
            {
                return mp.Propeid.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
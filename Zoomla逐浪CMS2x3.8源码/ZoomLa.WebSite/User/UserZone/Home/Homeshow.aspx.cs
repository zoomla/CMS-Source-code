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

namespace FreeHome.Home
{
    public partial class Homeshow : System.Web.UI.Page
    {
        #region 调用逻辑
        ProductTableBLL ptbll = new ProductTableBLL();
        HomeSetBLL hsbll = new HomeSetBLL();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ViewState["shid"] = Request.QueryString["shid"];
                GetInit();
            }
        }
        private int showid
        {
            get
            {
                if (ViewState["shid"] != null)
                    return int.Parse(ViewState["shid"].ToString());
                else
                    return 0;
            }
            set
            {
                showid = value;
            }
        }

        private void GetInit()
        {
            List<HomeCollocate> list = hsbll.GetHomeProductByUserID(showid);
            HomeHeadCollocate hhc = hsbll.GetHomeSetHeadByUserID(showid);
            if (hhc.UserID !=showid)
            {
                hhc.UserID = showid;
                hhc.UserHeadPic = "~/HomeImage/b_1.gif";
                hhc.UserIndexZ = 0;
                hhc.UserLeft = 0;
                hhc.UserTop = 0;
                hhc.CohabitID = Guid.Empty;
                hhc.CohabitHeadPic = "";
                hhc.CohabitIndexZ = 0;
                hhc.CohabitLeft = 0;
                hhc.CohabitTop = 0;
                hsbll.InsertUserHead(hhc);
                GetInit();
            }

            string message = null;

            message += "<table width=\"568\" height=\"500\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td background=\"HomeImage/BEDROOM_001.gif\"></td></tr></table>";
            #region 用户头像
            //用户头像
            message += "<DIV id=DIV_user style=\"Z-INDEX:" + hhc.UserIndexZ + "; LEFT: " + hhc.UserLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hhc.UserTop + "px; ; HEIGHT: 115px\"><img src=\"" + hhc.UserHeadPic.ToString().Substring(2) + "\" runat=\"server\" ID=\"userhead\"> </DIV>";
            #endregion

            #region 同居头像
            //同居头像
            if (hhc.CohabitID != Guid.Empty)
            {
                message += "<DIV id=DIV_friend style=\"Z-INDEX:" + hhc.CohabitIndexZ + "; LEFT: " + hhc.CohabitLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hhc.CohabitTop + "px; ; HEIGHT: 115px\"><img src=\".." + hhc.CohabitHeadPic.ToString().Substring(1) + "\" runat=\"server\" ID=\"userhead\" /> </DIV>";
            }
            #endregion

            #region 购买商品图片
            foreach (HomeCollocate hc in list)
            {

                message += "<DIV id=DIV_GIFT_" + hc.ID.ToString().Replace("-", "") + " style=\"Z-INDEX:" + hc.CIndexZ + "; LEFT: " + hc.CLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hc.CTop + "px; ; HEIGHT: 115px\"><img src=\"../../../" + hc.ProductPic.ToString() + "\"   > </DIV>";

            }

            #endregion

            this.messageLabel.Text = message;
        }
    }
}

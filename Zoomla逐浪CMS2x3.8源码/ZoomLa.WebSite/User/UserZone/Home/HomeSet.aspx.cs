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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;


namespace FreeHome.Home
{
    public partial class HomeSet : Page
    {
        
        #region 调用逻辑
        ProductTableBLL ptbll = new ProductTableBLL();
        HomeSetBLL hsbll = new HomeSetBLL();
        B_User ubll = new B_User();
        #endregion
        B_User buser = new B_User();
        int currentUser = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = buser.GetLogin().UserID;
            if (!IsPostBack)
            {

                GetInit();
                GetUserProduct();
            }
        }
        #region 页面调用方法



        //初始化小屋图片
        private void GetInit()
        {
            List<HomeCollocate> list = hsbll.GetHomeProductByUserID(currentUser);
            HomeHeadCollocate hhc = hsbll.GetHomeSetHeadByUserID(currentUser);

            #region 初始化
            string message=null;

            #region 初始化JS函数
            //初始化JS函数
            message = "<script type=\"text/javascript\">\n ";
            message += "function hidemenuie5()\n";
            message += "{\n";
            message += "ie5menu.style.visibility=\"hidden\";\n";
            message += "ie5menu2.style.visibility=\"hidden\";\n";
            foreach (HomeCollocate hc in list)
            {
                message += "ie5menu" + hc.ID.ToString().Replace("-","") + ".style.visibility='hidden';\n";
            }
            message += "}\n";
            message += "</script>\n";
            #endregion

            #region 活得移动后的位置
            //活得移动后的位置
            message += "<script type=\"text/javascript\">\n";
            message += " function SavePos()\n";
            message += "{\n";
            message += "RoomForm.user_left.value=document.all.DIV_user.style.posLeft;\n";
            message += "RoomForm.user_top.value=document.all.DIV_user.style.posTop;\n";
            foreach (HomeCollocate hc in list)
            {
                message += "RoomForm.posleft_" + hc.ID.ToString().Replace("-", "") + ".value = document.all.DIV_GIFT_" + hc.ID.ToString().Replace("-", "") + ".style.posLeft;\n";
                message += "RoomForm.postop_" + hc.ID.ToString().Replace("-", "") + ".value = document.all.DIV_GIFT_" + hc.ID.ToString().Replace("-", "") + ".style.posTop;\n";
            }
            message += "RoomForm.friend_left.value=document.all.DIV_friend.style.posLeft;\n";
            message += "RoomForm.friend_top.value=document.all.DIV_friend.style.posTop;\n";
            message += "}\n";
            message += "</script>\n";
            #endregion

            #region 页面Table
            //页面Table
            //message += "<SPAN onmousemove=move_ie()  id=RoomSpan style=\"Z-INDEX: 1; WIDTH: 568px; POSITION: absolute; TOP: 0px; HEIGHT: 600px; left: 1px;\">";

            message += "<table width=\"568\" height=\"500\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td background=\"HomeImage/BEDROOM_001.gif\"></td></tr></table>";
            #endregion

            #region 用户头像
            //用户头像
            message += "<input type=\"hidden\" name=\"user_left\" id=\"user_left\" runat=\"server\" ><input type=\"hidden\" name=\"user_top\" id=\"user_top\" runat=\"server\">";
            message += "<DIV onmouseup=disableMove(); onmousedown=enableMove(this) ; oncontextmenu=\"return showmenu()\"; onMouseOver=img_border(this); onMouseOut=no_img_border(this); id=DIV_user style=\"Z-INDEX:" + hhc.UserIndexZ + "; LEFT: " + hhc.UserLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hhc.UserTop + "px; ; HEIGHT: 115px\"><img src=\"" + hhc.UserHeadPic.ToString().Substring(2) + "\" runat=\"server\" ID=\"userhead\"> </DIV>";
            #endregion

            #region 同居头像
            //同居头像
            if(hhc.CohabitID!=Guid.Empty)
            {
                message += "<input type=\"hidden\" name=\"friend_left\" id=\"friend_left\" runat=\"server\" ><input type=\"hidden\" name=\"friend_top\" id=\"friend_top\" runat=\"server\">";
                message += "<DIV onmouseup=disableMove(); onmousedown=enableMove(this) ; oncontextmenu=\"return showmenu2()\"; onMouseOver=img_border(this); onMouseOut=no_img_border(this); id=DIV_friend style=\"Z-INDEX:" + hhc.CohabitIndexZ + "; LEFT: " + hhc.CohabitLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hhc.CohabitTop + "px; ; HEIGHT: 115px\"><img src=\".." + hhc.CohabitHeadPic.ToString().Substring(1) + "\" runat=\"server\" ID=\"userhead\" /> </DIV>";
            }
            #endregion

            #region 购买商品图片
            foreach (HomeCollocate hc in list)
            {
                message += "<script type=\"text/javascript\">\n";
                message += "function showmenu" + hc.ID.ToString().Replace("-", "") + "() \n";
                message += "{\n";
                message += "var rightedge=event.clientX;\n";
                message += "var bottomedge=event.clientY;\n";
                message += "if (rightedge>480)\n";
                message += "ie5menu" + hc.ID.ToString().Replace("-", "") + ".style.left=480;\n";
                message += "else\n";
                message += "ie5menu" + hc.ID.ToString().Replace("-", "") + ".style.left=rightedge+20;\n";
                message += "if (bottomedge>480)\n";
                message += "ie5menu" + hc.ID.ToString().Replace("-", "") + ".style.top=480;\n";
                message += "else\n";
                message += "ie5menu" + hc.ID.ToString().Replace("-", "") + ".style.top=bottomedge;\n";
                message += "ie5menu" + hc.ID.ToString().Replace("-", "") + ".style.visibility='visible';\n";
                message += "return false;\n";
                message += "}\n";
                message += "</script>";
                message += "<input type=\"hidden\" name=\"posleft_" + hc.ID.ToString().Replace("-", "") + "\" id=\"posleft_" + hc.ID.ToString().Replace("-", "") + "\"><input type=\"hidden\" name=\"postop_" + hc.ID.ToString().Replace("-", "") + "\" id=\"postop_" + hc.ID.ToString().Replace("-", "") + "\">";

                message += "<DIV onmouseup=disableMove(); onmousedown=enableMove(this) ; oncontextmenu=\"return showmenu" + hc.ID.ToString().Replace("-", "") + "()\"; onMouseOver=img_border(this); onMouseOut=no_img_border(this); id=DIV_GIFT_" + hc.ID.ToString().Replace("-", "") + " style=\"Z-INDEX:" + hc.CIndexZ + "; LEFT: " + hc.CLeft + "px; WIDTH: 80px; POSITION: absolute; TOP: " + hc.CTop + "px; ; HEIGHT: 115px\"><img src=\"../../../" + hc.ProductPic.ToString() + "\"   > </DIV>";
                
            }

            #endregion
            //message += "</span>";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";
            //message += "";

            #endregion
            this.messageLabel.Text = message;
        }

        //初始化用户商品
        private void GetUserProduct()
        {
            List<UserShopProduct> list = ptbll.GetUserShop(currentUser);
            EGV.DataSource = list;
            EGV.DataBind();
        }
      
        //放入设置
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            GridViewRow gr = lb.Parent.Parent as GridViewRow;
            if (lb.CommandName == "放入小屋")
            {
                HomeCollocate hc = new HomeCollocate();
                hc.UserID = currentUser;
                hc.ShopID = new Guid(this.EGV.DataKeys[gr.RowIndex].Value.ToString());
                hc.CLeft = 0;
                hc.CTop = 0;

                hsbll.InsertHomeSet(hc);
            }
            else
            {
                hsbll.DelHomeSet(new Guid(this.EGV.DataKeys[gr.RowIndex].Value.ToString()));
            }
            GetInit();
            GetUserProduct();
        }

        
        //商品状态
        protected string GetState(string ID)
        {
            if (hsbll.CheckShopid(new Guid(ID)))
            {
                return "使用中";
            }
            else
            {
                return "没有使用";
            }
        }

        //商品操作
        protected string GetLink(string ID)
        {
            if (hsbll.CheckShopid(new Guid(ID)))
            {
                return "移出小屋";
            }
            else
            {
                return "放入小屋";
            }
        }

        protected void Submit1_ServerClick(object sender, EventArgs e)
        {
            HomeHeadCollocate hcc = hsbll.GetHomeSetHeadByUserID(currentUser);
            hcc.UserID = currentUser;
            hcc.UserLeft = int.Parse(Request.Form["user_left"]);
            hcc.UserTop = int.Parse(Request.Form["user_top"]);
            if (hcc.CohabitID != Guid.Empty)
            {
                hcc.CohabitLeft = int.Parse(Request.Form["friend_left"]);
                hcc.CohabitTop = int.Parse(Request.Form["friend_top"]);
            }
            hsbll.UpdateHead(hcc);

            List<HomeCollocate> list = hsbll.GetHomeProductByUserID(currentUser);

            foreach(HomeCollocate hc in list )
            {
                string left = Request.Form["posleft_" + hc.ID.ToString().Replace("-", "")];
                string top = Request.Form["postop_" + hc.ID.ToString().Replace("-", "")];
                hsbll.UpdateProduct(int.Parse(left), int.Parse(top), hc.ID);
            }
            GetInit();
            GetUserProduct();
        }
        protected string getpic(string pic)
        {
            return "~/" + pic;
        }
        #endregion
    }
}

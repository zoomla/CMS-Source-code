using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using BDUModel;

namespace ZoomLaCMS.Zone
{
    public partial class showfriendsearch : System.Web.UI.Page
    {
        B_User buser = new B_User();
        int currentUser = 0;

        private int UserID
        {
            get
            {
                if (ViewState["UserID"] != null)
                    return int.Parse(ViewState["UserID"].ToString());
                return 0;
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            currentUser = buser.GetLogin().UserID;
            if (!IsPostBack)
            {
                ViewState["UserID"] = Request.QueryString["sID"];
                GetFriendGroup(null);
            }
        }

        //得到好友分组
        private void GetFriendGroup(PagePagination pagepag)
        {

        }

        protected void joinbtn_Click(object sender, EventArgs e)
        {

        }
    }
}
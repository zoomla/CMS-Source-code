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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace FreeHome.Home
{
    public partial class MyHome :Page
    {
        protected int uid;
        B_User ubll = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ubll.CheckIsLogin();
            if(!IsPostBack)
            {
                M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                uid = uinfo.UserID;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Components;

namespace ZoomLaCMS.Mis.OA
{
    public partial class Default : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_Structure strBll = new B_Structure();
        public M_OA_UserConfig ucMod = new M_OA_UserConfig();
        public B_OA_UserConfig ucBll = new B_OA_UserConfig();
        protected B_UserPurview bper = new B_UserPurview();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            string roleid = buser.GetLogin().UserRole;
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_Uinfo mub = new M_Uinfo();
                mub = buser.GetUserBaseByuserid(mu.UserID);
                UName_L.Text = mu.HoneyName;
                ucMod = ucBll.SelModelByUserID(mu.UserID);
                if (ucMod != null)//为空则不检测
                {
                    //leftChk2.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk2");
                    //leftChk3.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk3");
                    //leftChk4.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk4");
                    if (!ucMod.HasAuth(ucMod.LeftChk, "leftChk0"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "hiddendiv1();", true);
                    }
                }
            }
        }
        public string GetUserID(int userID)
        {
            string result = "";
            if (userID < 10)
            {
                result = "00" + userID;
            }
            else if (userID >= 10)
            {
                result = "0" + userID;
            }
            else
            {
                result = userID.ToString();
            }
            return result;
        }
    }
}
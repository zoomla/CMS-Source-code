using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class MIS_OA_Menu_LeftMenu : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_Structure strBll = new B_Structure();
    public M_OA_UserConfig ucMod = new M_OA_UserConfig();
    public B_OA_UserConfig ucBll = new B_OA_UserConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged("/Office");
        //string roleid = buser.GetLogin().UserRole;  
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            userNameL.Text = mu.HoneyName; 
            userGNL.Text = strBll.SelNameByUid(mu.UserID);
            Work_L.Text = mu.WorkNum;
            faceImg.Src = mu.UserFace;
            ucMod = ucBll.SelModelByUserID(mu.UserID);
            if (ucMod != null)//为空则不检测
            {
                calendarDiv.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk1");
                //leftChk2.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk2");
                //leftChk3.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk3");
                //leftChk4.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk4");
                leftChk5.Visible = ucMod.HasAuth(ucMod.LeftChk, "leftChk5");
                if (!ucMod.HasAuth(ucMod.LeftChk, "leftChk0"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "hiddendiv1();", true);
                }
            }
            string ulId = Request.QueryString["leftul"];
            if (!string.IsNullOrEmpty("left_ul_1_1")) 
            { 
                function.Script(this, "$('#" + ulId + "').show();");
            }
        }
              
    }
}
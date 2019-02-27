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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Sns.Model;
using ZoomLa.Sns.BLL;

public partial class User_UserInfo_UserReg_Move6 : Page 
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ut.GetLogin();
            Setinfo();
        }
    }

    private void Setinfo()
    {
        M_Uinfo info = ut.GetUserBaseByuserid(ut.GetLogin().UserID);
        txtAddress.Text = info.Address;
        txtOfficePhone.Text = info.OfficePhone;
        txtHomePhone.Text = info.HomePhone;
        txtFax.Text = info.Fax;
        txtZipCode.Text = info.ZipCode;
        txtPHS.Text = info.PHS;
        txtMobile.Text = info.Mobile;
        txtQQ.Text = info.QQ.Trim ();
        txtMSN.Text = info.MSN;
        txtICQ.Text = info.ICQ;
        txtUC.Text = info.UC.Trim();
        txtYaHoo.Text = info.Yahoo;
        txtHomePage.Text = info.HomePage;
    }

    protected void nextButton_Click(object sender, EventArgs e)
    {
        M_Uinfo info = ut.GetUserBaseByuserid(ut.GetLogin().UserID);
        info.Address = txtAddress.Text;
        info.OfficePhone = txtOfficePhone.Text;
        info.HomePhone = txtHomePhone.Text;
        info.Fax = txtFax.Text;
        info.ZipCode = txtZipCode.Text;
        info.PHS = txtPHS.Text;
        info.Mobile = txtMobile.Text;
        info.QQ = txtQQ.Text;
        info.MSN = txtMSN.Text;
        info.ICQ = txtICQ.Text;
        info.UC = txtUC.Text;
        info.Yahoo = txtYaHoo.Text;
        info.HomePage = txtHomePage.Text;
        bool isnull = IsNull();//判断修改前是否全部填写

        if (ut.UpdateBase(info))
        {
            if (IsNull() && isnull==false)
            {
               
            }
        }
    }


    //判断字符是否为空
    public bool IsNull()
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        if (info.FaceType != null && info.FaceType != "" &&
            info.Facey != null && info.Facey != "" &&
            info.HairType != null && info.HairType != "" &&
            info.JobAnd != null && info.JobAnd != "" &&
            info.Lovepetlife != null && info.Lovepetlife != "" &&
            info.UserAcctive != null && info.UserAcctive != "" &&
            info.UserArea != null && info.UserArea != "" &&
            info.UserAth != null && info.UserAth != "" &&
            info.UserAvoir != null && info.UserAvoir != "" &&
            info.UserBachelor != null && info.UserBachelor != "" &&
            info.UserBir != null && info.UserBir != "" &&
            info.UserBlood != null && info.UserBlood != "" &&
            info.UserBrother != null && info.UserBrother != "" &&
            info.UserCar != null && info.UserCar != "" &&
            info.UserChild != null && info.UserChild != "" &&
            info.UserCity != null && info.UserCity != "" &&
            info.UserCom != null && info.UserCom != "" &&
            info.UserConsortAge != null && info.UserConsortAge != "" &&
            info.UserConsortAvoir != null && info.UserConsortAvoir != "" &&
            info.UserConsortBachelor != null && info.UserConsortBachelor != "" &&
            info.UserConsortHome != null && info.UserConsortHome != "" &&
            info.UserConsortMarry != null && info.UserConsortMarry != "" &&
            info.UserConsortMonthIN != null && info.UserConsortMonthIN != "" &&
            info.UserConsortOther != null && info.UserConsortOther != "" &&
            info.UserConsortStature != null && info.UserConsortStature != "" &&
            info.UserConsortWorkArea != null && info.UserConsortWorkArea != "" &&
            info.UserConstellation != null && info.UserConstellation != "" &&
            info.UserDrink != null && info.UserDrink != "" &&
            info.UserFood != null && info.UserFood != "" &&
            info.UserHeart != null && info.UserHeart != "" &&
            info.UserHome != null && info.UserHome != "" &&
            info.UserLanguage != null && info.UserLanguage != "" &&
            info.UserLife != null && info.UserLife != "" &&
            info.UserLifePic1 != null && info.UserLifePic1 != "" &&
            info.UserLifePic2 != null && info.UserLifePic2 != "" &&
            info.UserLifePic3 != null && info.UserLifePic3 != "" &&
            info.UserLifePic4 != null && info.UserLifePic4 != "" &&
            info.UserLove != null && info.UserLove != "" &&
            info.UserMarry != null && info.UserMarry != "" &&
            info.UserMonthIn != null && info.UserMonthIn != "" &&
            info.UserMovie != null && info.UserMovie != "" &&
            info.UserMusic != null && info.UserMusic != "" &&
            info.UserName != null && info.UserName != "" &&
            info.UserNation != null && info.UserNation != "" &&
            info.UserNeedchild != null && info.UserNeedchild != "" &&
            info.UserPic != null && info.UserPic != "" &&
            info.UserProvince != null && info.UserProvince != "" &&
            info.UserSchool != null && info.UserSchool != "" &&
            info.UserSmoke != null && info.UserSmoke != "" &&
            info.UserSomato != null && info.UserSomato != "" &&
            info.UserStature != null && info.UserStature != "" &&
            info.UserWork != null && info.UserWork != "" &&
            info.Xinyang != null && info.Xinyang != "")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}

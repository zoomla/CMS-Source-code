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
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;
using FreeHome.common;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;
using ZoomLa.Common;

public partial class UserReg_Move2 : Page 
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ut.CheckIsLogin();
        if (!IsPostBack)
        {
            try
            {
                //初始化用户详细信息表
                if (utbll.GetMoreinfoByUserid(ut.GetLogin().UserID).UserID == 0)
                {
                    utbll.AddMoreinfo(ut.GetLogin().UserID);
                }
            }
            catch
            {

            }
            M_UserInfo uinfo = ut.GetLogin(); ;
            GetPage();                
            Setinfo();
        }
    }
    #region 页面调用方法

    private string Xmlurl
    {
        get
        {
            return Server.MapPath(@"~/User/Command/UserRegXml.xml");
        }
        set
        {
            Xmlurl = value;
        }
    }

    private void Setinfo()
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        this.Staturetxt.Text = info.UserConsortStature;
        this.Avoirtxt.Text = info.UserConsortAvoir;
        this.monthDropDownList.SelectedValue = info.UserConsortMonthIN;
        this.BachelorDropDownList.SelectedValue = info.UserConsortBachelor;
        this.homeDropDownList.SelectedValue = info.UserConsortHome;
        this.TextArea1.Value = info.UserConsortOther;
        if (info.UserProvince != null && info.UserProvince!="")
        {
            this.DropDownList3.SelectedValue = info.UserProvince;
            this.DropDownList3_SelectedIndexChanged(null, null);
        }        
        this.DropDownList4.SelectedValue=info.UserCity;
    }
    private void GetPage()
    {
        List<string> list = null;
        ListItem li3 = new ListItem();
        li3.Value = "";
        li3.Text = "不限";
        li3.Selected = true;
        //绑定省
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //DropDownList3.DataSource = list2;
        //DropDownList3.DataTextField = "name";
        //DropDownList3.DataValueField = "code";
        //DropDownList3.DataBind();
        DropDownList3.Items.Add(li3);
        DropDownList3_SelectedIndexChanged(null, null);
        
        //绑定婚姻
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMarry");
        marryDropDownList.DataSource = list;
        marryDropDownList.DataBind();
        marryDropDownList.Items.Add(li3);

        //绑定学历
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserBachelor");
        BachelorDropDownList.DataSource = list;
        BachelorDropDownList.DataBind();
        BachelorDropDownList.Items.Add(li3);

        //绑定月收入
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMonthIn");
        monthDropDownList.DataSource = list;
        monthDropDownList.DataBind();
        monthDropDownList.Items.Add(li3);

        //绑定住房条件
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserHome");
        homeDropDownList.DataSource = list;
        homeDropDownList.DataBind();
        homeDropDownList.Items.Add(li3);

    }

    //绑定城市
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro=DropDownList3.SelectedValue;
        if (pro != "")
        {
            DropDownList4.Visible = true;
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //DropDownList4.DataSource = listc;
            //DropDownList4.DataTextField = "name";
            //DropDownList4.DataValueField = "code";
            //DropDownList4.DataBind();
        }
        else {
            DropDownList4.Visible = false;
            DropDownList4.Items.Clear();
        }
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        UserMoreinfo info1 = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        info1.UserConsortAvoir = this.Avoirtxt.Text;
        info1.UserConsortMarry = this.marryDropDownList.Text;
        info1.UserConsortStature = this.Staturetxt.Text;
        info1.UserConsortMonthIN = this.monthDropDownList.Text;
        info1.UserConsortBachelor = this.BachelorDropDownList.Text;
        info1.UserConsortHome = this.homeDropDownList.Text;
        info1.UserConsortOther = this.TextArea1.Value;
        info1.UserProvince = this.DropDownList3.Text;
        info1.UserCity = this.DropDownList4.Text;
        bool isnull = IsNull();//判断修改前是否全部填写
        if (utbll.UpdateMoreinfo(info1))
        {
            if (IsNull() && isnull==false)
            {
                B_User bu = new B_User();
            }
        }
    }

    //判断字符是否为空
    public bool IsNull()
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        //Response.Write(info.UserIdcard);
        //Response.End();
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
    #endregion
}


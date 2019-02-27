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
using ZoomLa.BLL;
using FreeHome.common;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;

public partial class UserReg_Move5 : Page 
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    //UserFriendBLL ufbll = new UserFriendBLL();
    //HomeSetBLL hsbll = new HomeSetBLL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化用户详细信息表
            if (utbll.GetMoreinfoByUserid(ut.GetLogin().UserID).UserID == 0)
            {
                utbll.AddMoreinfo(ut.GetLogin().UserID);
            }
            M_UserInfo uinfo = ut.GetLogin();
            GetPage();
            SetInfo();
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

    private void SetInfo()
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        SmokeDropDownList.SelectedValue= info.UserSmoke;
        DrinkDropDownList.SelectedValue = info.UserDrink;
        ddlWork.SelectedValue = info.UserWork;
        CarDropDownList.SelectedValue = info.UserCar;
        NeedchildDropDownList.SelectedValue = info.UserNeedchild;
        Comtxt.Text = info.UserCom;
        TextArea1.Value = info.UserHeart;
        Textarea2.Value = info.UserLove;
    }

    private void GetPage()
    {
        List<string> list = null;

        //绑定是否吸烟
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserSmoke");
        SmokeDropDownList.DataSource = list;
        SmokeDropDownList.DataBind();

        //绑定是否喝酒
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserDrink");
        DrinkDropDownList.DataSource = list;
        DrinkDropDownList.DataBind();

        //绑定职业类别
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserWork");
        ddlWork.DataSource = list;
        ddlWork.DataBind();

        //绑定是否购车
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserCar");
        CarDropDownList.DataSource = list;
        CarDropDownList.DataBind();

        //绑定是否想要孩子
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserNeedchild");
        NeedchildDropDownList.DataSource = list;
        NeedchildDropDownList.DataBind();
    }

    protected void nextButton_Click(object sender, EventArgs e)
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        info.UserSmoke = SmokeDropDownList.SelectedItem.Text;
        info.UserDrink = DrinkDropDownList.SelectedItem.Text;
        info.UserWork = ddlWork.SelectedItem.Text;
        info.UserCar = CarDropDownList.SelectedItem.Text;
        info.UserNeedchild = NeedchildDropDownList.SelectedItem.Text ;
        info.UserCom = Comtxt.Text;
        info.UserHeart = TextArea1.Value ;
        info.UserLove = Textarea2.Value ;
        bool isnull = IsNull();//判断修改前是否全部填写
        //try
        //{
        //    UserTemporaryTable utt = new UserTemporaryTable();
        //    foreach (ListItem li in acctiveCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserAcctive += li.Text + ",";
        //        }

        //    }
        //    foreach (ListItem li in AthCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserAth += li.Text + ",";
        //        }

        //    }
        //    foreach (ListItem li in MusicCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserMusic += li.Text + ",";
        //        }

        //    }
        //    foreach (ListItem li in MovieCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserMovie += li.Text + ",";
        //        }

        //    }
        //    foreach (ListItem li in FoodCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserFood += li.Text + ",";
        //        }

        //    }
        //    foreach (ListItem li in AreaCheckBoxList.Items)
        //    {
        //        if (li.Selected)    //表示某一项被选中了
        //        {
        //            utt.UserArea += li.Text + ",";
        //        }

        //    }
        //    utt.UserID = new Guid(Session["TemUserID"].ToString());
        //    utt.UserSmoke = this.SmokeDropDownList.Text;
        //    utt.UserDrink = this.DrinkDropDownList.Text;
        //    utt.UserWork = this.ddlWork.Text ;
        //    utt.UserCar = this.CarDropDownList.Text;
        //    utt.UserCom = this.Comtxt.Text;
        //    utt.UserNeedchild = this.NeedchildDropDownList.Text;
        //    utt.UserHeart = this.TextArea1.Value;
        //    utt.UserLove = this.Textarea2.Value;
        //    utbll.UpdateTem5(utt);
        //    //插入用户表
        //    utbll.InsertUserTable(utt.UserID);
        //    utbll.UpdateState(utt.UserID);
        //    utbll.AddUserLogin(utt.UserID);
        //    //删除临时表
        //    utbll.DelTem(utt.UserID);
        //    //初始化好友分组
        //    FriendGroup uf = new FriendGroup();
        //    uf.HostID = utt.UserID;
        //    uf.GroupName = "默认分组";
        //    ufbll.InsertFriendGroup(uf);
        //    //初始化小屋形象
        //    HomeHeadCollocate hhc = new HomeHeadCollocate();
        //    hhc.UserID=utt.UserID;
        //    if (utbll.GetMoreinfoByUserid(hhc.UserID).UserSex == "男生")
        //    {
        //        hhc.UserHeadPic = @"~\Home\HomeImage\b_1.gif";
        //    }
        //    else
        //    {
        //        hhc.UserHeadPic = @"~\Home\HomeImage\g_1.gif";
        //    }
        //    hhc.UserIndexZ = 10;
        //    hhc.UserLeft = 0;
        //    hhc.UserTop = 0;
        //    hhc.CohabitID = Guid.Empty;
        //    hhc.CohabitIndexZ = 11;
        //    hhc.CohabitLeft = 0;
        //    hhc.CohabitTop = 0;
        //    hhc.CohabitIndexZ = 0;
        //    hsbll.InsertUserHead(hhc);

        //    //保存session
        //    Session["BasepageUserid"] = utt.UserID.ToString();
        //    Page.Response.Redirect(@"~/User/Index.aspx");
        //}
        //catch { }
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
    #endregion
}


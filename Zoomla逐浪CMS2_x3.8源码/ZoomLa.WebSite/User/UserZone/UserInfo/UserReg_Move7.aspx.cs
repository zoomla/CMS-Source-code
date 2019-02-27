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
using FreeHome.common;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;
using System.Text;
using ZoomLa.Common;

public partial class User_UserInfo_UserReg_Move7 : Page 
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
            M_UserInfo uinfo = ut.GetLogin();
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
        Bind(acctiveCheckBoxList, info.UserAcctive);
        Bind(AthCheckBoxList, info.UserAth);
        Bind(MusicCheckBoxList, info.UserMusic);
        Bind(MovieCheckBoxList, info.UserMovie);
        Bind(FoodCheckBoxList, info.UserFood);
        Bind(AreaCheckBoxList, info.UserArea);
        if (info.Lovepetlife != null && info.Lovepetlife!="")
        {
            DataSet ds = new DataSet();
            ds = function.XmlToTable(info.Lovepetlife);
            string love = ds.Tables[0].Rows[0]["Love"].ToString();
            string pet = ds.Tables[0].Rows[0]["Pet"].ToString();
            string lifeCan = ds.Tables[0].Rows[0]["LifeCan"].ToString();
            string self = ds.Tables[0].Rows[0]["Self"].ToString();
            string zuoxi = ds.Tables[0].Rows[0]["Zuoxi"].ToString();
            string duanL = ds.Tables[0].Rows[0]["DuanL"].ToString();
            string liveSamep = ds.Tables[0].Rows[0]["LiveSamep"].ToString();
            Bind(LiveSamep, liveSamep);
            Bind(DuanL,duanL);
            Bind(Zuoxi,zuoxi);
            Bind(Self,self);
            Bind(Love,love);
            Bind(Pet,pet);
            Bind(LifeCan,lifeCan);
        }       
        
    }

    private void Bind(CheckBoxList cbl,string str)
    {
        string[] cblstr = str.Split(',');
        foreach (string s in cblstr)
        {
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Value == s)
                    cbl.Items[i].Selected = true;
            }
        }
    }

    private void Bind(DropDownList cbl, string str)
    {
        cbl.SelectedValue = str;
    }

    private void GetPage()
    {
        List<string> list = null; 
        //绑定个性自评
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserSelf");
        Self.DataSource = list;
        Self.DataBind();
        //绑定兴趣爱好
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserLove");
        Love.DataSource = list;
        Love.DataBind();
        //绑定宠物
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserPet");
        Pet.DataSource = list;
        Pet.DataBind();
        //绑定擅长生活技能
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserLifeCan");
        LifeCan.DataSource = list;
        LifeCan.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserAcctive");
        acctiveCheckBoxList.DataSource = list;
        acctiveCheckBoxList.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserAth");
        AthCheckBoxList.DataSource = list;
        AthCheckBoxList.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMusic");
        MusicCheckBoxList.DataSource = list;
        MusicCheckBoxList.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMovie");
        MovieCheckBoxList.DataSource = list;
        MovieCheckBoxList.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserFood");
        FoodCheckBoxList.DataSource = list;
        FoodCheckBoxList.DataBind();

        //绑定喜欢的活动
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserArea");
        AreaCheckBoxList.DataSource = list;
        AreaCheckBoxList.DataBind();
    }
    
    protected void nextButton_Click(object sender, EventArgs e)
    {
        UserMoreinfo info =utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        string iteminfo = "";
        foreach (ListItem li in acctiveCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                iteminfo += li.Text + ",";
            }

        }
        info.UserAcctive=iteminfo;
        iteminfo = "";
        foreach (ListItem li in AthCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                iteminfo += li.Text + ",";
            }

        }
        info.UserAth =iteminfo ;
        iteminfo = "";
        foreach (ListItem li in MusicCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                    iteminfo += li.Text + ",";
            }

        }
        info.UserMusic = iteminfo;
        iteminfo = "";
        foreach (ListItem li in MovieCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                iteminfo += li.Text + ",";
            }

        }
        info.UserMovie = iteminfo;
        iteminfo = "";
        foreach (ListItem li in FoodCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                iteminfo += li.Text + ",";
            }

        }
        info.UserFood = iteminfo;
        iteminfo = "";
        foreach (ListItem li in AreaCheckBoxList.Items)
        {
            if (li.Selected)    //表示某一项被选中了
            {
                iteminfo += li.Text + ",";
            }

        }            
        info.UserArea = iteminfo;
        iteminfo = "";
        foreach (ListItem li in Love.Items)
        {
            if (li.Selected)
            {
                iteminfo += li.Text + ",";
            }
        }
        string iteminfo1 = "";
        foreach (ListItem li in Pet.Items)
        {
            if (li.Selected)
            {
                iteminfo1 += li.Text + ",";
            }
        }
        string iteminfo2 = "";
        foreach (ListItem li in LifeCan.Items)
        {
            if (li.Selected)
            {
                iteminfo2 += li.Text + ",";
            }
        }

        string iteminfo3 = "";
        foreach (ListItem li in Self.Items)
        {
            if (li.Selected)
            {
                iteminfo3 += li.Text + ",";
            }
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<Love>");
        sb.Append(iteminfo);
        sb.Append("</Love>");
        sb.Append("<Pet>");
        sb.Append(iteminfo1);
        sb.Append("</Pet>");
        sb.Append("<LifeCan>");
        sb.Append(iteminfo2);
        sb.Append("</LifeCan>");
        sb.Append("<Self>");
        sb.Append(iteminfo3);
        sb.Append("</Self>");
        sb.Append("<Zuoxi>");
        sb.Append(Zuoxi.SelectedValue);
        sb.Append("</Zuoxi>");

        sb.Append("<DuanL>");
        sb.Append(DuanL.SelectedValue);
        sb.Append("</DuanL>");

        sb.Append("<LiveSamep>");
        sb.Append(LiveSamep.SelectedValue);
        sb.Append("</LiveSamep>");
           

        string tempstr = "";
        tempstr += "<xml>";
        tempstr += sb.ToString();
        tempstr += "</xml>";

        info.Lovepetlife = tempstr;
        bool isnull = IsNull();//判断修改前是否全部填写
        if (utbll.UpdateMoreinfo(info))
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
    #endregion
}
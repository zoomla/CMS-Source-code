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
using ZoomLa.Model;
using ZoomLa.Components;
using FreeHome.common;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;
using ZoomLa.Common;

public partial class UserReg_Move3 : Page
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
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
        ddlSomato.SelectedValue = info.UserSomato;
        ddlBlood.SelectedValue = info.UserBlood;
        BrotherDropDownList.SelectedValue = info.UserBrother;
        LanguageDropDownList.SelectedValue = info.UserLanguage;
        ddlMarry.SelectedValue = info.UserMarry;
        ddlBachelor.SelectedValue = info.UserBachelor;
        ddlMonth.SelectedValue = info.UserMonthIn;
        ddlHome.SelectedValue = info.UserHome;
        ddlChild.SelectedValue = info.UserChild;
        Nationtxt.SelectedValue = info.UserNation;
        Schooltxt.Text = info.UserSchool;
        //身高只能设置一次，若身高没有设置过此框可以填写，若已经设置就不能填写
        if (info.UserStature != null && info.UserStature != "")
        {
            Staturetxt.Text = info.UserStature;
            Staturetxt.ReadOnly = true;
        }
        else
        {
            Staturetxt.ReadOnly = false;
        }
        Avoirtxt.Text = info.UserAvoir;
        faceType.SelectedValue = info.FaceType;
        hairType.SelectedValue = info.HairType;
        string faceystr = info.Facey.TrimStart(',').TrimEnd(',');
        if (faceystr != "" && faceystr != null)
        {
            string[] facrys = faceystr.Split(',');
            for (int i = 0; i < facrys.Length; i++)
            {
                if (facey.Items.FindByValue(facrys[i]) != null)
                {
                    facey.Items.FindByValue(facrys[i]).Selected = true;
                }
            }
        }
        xinyang.SelectedValue = info.Xinyang;
        Nationtxt.SelectedValue = info.UserNation;
        if (info.JobAnd != null && info.JobAnd != "")
        {
            string jobAnd = info.JobAnd;
            DataSet ds = new DataSet();
            ds = function.XmlToTable(jobAnd);
            Zhuan.SelectedValue = ds.Tables[0].Rows[0]["zhuan"].ToString();
            Hang.SelectedValue = ds.Tables[0].Rows[0]["hang"].ToString();
            JobStatus.SelectedValue = ds.Tables[0].Rows[0]["jobstatus"].ToString();
            JobFuture.SelectedValue = ds.Tables[0].Rows[0]["jobfuture"].ToString();
        }
    }
    private void GetPage()
    {
        List<string> list = null;

        ////绑定省
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //ddlProvince.DataSource = list2;
        //ddlCity.DataTextField = "name";
        //ddlProvince.DataValueField = "code";
        //ddlProvince.DataBind();
        //ddlProvince_SelectedIndexChanged(null, null);

        //绑定所学专业
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserZhuan");
        for (int i = 0; i < list.Count; i++)
        {
            Zhuan.Items.Add(new ListItem(list[i].ToString()));
        }


        //绑定所在行业
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserHang");
        for (int i = 0; i < list.Count; i++)
        {
            Hang.Items.Add(new ListItem(list[i].ToString()));
        }


        //绑定工作状态
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserJobStatus");
        for (int i = 0; i < list.Count; i++)
        {
            JobStatus.Items.Add(new ListItem(list[i].ToString()));
        }


        //绑定工作前景
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserJobFuture");
        for (int i = 0; i < list.Count; i++)
        {
            JobFuture.Items.Add(new ListItem(list[i].ToString()));
        }
        //绑定民族
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserNation");
        for (int i = 0; i < list.Count; i++)
        {
            Nationtxt.Items.Add(new ListItem(list[i].ToString()));
        }

        //绑定宗教
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserXinyang");
        xinyang.DataSource = list;
        xinyang.DataBind();
        //绑定体型
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserSomato");
        ddlSomato.DataSource = list;
        ddlSomato.DataBind();


        //绑定血型
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserBlood");
        ddlBlood.DataSource = list;
        ddlBlood.DataBind();


        //绑定兄弟姐妹
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserBrother");
        BrotherDropDownList.DataSource = list;
        BrotherDropDownList.DataBind();


        //绑定语言能力
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserLanguage");
        LanguageDropDownList.DataSource = list;
        LanguageDropDownList.DataBind();

        //绑定婚姻
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMarry");
        ddlMarry.DataSource = list;
        ddlMarry.DataBind();

        //绑定学历
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserBachelor");
        ddlBachelor.DataSource = list;
        ddlBachelor.DataBind();

        //绑定月收入
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMonthIn");
        ddlMonth.DataSource = list;
        ddlMonth.DataBind();

        //绑定住房条件
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserHome");
        ddlHome.DataSource = list;
        ddlHome.DataBind();

        //绑定有没有孩子
        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserChild");
        ddlChild.DataSource = list;
        ddlChild.DataBind();
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        info.UserSomato = ddlSomato.Text;
        info.UserBlood = ddlBlood.Text;
        info.UserBrother = BrotherDropDownList.Text;
        info.UserLanguage = LanguageDropDownList.Text;
        info.UserMarry = ddlMarry.Text;
        info.UserBachelor = ddlBachelor.Text;
        info.UserMonthIn = ddlMonth.Text;
        info.UserHome = ddlHome.Text;
        info.UserChild = ddlChild.Text;
        info.UserNation = Nationtxt.SelectedValue;
        info.UserSchool = Schooltxt.Text;
        info.UserStature = Staturetxt.Text;
        info.UserAvoir = Avoirtxt.Text;
        string fac = "";
        for (int i = 0; i < facey.Items.Count; i++)
        {
            if (facey.Items[i].Selected)
            {
                fac += facey.Items[i].Value + ",";
            }
        }
        fac = "," + fac;
        info.UserID = ut.GetLogin().UserID;
        info.Facey = fac;
        info.Xinyang = xinyang.SelectedValue;
        info.HairType = hairType.SelectedValue;
        info.FaceType = faceType.SelectedValue;
        info.JobAnd = "<xml><zhuan>" + Zhuan.SelectedValue + "</zhuan><hang>" + Hang.SelectedValue + "</hang><jobstatus>" + JobStatus.SelectedValue + "</jobstatus><jobfuture>" + JobFuture.SelectedValue + "</jobfuture></xml>";
        bool isnull = IsNull();//判断修改前是否全部填写
        if (utbll.UpdateMoreinfo(info))
        {
            if (IsNull() && isnull == false)
            {
                Response.Write("dsfasd");
                Response.End();
                B_User bu = new B_User();
            }
        }
        //try
        //{
        //    UserTemporaryTable utt = new UserTemporaryTable();
        //    utt.UserID = new Guid(Session["TemUserID"].ToString());
        //    utt.UserSomato = this.SomatoDropDownList.Text;
        //    utt.Useddlood = this.bloodDropDownList.Text;
        //    utt.UserProvince = ct.GetprovinceByCode(Server.MapPath(@"~/common/SystemData.xml"), this.DropDownList3.Text.ToString()).Name;
        //    utt.UserCity = ct.GetCityByCode(Server.MapPath(@"~/common/SystemData.xml"), this.DropDownList3.Text.ToString(), this.DropDownList4.Text.ToString()).Name;
        //    utt.UserNation = this.Nationtxt.Text;
        //    utt.UserSchool = this.Schooltxt.Text;
        //    utt.UserBrother = this.BrotherDropDownList.Text;
        //    utt.UserLanguage = this.LanguageDropDownList.Text;
        //    utbll.UpdateTem3(utt);
        //    Page.Response.Redirect("UserReg_Move4.aspx");
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
    //protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string pro = ddlProvince.SelectedValue;
    //    if (pro != "")
    //    {
    //        List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
    //        ddlCity.DataSource = listc;
    //        ddlCity.DataTextField = "name";
    //        ddlCity.DataValueField = "code";
    //        ddlCity.DataBind();
    //    }
    //    else
    //    {
    //        ddlCity.Items.Clear();           
    //    }
    //}
}


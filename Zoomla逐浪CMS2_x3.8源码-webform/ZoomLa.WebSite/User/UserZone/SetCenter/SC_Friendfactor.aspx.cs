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

public partial class User_UserZone_SetCenter_SC_Friendfactor : System.Web.UI.Page
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
        this.Staturetxt.Text = info.UserConsortStature;
        this.Avoirtxt.Text = info.UserConsortAvoir;
        this.monthDropDownList.SelectedValue = info.UserConsortMonthIN;
        this.BachelorDropDownList.SelectedValue = info.UserConsortBachelor;
        this.homeDropDownList.SelectedValue = info.UserConsortHome;
        this.TextArea1.Value = info.UserConsortOther;
        if (info.UserProvince != null && info.UserProvince != "")
        {
            this.DropDownList3.SelectedValue = info.UserProvince;
            this.DropDownList3_SelectedIndexChanged(null, null);
        }
        this.DropDownList4.SelectedValue = info.UserCity;
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
        string pro = DropDownList3.SelectedValue;
        if (pro != "")
        {
            DropDownList4.Visible = true;
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //DropDownList4.DataSource = listc;
            //DropDownList4.DataTextField = "name";
            //DropDownList4.DataValueField = "code";
            //DropDownList4.DataBind();
        }
        else
        {
            DropDownList4.Visible = false;
            DropDownList4.Items.Clear();
        }
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        UserMoreinfo info = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
        info.UserConsortAvoir = this.Avoirtxt.Text;
        info.UserConsortMarry = this.marryDropDownList.Text;
        info.UserConsortStature = this.Staturetxt.Text;
        info.UserConsortMonthIN = this.monthDropDownList.Text;
        info.UserConsortBachelor = this.BachelorDropDownList.Text;
        info.UserConsortHome = this.homeDropDownList.Text;
        info.UserConsortOther = this.TextArea1.Value;
        info.UserProvince = this.DropDownList3.Text;
        info.UserCity = this.DropDownList4.Text;
    }
    #endregion
}

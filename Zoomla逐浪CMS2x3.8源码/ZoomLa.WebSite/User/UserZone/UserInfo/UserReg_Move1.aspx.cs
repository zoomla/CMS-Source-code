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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Collections.Generic;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;


public partial class User_UserInfo_UserReg_Move1 : Page
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    #endregion
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
            M_UserInfo uinfo = ut.GetLogin();
            lblName.Text = uinfo.UserName;
            GetPage();
            M_Uinfo u = ut.GetUserBaseByuserid(uinfo.UserID);
            this.RadioButtonList1.SelectedValue = u.Privating.ToString();//加载隐私状态
            if (u.UserId == 0)
            {
                M_Uinfo uin = new M_Uinfo();
                uin.UserId = uinfo.UserID;
                uin.BirthDay = DateTime.Now.Date.ToString();
                uin.UserSex = true;
                ut.AddBase(uin);
            }
            if (utbll.GetMoreinfoByUserid(uinfo.UserID).UserID == 0)
            {
                utbll.AddMoreinfo(uinfo.UserID);
            }
            SetInfo();
        }
    }

    private void SetInfo()
    {

        M_Uinfo info = ut.GetUserBaseByuserid(ut.GetLogin().UserID);
        if (info.UserSex)
        {
            lblSex.Text = "男生";
        }
        else
        {
            lblSex.Text = "女生";
        }
        txtNum.Text = info.IDCard;
        if (info.BirthDay != null && info.BirthDay != "")
        {
            txtbir.Text = info.BirthDay;
        }
        else
        {
            txtbir.Text = DateTime.Now.Date.ToString();
        }
        ddlProvince.SelectedValue = info.Province;
        ddlProvince_SelectedIndexChanged(null, null);
        ddlCity.SelectedValue = info.County;
        CardType.SelectedValue = info.CardType;
       

    }

    private void GetPage()
    {
        //绑定省
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //ddlProvince.DataSource = list2;
        //ddlProvince.DataTextField = "name";
        //ddlProvince.DataValueField = "code";
        //ddlProvince.DataBind();
        //ddlProvince_SelectedIndexChanged(null, null);
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro = ddlProvince.SelectedValue;
        if (pro != "")
        {
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //ddlCity.DataSource = listc;
            //ddlCity.DataTextField = "name";
            //ddlCity.DataValueField = "code";
            //ddlCity.DataBind();
        }
    }
    protected void nextButton_Click(object sender, EventArgs e)
    {
        M_Uinfo info = ut.GetUserBaseByuserid(ut.GetLogin().UserID);
        info.IDCard = txtNum.Text;
        info.BirthDay = txtbir.Text;
        info.Province = ddlProvince.SelectedValue;
        info.County = ddlCity.SelectedValue;
        info.CardType = CardType.SelectedValue;
        info.HoneyName = lblhoney.Text;
        info.Privating = Convert.ToInt32(this.RadioButtonList1.SelectedValue); //隐私设定
        if (ut.UpdateBase(info))
        {
            UserMoreinfo um = utbll.GetMoreinfoByUserid(ut.GetLogin().UserID);
            if (utbll.UpdateMoreinfo(um))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('信息已经成功提交！');location.href=location.href;", true);
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('提交出错！');location.href=location.href;", true);
        }
        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('提交出错！');location.href=location.href;", true);
    }
    public string GetTree(DateTime dateTime)
    {
        System.Globalization.ChineseLunisolarCalendar chinseCaleander = new System.Globalization.ChineseLunisolarCalendar();
        string TreeYear = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
        int intYear = chinseCaleander.GetSexagenaryYear(dateTime);
        string Tree = TreeYear.Substring(chinseCaleander.GetTerrestrialBranch(intYear) - 1, 1);
        return Tree;
    }

    public string GetConstellation(DateTime birthday)
    {

        float birthdayF = 0.00F;
        string strday;

        if (birthday.Day < 10)
        {
            strday = "0" + birthday.Day.ToString();
        }
        else
        {
            strday = birthday.Day.ToString();
        }

        if (birthday.Month == 1 && birthday.Day < 20)
        {
            birthdayF = float.Parse(string.Format("13.{0}", strday));
        }
        else
        {
            birthdayF = float.Parse(string.Format("{0}.{1:2}", birthday.Month, strday));
        }
        float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
        string[] atoms = { "水瓶座", "双鱼座", "牡羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };
        string ret = "";

        for (int i = 0; i < atomBound.Length - 1; i++)
        {
            if (atomBound[i] <= birthdayF && atomBound[i + 1] > birthdayF)
            {
                ret = atoms[i];
                break;
            }
        }
        return ret;
    }


}

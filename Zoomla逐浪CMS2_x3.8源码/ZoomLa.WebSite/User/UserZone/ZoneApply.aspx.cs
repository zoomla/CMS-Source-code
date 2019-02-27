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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Sns;
using ZoomLa.Model;
using System.Collections.Generic;
using FreeHome.common;
using ZoomLa.Sns.Model;
using ZoomLa.Sns.BLL;
using ZoomLa.Common;

public partial class User_UserZone_ZoneApply : Page
{
    #region 业务对象
    B_User ubll = new B_User();
    blogTableBLL btbll = new blogTableBLL();
    UserTableBLL utbll = new UserTableBLL();
    int currentUser = 0;
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
    #endregion

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetLogin();


            blogTable bt = btbll.GetUserBlog(uinfo.UserID);

            if (bt.UserID <= 0)
            {
                bt.BlogState = 1;
                bt.CommendState = 0;
                bt.UserID = uinfo.UserID;
                bt.BlogName = uinfo.UserName + "的空间";
                //bt.BlogContent = this.textareacontent.Value;
                bt.StyleID = 0;
                btbll.InsertBlog(bt);
            }
            Response.Write("<script>location.href='Default.aspx'</script>");
            //Response.Redirect(ResolveUrl(""));
        }
    }
    #endregion

    #region 页面方法
    private void GetInit()
    {
        

        M_Uinfo info= ubll.GetUserBaseByuserid(currentUser);

        if (!string.IsNullOrEmpty(info.BirthDay))
        {
            txtbir.Text = info.BirthDay;
        }

        //绑定省
       // List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //ddlProvince1.DataSource = list2;
        //ddlProvince1.DataTextField = "name";
        //ddlProvince1.DataValueField = "code";
        //ddlProvince1.DataBind();
        //if (!string.IsNullOrEmpty(info.Province))
        //{
        //    ddlProvince1.SelectedValue = info.Province;
        //}
        //ddlProvince1_SelectedIndexChanged(null, null);

        //绑定血型
        List<string> list = UserRegConfig.GetInitUserReg(Xmlurl, "UserBlood");
        ddlBlood.DataSource = list;
        ddlBlood.DataBind();
        UserMoreinfo uminfo = new UserMoreinfo();
        uminfo = utbll.GetMoreinfoByUserid(currentUser);
        if (!string.IsNullOrEmpty(uminfo.UserBlood))
        {
            ddlBlood.SelectedValue = uminfo.UserBlood; 
        }

        //性别
        if (!string.IsNullOrEmpty(info.UserSex.ToString()))
        {
            this.RadioButtonList1.SelectedValue = info.UserSex.ToString();
        }

        //头像
        if (!string.IsNullOrEmpty(info.UserFace))
        {
            Image1.ImageUrl = info.UserFace;
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {

        UserMoreinfo um = utbll.GetMoreinfoByUserid(ubll.GetLogin().UserID);
        if (um.UserID == 0)
        {
            utbll.AddMoreinfo(ubll.GetLogin().UserID);
        }

        um.UserSex = bool.Parse(this.RadioButtonList1.SelectedValue);
        um.UserProvince = ddlProvince1.SelectedValue;
        um.UserCity = ddlCity1.SelectedValue;
        um.UserBir = txtbir.Text;
        um.UserBlood = ddlBlood.Text;
        um.UserID=currentUser ;
        if (utbll.UpdateMoreinfo(um))
        {
            M_Uinfo muser = ubll.GetUserBaseByuserid(ubll.GetLogin().UserID);
            muser.BirthDay = txtbir.Text;
            muser.Province = ddlProvince1.SelectedValue;
            muser.County = ddlCity1.SelectedValue;
            muser.UserSex =bool.Parse(this.RadioButtonList1.SelectedValue);
            muser.UserFace = HiddenField1.Value ;
            muser.UserId =currentUser ;
            if (ubll.UpdateBase(muser))
            {
                blogTable bt = btbll.GetUserBlog(currentUser);
                bt.BlogState = 0;
                bt.CommendState = 0;
                bt.UserID = currentUser;
                bt.BlogName = this.Nametxt.Text;
                bt.BlogContent = this.textareacontent.Value;
                bt.StyleID = 0;
                btbll.InsertBlog(bt);
                base.Response.Redirect("ZoneEditApply.aspx");
            }
            else
                function.WriteErrMsg("提交出错！");
        }   
        else
            function.WriteErrMsg("提交出错！");
    }

    protected void ddlProvince1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string pro = ddlProvince1.SelectedValue;
        //if (pro != "")
        //{
        //    List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
        //    ddlCity1.DataSource = listc;
        //    ddlCity1.DataTextField = "name";
        //    ddlCity1.DataValueField = "code";
        //    ddlCity1.DataBind();
        //}
    } 
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        //SaveAsFile saf = new SaveAsFile();
        //this.HiddenField1.Value = saf.fileSaveAs(UpImg, false)[1];
        //Image1.ImageUrl = this.HiddenField1.Value;
        //Response.Write(this.HiddenField1.Value);
    }
    #endregion
}

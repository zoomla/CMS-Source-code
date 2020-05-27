using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using System.Globalization;
using System.Data.SqlClient;

public partial class MIS_Mis : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_MisAttendance Battendance = new B_MisAttendance();
    M_MisAttendance Mattendance = new M_MisAttendance();
    B_Mis bll = new B_Mis();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/Mis/");
        if (!IsPostBack)
        {
           
        }
        M_UserInfo info = buser.GetLogin();
        lblUserName.Text = info.UserName;
        M_Uinfo binfo = buser.GetUserBaseByuserid(info.UserID);
        imgUserPic.ImageUrl = getproimg(binfo.UserFace);
        LoginTime.Text = info.LastLoginTimes.ToString();
    }
    protected void CheckisAtt()
    {
        
    }
    public string getproimg(string type)
    {
        string restring = "";
        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {

            string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";

            if (type.IndexOf("uploadfiles") > -1)
            {
                restring = type;
            }
            else if (type.StartsWith("http://", true, CultureInfo.CurrentCulture) || type.StartsWith("/", true, CultureInfo.CurrentCulture) || type.StartsWith(delpath, true, CultureInfo.CurrentCulture) || type.StartsWith("~", true, CultureInfo.CurrentCulture))
                restring = type;
            else
            {
                restring = SiteConfig.SiteOption.UploadDir + "/" + type;
            }
        }
        else
        {
            restring = "/Images/userface/noface.png";
        }
        return restring;
    }
    protected void BtnBegin_Click(object sender, EventArgs e)
    {
        Mattendance.DepartMent = "";
        Mattendance.BeginTime = DateTime.Now.ToString().Replace("/","-");
        Mattendance.UserName = buser.GetLogin().UserName;
        Mattendance.Comment = "";
        Mattendance.BeginStatus = 1;
        Battendance.insert(Mattendance);
        Response.Redirect("MisAttendance.aspx");
    }
    protected void BtnEnd_Click(object sender, EventArgs e)
    {
        
    }
}
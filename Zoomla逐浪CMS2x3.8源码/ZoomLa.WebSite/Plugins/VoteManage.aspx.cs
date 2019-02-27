using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;

public partial class Plugins_VoteManage : System.Web.UI.Page
{

    M_Survey model = new M_Survey();
    B_Survey bll = new B_Survey();
    B_Answer_Recode bans = new B_Answer_Recode();
    DataTable dt = new DataTable();
    DataTable tblAnswers = new DataTable();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
         buser.CheckIsLogin();
         if (!IsPostBack)
         {
             int SID = string.IsNullOrEmpty(Request.QueryString["SID"]) ? 0 : DataConverter.CLng(Request.QueryString["SID"]);
       
             int uid = buser.GetLogin().UserID;
                 B_Answer ban = new B_Answer();
             if (SID > 0)
             {
                 //tblAnswers = ban.Sel("Surveyid=" + SID + " And UserID=" + uid, "");
                 //dt = bans.Sel("Userid=" + uid + " And Sid=" + SID, "");
                 //int sta=Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                 //string str="";
                 //switch (sta)
                 //{
                 //    case 0:
                 //        str = "未审核";
                 //        break;
                 //    case 1:
                 //        str = "已审核";
                 //        break;
                 //    case 2:
                 //        str = "已录取";
                 //        break;
                 //    case -1:
                 //        str = "未提交";
                 //        break;
                 //    case -2:
                 //        str = "已解锁";
                 //        break;
                 //    default:
                 //        break;
                 }
                 //lbStatus.Text = str;
             }
    }
}
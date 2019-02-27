using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
public partial class User_UserZone_Kiss_rekiss : System.Web.UI.Page
{
    private B_User ull = new B_User();
    private B_Sns_Kiss kll = new B_Sns_Kiss();
    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();
        M_UserInfo info = ull.GetLogin();
        int userid=DataConverter.CLng(Request.QueryString["userid"]);

  
        this.Image1.ImageUrl = ull.GetUserBaseByuserid(info.UserID).UserFace;
        this.Label1.Text = info.UserName;


        this.Label2.Text = "向" + ull.GetUserByUserID(userid).UserName+"发送秋波";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        int userid = DataConverter.CLng(Request.QueryString["userid"]);
  
        M_Sns_Kiss kinfo = new M_Sns_Kiss();
        int sendtoid = userid;
        kinfo.Content = Request.Form["txt_content"].ToString();
        kinfo.Inputer = ull.GetLogin().UserName;
        kinfo.InputerID = ull.GetLogin().UserID;
        kinfo.SendtoID = sendtoid;
        kinfo.Sendto = ull.GetUserByUserID(sendtoid).UserName;
        kinfo.Title = this.TextBox1.Text;
        kinfo.SendTime = DateTime.Now;
        kinfo.ReadTime = DateTime.Now;
        kinfo.Otherdel = 0;
        kinfo.IsRead = 0;
        kll.GetInsert(kinfo);
        Response.Write("<script>alert('发送成功!');location.href='kiss.aspx';</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("kiss.aspx");
    }
}

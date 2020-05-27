using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Common_Chat_chathistory : System.Web.UI.Page
{
    B_Admin adminBll = new B_Admin();
    B_User buser = new B_User();
    B_ChatMsg chatBll = new B_ChatMsg();
    public string suid { get { return Request.QueryString["suid"]; } }//senduid
    public string ruid
    {
        get { return ViewState["ruid"] as string; }
        set { ViewState["ruid"] = value; }
    }//receuser
    public int pageSize = 20;
    public int CPage { get { return PageCommon.GetCPage(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();//用户只能看到自己与别人的聊天记录
            if (string.IsNullOrEmpty(suid)) { function.WriteErrMsg("参数不正确"); }
            if (adminBll.CheckLogin())
            {
                //管理员允许指定发送和接收人
                if (!string.IsNullOrEmpty(Request.QueryString["ruid"]))
                {
                    ruid = Request.QueryString["ruid"].Trim(',');
                }
                else
                {
                    ruid = mu.UserID.ToString();
                }
            }
            else if (buser.CheckLogin())
            {
                ruid = mu.UserID.ToString();
            }
            else { function.WriteErrMsg("你当前尚未登录,只有登录后才可查看信息"); }
            M_UserInfo smu = buser.SeachByID(DataConvert.CLng(suid));
            if (smu != null)
                CUName_L.Text = "与" + smu.UserName + "的聊天记录";
            else
                function.WriteErrMsg("参数错误！");
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = chatBll.SelHistoryMsg(suid, ruid);
        int pageCount = 0;
        RPT.DataSource = PageCommon.GetPageDT(pageSize, CPage, dt, out pageCount);
        RPT.DataBind();
        Page_Lit.Text = PageCommon.CreatePageHtml(pageCount, CPage);
    }
    string mytlp = "<div class='mychat'><div class='otherchat_face'><img src='{0}' class='userface myface' onerror='this.src=\"/Images/userface/noface.png\";' />{1}</div>"
            + "<div class='mychat_c'><p class='other_time'><span class='remindgray'>{3}</span></p>"
            + "<div class='chat_content'>{2}</div><span class='arrow-before arrow'></span><span class='arrow-after arrow'></span></div></div>";
    string othertlp = "<div class='otherchat'><div class='otherchat_face'><img src='{0}' class='userface otherface' onerror='this.src=\"/Images/userface/noface.png\";'/>{1}</div>"
     + "<div class='chat_cont_c'><p class='other_time'><span class='remindgray'>{3}</span></p>"
     + "<div class='chat_content othercontent'>{2}</div><span class='arrow-before arrow'></span><span class='arrow-after arrow'></span></div></div>";
    public string GetChatContent()
    {
        string html = Eval("UserID").ToString().Equals(ruid.ToString()) ? mytlp : othertlp;
        return string.Format(html, Eval("UserFace"), Eval("UserName"), Eval("Content"),Convert.ToDateTime(Eval("CDate")).ToString("yy/MM/dd HH:mm:ss"));
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;
using ZoomLa.Components;
using Newtonsoft.Json;

public partial class Guest_AskAdd : System.Web.UI.Page
{
    protected B_User b_User = new B_User();//基本用户BLl
    protected M_UserInfo m_UserInfo = new M_UserInfo();
    protected B_Ask b_Ask = new B_Ask();//问题BLL
    protected M_Ask m_Ask = new M_Ask();
    protected B_GuestAnswer b_Ans = new B_GuestAnswer();
    B_TempUser tpuserBll = new B_TempUser();
    protected string fixID = "";//求助对象
    protected string fixName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "getgrade":
                    result = BindSubGrade(Request.Form["value"]);
                    break;
                default:
                    break;
            }
            Response.Write(result);Response.Flush();Response.End();
        }
        if (!b_User.CheckLogin())
        {
            if (GuestConfig.GuestOption.WDOption.IsLogin)
                needlog_div.Visible = true;
            else
                vode_div.Visible = true;   
        }
        M_UserInfo info = tpuserBll.GetLogin();// b_User.GetLogin(false);
        if (!string.IsNullOrEmpty(GuestConfig.GuestOption.WDOption.QuestGroup))
        {//用户组提问权限
            string groups = "," + GuestConfig.GuestOption.WDOption.QuestGroup + ",";
            if (!groups.Contains("," + info.GroupID.ToString() + ","))
                function.WriteErrMsg("您没有提问的权限!");
        }
        if (!IsPostBack)
        {
            BindddlCate();
            this.txtContent.Text = Request["strwhere"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["fix"]))
        {
            fixID = Request.QueryString["fix"];
            fixName = b_User.SeachByID(Convert.ToInt32(fixID)).UserName;
            fix.Visible = true;
        }
    }
    protected void BindddlCate()
    {
        DataTable dt = B_GradeOption.GetGradeList(2, 0);
        ddlCate.DataSource = dt;
        ddlCate.DataTextField = "GradeName";
        ddlCate.DataValueField = "GradeID";
        ddlCate.DataBind();
        function.Script(this, "InitSubGrade(" + BindSubGrade(ddlCate.SelectedValue) + ")");
    }
    /// <summary>
    /// 绑定子分类
    /// </summary>
    public string BindSubGrade(string value)
    {
        DataTable dt = B_GradeOption.GetGradeList(2, DataConvert.CLng(value));
        return JsonConvert.SerializeObject(dt);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        m_UserInfo=tpuserBll.GetLogin();
        if (m_UserInfo.UserID>0)
        {
            if (m_UserInfo.UserExp < Convert.ToInt32(ddlScore.SelectedItem.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('积分不足！');</script>");
                return;
            }
        }
        else if(!GuestConfig.GuestOption.WDOption.IsLogin)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text.Trim()))
            {
                function.WriteErrMsg("验证码不正确!");
            }
        }
        else
            Response.Redirect("/User/Login.aspx?ReturnUrl=/Guest/Ask/Add.aspx");
        m_Ask.Qcontent = txtContent.Text.Trim();
        m_Ask.Supplyment = txtSupplyment.Text.Trim();
        m_Ask.AddTime = DateTime.Now;
        m_Ask.UserId = m_UserInfo.UserID;
        m_Ask.UserName = m_UserInfo.UserID > 0 ? m_UserInfo.UserName : m_UserInfo.UserName + "[" + m_UserInfo.WorkNum + "]";
        m_Ask.Score = Convert.ToInt32(ddlScore.SelectedItem.Text);
        if (isNi.Checked == true)
        {
            m_Ask.IsNi = 1;
        }
        else m_Ask.IsNi = 0;

        m_Ask.QueType = Request.Form["subgrade"];
        if (string.IsNullOrEmpty(m_Ask.QueType)) { m_Ask.QueType=ddlCate.SelectedValue; }
        m_Ask.Elite = 0;
        m_Ask.Status = 1;
        int flag = b_Ask.insert(m_Ask);
        DataTable dts = b_Ask.Sel();
        if (dts.Rows.Count > 0)
        {
            hfid.Value = (dts.Rows[0]["ID"]).ToString();
        }
        if (Convert.ToInt32(ddlScore.SelectedItem.Text) > 0&&b_User.CheckLogin())
        {
            //悬赏积分
            b_User.ChangeVirtualMoney(m_UserInfo.UserID, new M_UserExpHis()
            {
                score = 0 - Convert.ToInt32(ddlScore.SelectedItem.Text),
                ScoreType = (int)M_UserExpHis.SType.Point,
                detail = m_UserInfo.UserName + "提交问题[" + txtContent.Text.Trim() + "],扣除悬赏积分-" + ddlScore.SelectedItem.Text
            });
        }
        if (fix.Visible == true)//提交时，若求助对象可见，则向求助对象发送一条短信息
        {
            B_Message message = new B_Message();
            M_Message messInfo = new M_Message();
            messInfo.Incept = fixID;
            string UserName = m_UserInfo.UserName;
            messInfo.Sender = m_UserInfo.UserID.ToString();
            messInfo.Title = "来自" + m_UserInfo.UserName + "的问答求助";
            messInfo.PostDate = DateTime.Now;
            messInfo.Content = "<a href=\"/Guest/Question/MyAnswer.aspx?ID=" + (dts.Rows[0]["ID"]).ToString() + "\" target=\"_blank\">" + txtContent.Text + "</a>";
            messInfo.Savedata = 0;
            messInfo.Receipt = "";
            int i = message.GetInsert(messInfo);
        }
        if (flag > 0 && m_UserInfo.UserID>0)
        {
            b_User.ChangeVirtualMoney(m_UserInfo.UserID, new M_UserExpHis()
            {
                score = GuestConfig.GuestOption.WDOption.QuestPoint,
                ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                detail = m_UserInfo.UserName + "提交问题[" + txtContent.Text.Trim() + "],增加问答积分" + GuestConfig.GuestOption.WDOption.QuestPoint
            });
        }
        Response.Write("<script>location.href='Success.aspx?ID=" + m_UserInfo.UserID + "';</script>");

    }

    
    protected string getstyle()
    {
        if (b_User.CheckLogin())
        {
            return "display:inline-table;";
        }
        else return "display:none";
    }
    protected string getstyles()
    {
        if (b_User.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inline-table;";
    }
    /// <summary>
    /// 取已解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvedCount()
    {
        return b_Ask.IsExistInt("Status=2").ToString();
    }
    /// <summary>
    /// 取待解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvingCount()
    {
        return b_Ask.IsExistInt("Status=1").ToString();
    }
    /// <summary>
    /// 取得用户总数
    /// </summary>
    /// <returns></returns>
    public string getUserCount()
    {
        return b_User.GetUserNameListTotal("").ToString();
    }
    /// <summary>
    /// 取得当前在线人数
    /// </summary>
    /// <returns></returns>
    public string getLogined()
    {
        DateTime dtNow = DateTime.Now.AddMinutes(-1);
        if (Application["online"] != null)
            return Application["online"].ToString();
        else
            return "";
    }
    ///<summary>
    ///取得最佳回答采纳率
    ///</summary>
    /// <returns></returns>
    public string getAdoption()
    {
        double adopCount = b_Ans.IsExistInt(2);
        double count = b_Ans.getnum();
        return ((adopCount / count) * 100).ToString("0.00") + "%";
    }
}
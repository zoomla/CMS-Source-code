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
using System.Text.RegularExpressions;

public partial class User_Questions_MyCoruse : System.Web.UI.Page
{
    private B_Exam_Class cll = new B_Exam_Class();
    protected B_User ull = new B_User();
    private B_Course bcourse = new B_Course();
    private B_UserCourse busercourse = new B_UserCourse();
    protected B_ExClassgroup ell = new B_ExClassgroup();
    private B_Courseware bcourseware = new B_Courseware();

    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();
        if (!IsPostBack)
        {
            RepNodeBind();
        }
    } 

    #region 数据邦定
    /// <summary>
    /// 数据邦定
    /// </summary>
    private void RepNodeBind()
    {
        int CPage;
        int temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = DataConverter.CLng(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = DataConverter.CLng(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        Regex urlRegex = new Regex(@"(?:^|\?|&)c_id=([^&]*)(?:&|$)");
       // Match m = urlRegex.Match(Request.UrlReferrer.ToString().ToLower());
        string pagenum = string.Empty;
        //if (m.Success)
        //{
        //    pagenum = m.Groups[1].Value;
        //}
        int cid = DataConverter.CLng(pagenum);
        //用户审核通过课程
        //string url=Request.UrlReferrer.ToString();

        DataTable qus = busercourse.SelectByUserid(ull.GetLogin().UserID, 1, cid);

        rep.DataSource = qus;
        this.rep.DataBind();
        
    }
    #endregion

    public string GetCourse(string courseID)
    {
        M_Course cour = bcourse.GetSelect(DataConverter.CLng(courseID));
        if (cour != null)
        {
            return cour.CourseName;
        }
        else
        {
            return "";
        }
    }

    public string GetUser(string userid)
    {
        M_UserInfo info = ull.GetUserByUserID(DataConverter.CLng(userid));
        if (info != null && info.UserID > 0)
        {
            return info.UserName;
        }
        else
        {
            return "";
        }
    }

    public string GetTime(string Classid)
    {
        M_ExClassgroup mex = ell.GetSelect(DataConverter.CLng(Classid));
        if (mex != null)
        {
            return mex.Setuptime.ToShortDateString();
        }
        else
        {
            return "";
        }
    }

    public string GetClass(string classid, int ucid, string type)
    {
        M_ExClassgroup mex = ell.GetSelect(DataConverter.CLng(classid));
        M_UserCourse muc = busercourse.GetSelect(ucid);
        if (mex != null)
        {
            if (type == "1" || type == "2")
            {
                int courseDay = ((new TimeSpan(mex.Endtime.Ticks)) - (new TimeSpan(mex.Setuptime.Ticks))).Days;
                int userDay = ((new TimeSpan(DateTime.Now.Ticks)) - (new TimeSpan(muc.AddTime.Ticks))).Days;
                if (courseDay - userDay > 0)
                {
                    return courseDay - userDay + "/" + courseDay;
                }
                else
                {
                    B_Payment bpm = new B_Payment();
                    M_Payment mpm = bpm.SelModelByPayNo(muc.OrderID);
                    if (mpm==null||mpm.Status!=3)
                    {
                        if (!(muc.State == 1 && muc.Aunit == 0))
                        {
                            muc.State = 1;
                            muc.Aunit = 0;
                            muc.OrderID = "";
                            busercourse.GetUpdate(muc);
                        }
                        return "0/" + courseDay + "<br><font style='color:#999999'>(请续费)</font>"; ;
                    }
                    else
                    {
                        return "0/" + courseDay + "<br><font style='color:#999999'>(已续费)</font>"; ;
                    }
                }
            }
            if (type == "3")
            {
                return mex.CourseHour.ToString();
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    public bool isCoruseExpireByUcid(int UserCoruseID)
    {
        B_UserCourse buc = new B_UserCourse();
        M_UserCourse muc = buc.GetSelect(UserCoruseID);
        B_ExClassgroup bex = new B_ExClassgroup();
        M_ExClassgroup mex = bex.GetSelect(muc.ClassID);
        int courseDay = ((new TimeSpan(mex.Endtime.Ticks)) - (new TimeSpan(mex.Setuptime.Ticks))).Days;
        int userDay = ((new TimeSpan(DateTime.Now.Ticks)) - (new TimeSpan(muc.AddTime.Ticks))).Days;
        if (courseDay - userDay > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string getIsExpireStr(int id)
    {
        string ruselt = "";
        if (isCoruseExpireByUcid(id))
        {

        }
        else
        {
           
        }
        return ruselt;
    }
    public string getStartLesson(int id, int ucid)
    {
        B_UserCourse buc = new B_UserCourse();
        M_UserCourse muc = buc.GetSelect(ucid);
        string result = "";
        if (isCoruseExpireByUcid(ucid))
        {

            result = "<input type=\"button\" value=\"开始上课\" class=\"C_input\" onclick=\"onShow('" + id + "')\" />";
        }
        else
        {
            B_Payment bpm = new B_Payment();
            M_Payment mpm = bpm.SelModelByPayNo(muc.OrderID);
            if (mpm==null || mpm.Status != 3)
            {
                result = "<a  href=\"CreateCourse.aspx?courseid=" + muc.CourseID + "&ucid=" + ucid + "&xufei=true\" onclick=\"\" >续费</a><br><font style='color:#999999'>(已到期)</font>";

            }
            else
            {

                result = " <font style='color:#999999'>已续费<br>(等待审核)</font>"; 
            }
        }
        return result;
    }

    public string GetState(string classid)
    {
        M_ExClassgroup mex = ell.GetSelect(DataConverter.CLng(classid));
        if (mex != null)
        {
            if (mex.Setuptime <= DateTime.Now)
            {
                return "开放";
            }
            else
            {
                return "未开放";
            }
        }
        else
        {
            return "未开放";
        }
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        RepNodeBind();
    }


    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Repeater Detail = e.Item.FindControl("Detail") as Repeater;
            HiddenField hfId = e.Item.FindControl("hfId") as HiddenField;
            DataTable dt = bcourseware.Select_CourseID(DataConverter.CLng(hfId.Value));
            if (dt != null && dt.Rows.Count > 0)
            {
                Detail.DataSource = dt;
                Detail.DataBind();
            }
        }
    }
    protected void lbStudy_Click(object sender, EventArgs e)
    {
        LinkButton lbTemp = ((LinkButton)sender);
        B_ExAttendance bExAttendance = new B_ExAttendance();
        M_ExAttendance mExAttendance = new M_ExAttendance();
        M_UserInfo mUserInfo = ull.GetLogin();
        mExAttendance.Stuid = mUserInfo.UserID;
        mExAttendance.StuName = mUserInfo.UserName;
        mExAttendance.LogTime = DateTime.Now;
        mExAttendance.Logtimeout = 0;
        mExAttendance.Location = lbTemp.CommandName;
        bExAttendance.insert(mExAttendance);

        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(upSpare, upSpare.GetType(), "", "alert('" + mExAttendance.Stuid + ":" + mExAttendance.StuName + ":" + mExAttendance.LogTime + ":" + mExAttendance.Logtimeout + ":" + mExAttendance.Location + "');", true); 
        function.Script(this,"open('" + lbTemp.CommandArgument + @"','','');");
    }




}
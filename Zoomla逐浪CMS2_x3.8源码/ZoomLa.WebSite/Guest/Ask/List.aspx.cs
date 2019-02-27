using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Components;
public partial class Guest_AnswerList : System.Web.UI.Page
{
    protected B_Ask b_Ask = new B_Ask();//问题BLL
    protected B_User b_User = new B_User();//基本用户BLl
    protected B_GuestAnswer b_Ans = new B_GuestAnswer();
    public int Type { get { return DataConvert.CLng(Request.QueryString["type"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = null;
        if (Type == 1)
            dt= b_Ask.SelWaitQuest();
        else
            dt= b_Ask.SelAll();
        EGV.DataSource = dt;
        if (!string.IsNullOrEmpty(Request.QueryString["QueType"]))
        dt.DefaultView.RowFilter = "QueType='" + Request.QueryString["QueType"] + "'";
        EGV.DataKeyNames = new string[] { "ID" };
        EGV.DataBind();
       
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected string gettype(int id)
    {
        DataTable dt = Sql.Sel("zl_grade", " GradeID=" + id, "");
        if (dt.Rows.Count > 0)
        {
            string name;
            name = (dt.Rows[0]["GradeName"]).ToString();
            return name;

        }
        else
            return " ";
    }

    public string GetCate(string CateID)
    {
        string re = "";
        return re;
    }
   
    // 取已解决问题总数
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
    // 取得当前在线人数
    public string getLogined()
    {
        DateTime dtNow = DateTime.Now.AddMinutes(-1);
        if (Application["online"] != null)
            return Application["online"].ToString();
        else
            return "";
    }
    //取得最佳回答采纳率
    public string getAdoption()
    {
        double adopCount = b_Ans.IsExistInt(2);
        double count = b_Ans.getnum();
        return ((adopCount / count) * 100).ToString("0.00") + "%";
    }

    protected string getanswer(int id)
    {
        string answer;
        DataTable dt = Sql.Sel("ZL_GuestAnswer", " QueId=" + id, "");
        if (dt.Rows.Count > 0)
        {
            answer = (dt.Rows[0]["Content"]).ToString();
            return answer;
        }
        else
            return " ";
    }

    protected string Getname(string isNi, string UserName, string UserID)
    {
        string str = "";
        if (isNi == "1") { str = "匿名"; }
        else { str = " <a href='../../ShowList.aspx?id=" + UserID + "' target='_blank'>" + UserName + "</a></asp:Label>"; }
        return str;
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Guest_AskStar : System.Web.UI.Page
{
    protected B_User b_User = new B_User();//基本用户BLl
    protected M_UserInfo m_UserInfo = new M_UserInfo();
    protected B_Ask b_Ask = new B_Ask();//问题BLL
    protected M_Ask m_Ask = new M_Ask();
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo info = b_User.GetLogin();
        if (!IsPostBack)
        {
            bindRepeater1();
        }
    }
    
    public string GetCate(string CateID)
    {
        string re = "";
        return re;
    }
   
    
    protected string getstyle()
    {
        if (b_User.CheckLogin())
        {
            return "display:inherit";
        }
        else return "display:none";
    }
    protected string getstyles()
    {
        if (b_User.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inherit";
    }
    /// <summary>
    /// 取已解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvedCount()
    {
        return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_Ask where Status=1 ", null).Rows[0][0].ToString();
    }
    /// <summary>
    /// 取待解决问题总数
    /// </summary>
    /// <returns></returns>
    public string getSolvingCount()
    {
        return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_Ask where Status=0 ", null).Rows[0][0].ToString();
    }
    /// <summary>
    /// 取得用户总数
    /// </summary>
    /// <returns></returns>
    public string getUserCount()
    {
        return SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_User ", null).Rows[0][0].ToString();
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
        double adopCount = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where Status=1", null).Rows[0][0]);
        double count = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer", null).Rows[0][0]);
        return ((adopCount / count)*100).ToString("0.00") + "%";
    }

    private void bindRepeater1()
    {
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select top 10 a.* from (select a.UserID as cuid,count(a.UserID) as ccount from ZL_User a,ZL_GuestAnswer b where b.Status=1 and a.UserID=b.UserID group by(a.UserID)) c,ZL_User a where c.cuid=a.UserID order by c.ccount desc", null);//取被采纳问题数前十为知道之星
        Repeater1.DataSource = dt;
        Repeater1.DataBind(); 
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rep = e.Item.FindControl("Repeater2") as Repeater;
        DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
        int UserID = Convert.ToInt32((rowv["UserID"]));
        DataTable dt1 = SqlHelper.ExecuteTable("SELECT TOP 3 * FROM ZL_GuestAnswer WHERE Status=1 and UserId=" + UserID + " ORDER BY AddTime DESC");
        string str = "";
        if (dt1.Rows.Count > 0)
        {
            str = " ID=" + dt1.Rows[0]["QueId"].ToString();
            for (int i = 1; i < dt1.Rows.Count; i++)
            {
                str = str + " or ID=" + dt1.Rows[i]["QueId"].ToString();
            }
        }
        DataTable dt2 = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_Ask where " + str, null);
        rep.DataSource = dt2;
        //Response.Write("<script>alert('" + dt2.Rows.Count.ToString() + "');</script>");
        rep.DataBind();
    }
    /// <summary>
    /// 获取回答数
    /// </summary>
    /// <returns></returns>
    protected string getanswer(string uid)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where UserId=@uid", sp);
        return dt.Rows[0][0].ToString();
    }
    protected string getask(string uid)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_Ask where UserId=@uid", sp);
        return dt.Rows[0][0].ToString();
    }
    protected string getRat(string uid)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
        double adopCount = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where Status=1 and UserId=@uid", sp).Rows[0][0]);
        double count = Convert.ToDouble(SqlHelper.ExecuteTable(CommandType.Text, "select count(*) from ZL_GuestAnswer where UserId=@uid", sp).Rows[0][0]);
        return ((adopCount / count) * 100).ToString("0.00") + "%";
    }
}
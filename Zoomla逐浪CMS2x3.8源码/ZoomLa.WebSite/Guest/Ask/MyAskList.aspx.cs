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
using ZoomLa.BLL.User;
using ZoomLa.Components;


public partial class Guest_MyAskList : System.Web.UI.Page
{

    protected B_Ask b_Ask = new B_Ask();//问题BLL
    protected B_User b_User = new B_User();//基本用户BLl
    B_TempUser tpuserBll = new B_TempUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        string id = Request.QueryString["ID"];
        string strwhere = "";
        M_UserInfo mu = tpuserBll.GetLogin();
        if (mu.UserID > 0)
        {
            strwhere = " UserName=@name AND Status<>0";
        }
        else
            strwhere = " UserName='" + mu.UserName + "[" + mu.WorkNum + "]" + "'";

        DataTable dt;
        if (string.IsNullOrEmpty(Request.QueryString["QueType"]) && string.IsNullOrEmpty(id))
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", mu.UserName) };
            dt = b_Ask.Sel(strwhere, "", sp);
        }
        else if (string.IsNullOrEmpty(Request.QueryString["QueType"]) && !string.IsNullOrEmpty(id))
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", mu.UserName), new SqlParameter("id", id) };
            dt = b_Ask.Sel(" UserId=@id", "", sp);
        }
        else
        {
            string QueType = Request.QueryString["QueType"].ToString();
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", mu.UserName), new SqlParameter("QueType", "%" + QueType + "%") };
            dt = b_Ask.Sel(" QueType like @QueType" + strwhere, "", sp);
        }

        //if (!string.IsNullOrEmpty(key.Trim()))
        //{
        //    dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
        //    dt = dt.DefaultView.ToTable();
        //}
        EGV.DataSource = dt;
        EGV.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public string gettype(string id)
    {
        int gid = Convert.ToInt32(id);
        DataTable dt = Sql.Sel("zl_grade", " GradeID=" + gid, "");
        if (dt.Rows.Count > 0)
        {
            string name;
            name = (dt.Rows[0]["GradeName"]).ToString();
            return name;

        }
        else
            return "";
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
        return ((adopCount / count) * 100).ToString("0.00") + "%";
    }

    //切字
    public string GetLeftString(string str, int length)
    {
        //str = function.Decode(str);
        if (str.Length <= length)
            return str;
        return str.Substring(0, length) + "..."; ;
    }

    protected string GetStatus(int id)
    {
        if (id == 0)
        {
            return "未处理";
        }
        else if (id == 1)
        {
            return "处理中";
        }
        else
        {
            return "<font class='red'>已解决</font>";
        }
    }
    protected string Getname(string isNi, string UserName, string UserID)
    {
        string str = "";
        if (isNi == "1") { str = "匿名"; }
        else { str = " <a href='../../ShowList.aspx?id=" + UserID + "' target='_blank'>" + UserName + "</a></asp:Label>"; }
        return str;
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                //删除记录，同时删除目标数据库
                break;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_UserZone_QueryUser : System.Web.UI.Page
{
    B_User buser = new B_User();
    public int UserID { get { return DataConvert.CLng(Request.QueryString["UserID"]); } }
    public string Skey { get { return HttpUtility.UrlDecode(Request.QueryString["Skey"] ?? ""); } }
    public int Sex { get { return DataConvert.CLng(Request.QueryString["sex"]); } }
    public string Age { get { return Request.QueryString["age"] ?? ""; } }
    //本页面默认关闭
    //仅显示简略的用户信息,并提供添加好友功能
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        EGV.DataSource = SelByInfo();
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                break;
        }
        MyBind();
    }
    private DataTable SelByInfo()
    {
        M_UserInfo mu=buser.GetLogin();
        string where = " A.UserID !=" + mu.UserID + " ";//过滤掉自己
        List<SqlParameter> sp = new List<SqlParameter>();
        if (UserID > 0) { where += " AND A.UserID = " + UserID; }
        if (!string.IsNullOrEmpty(Skey))
        {
            sp.Add(new SqlParameter("skey", "%" + Skey + "%"));
            where += " AND A.UserName LIKE @skey";
        }
        if (Sex > 0) { where += " AND B.UserSex=" + Sex; }
        //后期支持年龄段查询 Birthday
        //if (!string.IsNullOrEmpty(Age)) { where += " AND B.Age<" + DataConvert.CLng(Age); }
        return DBCenter.JoinQuery("A.*,B.UserSex", "ZL_User", "ZL_UserBase", "A.UserID=B.UserID", where, "A.RegTime DESC", sp.ToArray());
    }
}
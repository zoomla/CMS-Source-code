using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
public partial class manage_AddOn_ProjectIndex : System.Web.UI.Page
{
    private B_Client_Basic bll = new B_Client_Basic();

    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='ManageProjects.aspx'>项目管理</a></li><li>"+ DateTime.Now.ToShortDateString() + "　农历：" + Season.GetLunarCalendar(DateTime.Now) + "　" + Season.GetWeekCHA() + "　" + Season.ChineseTwentyFourDay(DateTime.Now) + "</li>");
        if (!Page.IsPostBack)
        {
            DataTable dt = new DataTable();
            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
               // dt = bps.Select_All();
            }
            else
            {
               // dt = bps.Select_All("", "", "", "", 0, badmin.GetAdminLogin().AdminName);
            }
            bind();

            if (dt != null)
                ProjectBind(dt);
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        DataTable tables = bll.SelectNew(5);
        if (tables != null && tables.Rows.Count > 0)
        {
            tables.DefaultView.Sort = "Flow desc";
            Page_list(tables);
        }
    }

    private void bind()
    {
        DataTable Cll = B_IServer.SeachTop("未解决", -1);
        if (Cll != null)
        {
            resultsRepeater_w.DataSource = Cll.DefaultView;
            resultsRepeater_w.DataBind();
        }
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        if (Cll != null)
        {
            Repeater2.DataSource = Cll;
            Repeater2.DataBind();
        }
    }
    #endregion

    protected void ProjectBind(DataTable dt)
    {
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dt.DefaultView;
        pds.AllowPaging = true;
        pds.PageSize = 7;
        Repeater1.DataSource = pds;
        Repeater1.DataBind();
        foreach (RepeaterItem item in Repeater1.Items)
        {
            HtmlGenericControl line = (HtmlGenericControl)item.FindControl("line");
            string id = (item.FindControl("Label1") as Label).Text;
            line.Style["width"] = GetLong(id);
        }
    }

    protected string GetLong(string id)
    {
        return "";
    }

    //绑定类型
    protected string GetProType(string typeid)
    {
        //if (bptp.GetSelect(DataConverter.CLng(typeid)).ProjectTypeName == "")
        //    return "类型已删";
        //else
        //    return bptp.GetSelect(Convert.ToInt32(typeid)).ProjectTypeName;
        return "";
    }
    protected string GetLeader(string leader)
    {
        if (leader != null && leader != "")
        {
            return leader;
        }
        else
            return "暂无";
    }

    /// <summary>
    /// 获得管理员等级
    /// </summary>
    /// <returns></returns>
    protected int GetManageGroup(string Leader)
    {
        B_Admin badmin = new B_Admin();

        if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
        {
            return 1;
        }
        else
        {
            if (Leader == badmin.GetAdminLogin().AdminName)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Zone_SnsClassRoom : CustomerPageAction
{
    B_ClassRoom classBll = new B_ClassRoom();
    public int ClassStatus { get { return string.IsNullOrEmpty(Request.QueryString["status"]) ? -1 : DataConverter.CLng(Request.QueryString["status"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Exam/Papers_System_Manage.aspx'>教育模块</a></li><li class='active'>班级管理<a href='AddClassRoom.aspx'>[添加班级]</a></li>");
        }
    }
    public void MyBind(string key = "")
    {
        Egv.DataSource = classBll.Select_All(0, ClassStatus, key);
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected string GetUser(string userid)
    {
        int uid = DataConverter.CLng(userid);
        B_User ull = new B_User();
        return ull.GetUserByUserID(uid).UserName;
    }

    protected string GetSchool(string SchoolID)
    {
        int sid = DataConverter.CLng(SchoolID);
        B_School sll = new B_School();
        return sll.GetSelect(sid).SchoolName;
    }
    protected string GetAudit()
    {
        if (Eval("IsTrue").ToString().Equals("1")) { return "<span style='color:green;'>审核通过</span>"; }
        return "<span style='color:red;'>未审核</span>";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //批量删除
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            classBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //审核操作
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            classBll.UpdateByState(Request.Form["idchk"], 1);
        }
        MyBind();
    }
    //是否毕业
    public string GetIsDone()
    {
        if (Eval("IsDone").Equals(1)) { return "<span class='fa fa-check'></span>"; }
        else { return "<span class='fa fa-remove'></span>"; }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del":
                classBll.GetDelete(Convert.ToInt32(e.CommandArgument.ToString()));
                function.WriteSuccessMsg("删除成功!", "SnsClassRoom.aspx");
                break;
            default :
                break;
        }
        MyBind();
    }
    //评星
    public string GetStar()
    {
        string result = "";
        int score = DataConverter.CLng(Eval("ClassStar"));
        for (int i = 0; i < score; i++)
        {
            result += "<i class='staricon fa fa-star'></i>";
        }
        for (int i = 0; i < 5 - score; i++)
        {
            result += "<i class='staricon fa fa-star-o'></i>";
        }
        return result;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //取消审核操作
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            classBll.UpdateByState(Request.Form["idchk"], 0);
        }
        MyBind();
    }
    public string GetIcon()
    {
        string iconstr = Eval("Monitor").ToString();
        return StringHelper.GetItemIcon(iconstr);
        //if (iconstr.Contains("/")||string.IsNullOrEmpty(iconstr))//判断是否为路径
        //{ return "<img src='" + Eval("Monitor") + "' onerror=\"this.src = '/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' />"; }
        //return "<span class='" + Eval("Monitor") + "'></span>";
        
    }
}

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

public partial class manage_Question_ClassManage : CustomerPageAction
{
    B_ClassRoom classBll = new B_ClassRoom();
    B_Student stuBll = new B_Student();
    B_User buser = new B_User();
    public int audit { get { return string.IsNullOrEmpty(Request.QueryString["audit"]) ? -1 : DataConverter.CLng(Request.QueryString["audit"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        RPT_Teach.DataSource = classBll.Select_Stu_All(mu.UserID, -1, audit);
        RPT_Teach.DataBind();
        StuClass_Div.Visible = true;
        //if (!buser.IsTeach())//如果是学生添加筛选
        //{

        //}
        //else
        //{
        //    RPT_Teach.DataSource = classBll.Select_All(mu.UserID, 1);
        //    RPT_Teach.DataBind();
        //}
        Del_Btn.Visible = buser.IsTeach();

        function.Script(this, "InitTab(" + audit + ")");
    }
    public string GetOP()
    {
        if (!Eval("IsTrue").Equals(1)) { return ""; }//未经审核不能操作
        string opthtml = "";
        if (Eval("CreateUser").Equals(buser.GetLogin().UserID))//班级创建者可以编辑班级
        {
            opthtml += "<a href='AddClass.aspx?cid=" + Eval("RoomID") + "' title='修改'><span class='fa fa-pencil'></span></a> ";
            opthtml += "<a href='javascript:;' onclick=\"DelByID('" + Eval("RoomID") + "')\" title='删除'><span class='fa fa-trash-o'></span></a> ";
        }
        if (DataConverter.CLng(Eval("StuAuditing").ToString())!=0)
        {
            opthtml += "<a href='/User/Exam/StudentManage.aspx?cid=" + Eval("RoomID") + "&stutype=1'>学生列表</a> ";
            opthtml += "<a href='/User/Exam/StudentManage.aspx?cid=" + Eval("RoomID") + "&stutype=2'>教师列表</a> ";
            opthtml += "<a href='/User/Exam/StudentManage.aspx?cid="+Eval("RoomID")+"&stutype=3'>家长列表</a> ";
        }
        return opthtml;
    }
    public string GetStatu()
    {
        if (Eval("IsTrue").Equals(1)) { return "<span style='color:green;'>已审核</span>"; }
        return "<span style='color:red;'>未审核</span>";
    }
   //是否毕业
    public string GetIsDone()
    {
        if (Eval("IsDone").Equals(1)) { return "<span class='fa fa-check'></span>"; }
        else { return "<span class='fa fa-remove'></span>"; }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        classBll.Del(DataConverter.CLng(DelClass_Hid.Value));
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
    public string GetIcon()
    {
        string iconstr = Eval("Monitor").ToString();
        iconstr = string.IsNullOrEmpty(iconstr) ? "/UploadFiles/nopic.gif" : iconstr;
        if (iconstr.Contains("/"))//判断是否为路径
        { return "<a href='"+ iconstr + "' target='_bank'><img src='" + Eval("Monitor") + "' onerror=\"this.src = '/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' /></a>"; }
        return "<span class='" + iconstr + "'></span>";
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_Exam_QuestView : System.Web.UI.Page
{
    //用于浏览试题详情,是否需要必须是老师才可浏览,避免作弊
    B_User buser = new B_User();
    B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    M_Exam_Sys_Questions questMod = new M_Exam_Sys_Questions();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind() 
    {
        questMod = questBll.GetSelect(Mid);
        Title_L.Text = questMod.p_title;
        Content_L.Text = questMod.p_Content;
        Jiexi_L.Text = questMod.Jiexi;
        Shuming_L.Text = questMod.p_shuming;
    }
}
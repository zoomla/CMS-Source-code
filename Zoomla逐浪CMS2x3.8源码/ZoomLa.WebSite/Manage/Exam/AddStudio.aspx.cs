using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class manage_Question_AddStudio : CustomerPageAction
{
    protected M_ExStudent sinfo = new M_ExStudent();
    protected B_ExStudent sll = new B_ExStudent();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int sid = DataConverter.CLng(Request.QueryString["id"]);
            M_ExStudent stinfo = sll.GetSelect(sid);
            this.txt_Stuname.Text = stinfo.Stuname;
            this.txt_Stupassword.Text = stinfo.Stupassword;
            this.txt_Addtime.Text = stinfo.Addtime.ToString();
            //this.txt_Qualified.Text = stinfo.Qualified.ToString();
            this.txt_Regulation.Text = stinfo.Regulation.ToString();
            this.txt_Stucardno.Text = stinfo.Stucardno.ToString();
            this.txt_Stugroup.Text = stinfo.Stugroup.ToString();
            this.txt_Exptime.Text = stinfo.Exptime.ToString();
            this.txt_Course.Text = stinfo.Course;

        }
        else
        {
            this.txt_Addtime.Text = DateTime.Now.ToString();
        }
        Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>学员管理</li><li>" + Label2.Text + "</li>");
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        sinfo.Addtime =DataConverter.CDate(this.txt_Addtime.Text);
        sinfo.Course = this.txt_Course.Text;//学习课程
        sinfo.Examnum = 0;//考试次数
        sinfo.Exptime = DataConverter.CDate(this.txt_Exptime.Text);
        sinfo.Lognum = 0;//登录次数
        sinfo.Logtimeout = 0;//登录时长
        //sinfo.Qualified = DataConverter.CLng(this.txt_Qualified.Text);//是否合格
        sinfo.Regulation =DataConverter.CLng(this.txt_Regulation.Text);//监管人ID
        sinfo.strCompetence = "";//考生权限
        sinfo.Stucardno = this.txt_Stucardno.Text;//考号
        sinfo.Stugroup =DataConverter.CLng(this.txt_Stugroup.Text);//所在组别ID
        sinfo.Stuname = this.txt_Stuname.Text;//考生姓名
        sinfo.Stupassword = this.txt_Stupassword.Text;//考生密码
        sinfo.Userid = 0;//绑定用户ID
        
        if (Request.Form["sid"] != null)
        {
            sinfo.Stuid = DataConverter.CLng(Request.Form["sid"]);
            sll.GetUpdate(sinfo);
            function.WriteSuccessMsg("修改成功!", "StudioManage.aspx");
        }
        else
        {
            sll.GetInsert(sinfo);
            function.WriteSuccessMsg("添加成功!", "StudioManage.aspx");
        }   
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudioManage.aspx");
    }
}
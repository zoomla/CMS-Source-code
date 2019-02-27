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


namespace ZoomLaCMS.Manage.Exam
{
    public partial class EditCourese : System.Web.UI.Page
    {
        protected M_ExStudent sinfo = new M_ExStudent();
        protected B_UserCourse sll = new B_UserCourse();
        protected B_User buser = new B_User();
        protected B_ExClassgroup cll = new B_ExClassgroup();
        protected B_Group gll = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable tinfo = cll.Select_All();
                this.ClassID.DataSource = tinfo;
                this.ClassID.DataValueField = "GroupID";
                this.ClassID.DataTextField = "Regulationame";
                this.ClassID.DataBind();

                if (Request.QueryString["menu"] == "edit")
                {
                    if (Request.QueryString["id"] != null)
                    {
                        this.Label1.Text = "修改学员";
                        this.EBtnSubmit.Text = "保存修改";

                        int sid = DataConverter.CLng(Request.QueryString["id"]);
                        M_UserCourse stinfo = sll.GetSelect(sid);
                        this.txt_Stuname.Text = buser.GetUserByUserID(stinfo.UserID).UserName;
                        this.txt_Stuname.ReadOnly = true;
                        this.txt_Addtime.Text = stinfo.AddTime.ToString();
                        this.txt_Remark.Text = stinfo.Remark.ToString();
                        this.ClassID.SelectedValue = stinfo.ClassID.ToString();
                        this.txt_State.SelectedValue = stinfo.State.ToString();
                        this.PayMent.SelectedValue = stinfo.PayMent.ToString();

                    }
                }
                else
                {
                    this.txt_Addtime.Text = DateTime.Now.ToString();
                }
                Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>学员管理</li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_UserCourse ucinfo = new M_UserCourse();

            if (Request.Form["sid"] != null)
            {
                int sid = DataConverter.CLng(Request.Form["sid"]);
                ucinfo = sll.GetSelect(sid);
            }
            int classids = DataConverter.CLng(this.ClassID.SelectedValue);
            ucinfo.ClassID = DataConverter.CLng(this.ClassID.SelectedValue);
            ucinfo.AddTime = DateTime.Now;
            ucinfo.CurrCoureHour = 0;
            ucinfo.PayMent = 0;
            ucinfo.Remark = this.txt_Remark.Text;
            ucinfo.State = DataConverter.CLng(this.txt_State.SelectedValue);
            ucinfo.CourseID = cll.GetSelect(classids).CourseID;


            string username = this.txt_Stuname.Text;
            M_UserInfo infos = buser.GetUserIDByUserName(username);
            if (infos.UserID > 0)
            {
                if (this.txt_Stupassword.Text != "")//密码不为空，则为修改密码
                {
                    /*更新用户密码*/
                    infos.UserPwd = StringHelper.MD5(this.txt_Stupassword.Text);
                    buser.UpDateUser(infos);//更新用户信息
                    ucinfo.UserID = infos.UserID;
                }
            }
            else
            {
                /*添加新用户*/
                //M_UserInfo uinfo = new M_UserInfo();
                //uinfo.UserName = this.txt_Stuname.Text;
                //uinfo.UserPwd = StringHelper.MD5(this.txt_Stupassword.Text);
                //uinfo.GroupID = gll.DefaultGroupID();
                //uinfo.JoinTime = DateTime.Now;
                //uinfo.State = 0;
                //int userid = buser.Add(uinfo); //获取刚产生的用户ID(添加用户表信息)
                //M_Uinfo infod = new M_Uinfo(false);
                //infod.UserId = userid;
                //buser.AddBase(infod);//添加用户基本信息表
                //ucinfo.UserID = userid;
            }

            if (Request.Form["sid"] != null)
            {
                sll.GetUpdate(ucinfo);
                function.WriteSuccessMsg("修改成功!", "ViewStudio.aspx?cid=" + this.ClassID.SelectedValue);
            }
            else
            {
                sll.GetInsert(ucinfo);
                function.WriteSuccessMsg("添加成功!", "ViewStudio.aspx?cid=" + this.ClassID.SelectedValue);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassManage.aspx");
        }
    }
}
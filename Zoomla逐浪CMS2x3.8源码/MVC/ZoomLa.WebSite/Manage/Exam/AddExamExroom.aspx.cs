namespace ZoomLaCMS.Manage.Exam
{
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
    public partial class AddExamExroom : System.Web.UI.Page
    {
        protected B_Exroom rll = new B_Exroom();
        protected B_ExStudent sll = new B_ExStudent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cid"] != null)
                {
                    string cid = Request.QueryString["cid"];
                    M_Exroom rinfo = rll.GetSelect(DataConverter.CLng(cid));
                    this.Label1.Text = "修改考场";
                    this.Literal1.Text = "修改考场";
                    this.EBtnSubmit.Text = "修改考场";
                    hiden_Hid.Value = cid;
                    this.txt_RoomName.Text = rinfo.RoomName;
                    this.txt_Starttime.Text = rinfo.Starttime.ToString();
                    this.txt_AddTime.Text = rinfo.AddTime.ToString();
                    this.txt_Endtime.Text = rinfo.Endtime.ToString();
                    if (rinfo.Stuidlist != null && rinfo.Stuidlist != "")
                    {
                        if (rinfo.Stuidlist.IndexOf(',') > -1)
                        {
                            string[] studarr = rinfo.Stuidlist.Split(',');
                            for (int i = 0; i < studarr.Length; i++)
                            {
                                int sid = DataConverter.CLng(studarr[i]);
                                if (sid > 0)
                                {
                                    M_ExStudent sinfo = sll.GetSelect(sid);
                                    ListItem itemlis = new ListItem();
                                    itemlis.Text = sinfo.Stuname.ToString();
                                    itemlis.Value = sinfo.Stuid.ToString();
                                    itemlis.Selected = true;
                                    this.txt_Stuidlist.Items.Add(itemlis);
                                }
                            }
                        }
                        else
                        {
                            int sid = DataConverter.CLng(rinfo.Stuidlist);
                            if (sid > 0)
                            {
                                M_ExStudent sinfo = sll.GetSelect(sid);
                                ListItem itemlis = new ListItem();
                                itemlis.Text = sinfo.Stuname.ToString();
                                itemlis.Value = sinfo.Stuid.ToString();
                                itemlis.Selected = true;
                                this.txt_Stuidlist.Items.Add(itemlis);
                            }
                        }
                    }
                }
                else
                {
                    this.txt_AddTime.Text = DateTime.Now.ToString();
                }
                Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>" + Literal1.Text + "</li>");
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamExroom.aspx");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Exroom roominfo = new M_Exroom();
            if (Request.Form["hiden"] != null)
            {
                int roomid = DataConverter.CLng(Request.Form["hiden"]);
                roominfo = rll.GetSelect(roomid);
            }
            roominfo.AddTime = DateTime.Now;
            roominfo.Endtime = DataConverter.CDate(this.txt_Endtime.Text);
            roominfo.ExaID = DataConverter.CLng(Request.Form["pageid"]);
            roominfo.RoomName = this.txt_RoomName.Text;
            roominfo.Starttime = DataConverter.CDate(this.txt_Starttime.Text);
            roominfo.Stuidlist = this.Request.Form["txt_Stuidlist"];
            if (Request.Form["hiden"] != null)
            {
                rll.GetUpdate(roominfo);
                function.WriteSuccessMsg("修改成功!", "ExamExroom.aspx");
            }
            else
            {
                rll.GetInsert(roominfo);
                function.WriteSuccessMsg("添加成功!", "ExamExroom.aspx");
            }
        }
    }
}
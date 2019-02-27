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

public partial class manage_Exam_AddStudioInfo : CustomerPageAction
{
    protected B_Recruitment rll = new B_Recruitment();
    protected B_EnrollList ell = new B_EnrollList();
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            if (Request.QueryString["stuid"] != null)
            {
                liCoures.Text = "修改学员信息";
                Label1.Text = "修改学员信息";
                int stuid = DataConverter.CLng(Request.QueryString["stuid"]);

                M_Recruitment rinfo = rll.GetSelect(stuid);
                this.txt_Studioname.Text = rinfo.Studioname.ToString();
                this.txt_CradNo.Text = rinfo.CradNo;
                this.txt_LogPassWord.Text = rinfo.LogPassWord;
                this.txt_PriorUserName.Text = rinfo.PriorUserName;
                this.txt_Remark.Text = rinfo.Remark;
                this.txt_Addinfo.Text = rinfo.Addinfo;
                this.txt_Tel.Text = rinfo.Tel;
                this.EBtnSubmit.Text = "修改信息";
            }
            else
            {
                liCoures.Text = "添加学员信息";
                int id = DataConverter.CLng(Request.QueryString["id"]);
                HiddenField hiden = new HiddenField();
                sid_Hid.Value = id.ToString();
                this.txt_WriteTime.Text = DateTime.Now.ToString();
            }
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>培训资料库</li><li>" + liCoures.Text+ "</li>");
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (Request.Form["stuid"] != null)
        {
            int stuid = DataConverter.CLng(Request.Form["stuid"]);

            M_Recruitment rinfo = rll.GetSelect(stuid);

            rinfo.Studioname = this.txt_Studioname.Text;
            rinfo.CradNo = this.txt_CradNo.Text;
            rinfo.LogPassWord = this.txt_LogPassWord.Text;
            rinfo.PriorUserName = this.txt_PriorUserName.Text;
            rinfo.Remark = this.txt_Remark.Text;
            rinfo.Addinfo = this.txt_Addinfo.Text;
            rinfo.Tel = this.txt_Tel.Text;
            string sid = rinfo.EnrolllistID.ToString();
            rll.GetUpdate(rinfo);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功!');location.href='StudioInfoListByTech.aspx?id=" + sid.ToString() + "';", true);

        }
        else
        {
            int sid = DataConverter.CLng(Request.Form["sid"]);
            M_EnrollList llinfo = ell.GetSelect(sid);
            M_Recruitment rinfo = new M_Recruitment();
            rinfo.Studioname = this.txt_Studioname.Text;
            rinfo.CradNo = this.txt_CradNo.Text;
            rinfo.EnrolllistID = DataConverter.CLng(Request.Form["sid"]);
            rinfo.LogPassWord = this.txt_LogPassWord.Text;
            rinfo.PriorUserName = this.txt_PriorUserName.Text;
            rinfo.Remark = this.txt_Remark.Text;
            rinfo.Addinfo = this.txt_Addinfo.Text;
            rinfo.TechID = llinfo.UesrID;
            rinfo.Tel = this.txt_Tel.Text;
            rinfo.WriteTime = this.txt_WriteTime.Text == "" ? DateTime.Now : DataConverter.CDate(this.txt_WriteTime.Text);
            rinfo.AddTime = DateTime.Now;
            rll.GetInsert(rinfo);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功!');location.href='StudioInfoListByTech.aspx?id=" + DataConverter.CLng(Request.Form["sid"]).ToString() + "';", true);
        }
    }
}
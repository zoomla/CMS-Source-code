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


public partial class manage_Exam_UpdateExamPoint : CustomerPageAction
{
    private B_ExamPoint bep = new B_ExamPoint();
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {      

        int id = DataConverter.CLng(Request.QueryString["ID"]);
        if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "del")
        {
            int cids = DataConverter.CLng(Request.QueryString["id"]);
            bep.DeleteByGroupID(cids);
        }
        if (!IsPostBack)
        {
            //Bind();
            M_ExamPoint mep = bep.GetSelect(id);
            //if (mep != null && mep.ID > 0)
            //{
                //Label1.Text = "修改考点";
                this.txtCoureTime.Text =DataConverter.CDate(mep.AddTime).ToString();
                this.txt_End.Text = mep.OrderBy.ToString();
                //ddpoint.SelectedValue = mep.TID.ToString();
            //}
            //else
            //{
            //    Label1.Text = "添加考点";     
            //}  
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li> <li>添加考点</li>");
        }
    }
    #region 添加,修改考点

    //private void Bind()
    //{
    //    DataTable dt = bep.Select_All();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddpoint.Items.Clear();
    //        ddpoint.Items.Add(new ListItem("请选择", "0"));
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            ListItem li = new ListItem();
    //            li.Text = dr["testpoint"].ToString();
    //            li.Value = dr["ID"].ToString();
    //            ddpoint.Items.Add(li);
    //        }
    //    }
    //}

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(Request.QueryString["ID"]);
        M_ExamPoint mc = bep.GetSelect(id);
        mc.AddUser =badmin.GetAdminLogin().AdminId;
        //mc.TID =DataConverter.CLng(ddpoint.SelectedValue);
        mc.OrderBy =DataConverter.CLng(this.txt_End.Text);
        mc.AddTime = DateTime.Now;
        mc.ID = DataConverter.CLng(id);
        if (mc != null && mc.ID > 0)
        {
            bool result = bep.GetUpdate(mc);
            if (result)
            {
                function.WriteSuccessMsg("修改成功！", "ExamPointManage.aspx");
            }
            else
            {
                function.WriteErrMsg("修改失败！");
            }
        }
        //else
        //{
        //    int ids = bep.GetInsert(mc);
        //    if (ids > 0)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('添加成功！');location.href='ExamPointManage.aspx'</script>");
        //    }
        //    else
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('添加失败！');</script>");
        //    }
        //}
    }
    #endregion 
    #region 返回
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExamPointManage.aspx");
    }
    #endregion
    #region 读取用户名
    public string GetAdminName(string name)
    {
        int id = DataConverter.CLng(name);
        M_AdminInfo madmininfo = B_Admin.GetAdminByAdminId(id);
        if (madmininfo != null && madmininfo.AdminId > 0)
        {
            return madmininfo.AdminName;
        }
        else
        {
            return "";
        }
    }
    #endregion 
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;

public partial class manage_Question_CoureseManage : CustomerPageAction
{
    private B_Course bcourse = new B_Course();
    private B_Admin badmin = new B_Admin();
    private B_Exam_Class bqclass = new B_Exam_Class ();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bcourse.DeleteByGroupID(id);
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='Papers_System_Manage.aspx'>考试管理</a></li><li>课程管理<a href='AddCoures.aspx'>[添加课程]</a></li>" + Call.GetHelp(75));
        }
    }
    public void MyBind()
    {
        DataTable dt = bcourse.Select_All();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    // 是否热门
    public string GetHot(string isHot)
    {
        if (isHot == "0")
        {
            return "否";
        }
        else
        {
            return "是";
        }
    }

    public string GetClass(string classid)
    {
        M_Exam_Class mquestion = bqclass.GetSelect(DataConverter.CLng(classid));
        if (mquestion != null && mquestion.C_id > 0)
        {
            return mquestion.C_ClassName;
        }
        else
        {
            return "无分类";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string item = Request.Form["item"];
        if (item != null && item != "")
        {
            if (item.IndexOf(',') > -1)
            {
                string[] itemarr = item.Split(',');
                for (int i = 0; i < itemarr.Length; i++)
                {
                    bcourse.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                bcourse.DeleteByGroupID(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "CoureseManage.aspx");
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            //String id = EGV.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("ondblclick", "window.location.href = 'AddCoures.aspx?id=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
        }
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
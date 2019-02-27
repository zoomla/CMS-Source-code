using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_Question_Question_Class_Manage : CustomerPageAction
{
    B_Exam_Class examBll=new B_Exam_Class();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result = "";
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            switch (action)
            {
                case "GetChild":
                    result = JsonHelper.JsonSerialDataTable(examBll.GetSelectByC_ClassId(DataConverter.CLng(value)));
                    break;
                case "Del":
                    examBll.DeleteByGroupID(DataConverter.CLng(value));
                    result = "1";
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li><li>科目管理<a href='AddQuestion_Class.aspx?menu=Add&C_id=0'>[添加科目]</a></li>" + Call.GetHelp(76));
        }
    }

    public void MyBind()
    {
        RPT.DataSource = examBll.GetSelectByC_ClassId(0);
        RPT.DataBind();
    }

    public string GetIcon()
    {
        return DataConverter.CLng(Eval("ChildCount")) > 0 ? "fa fa-folder" : "fa fa-file";
    }


}

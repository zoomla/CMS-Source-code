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

public partial class manage_Question_ExamManage : CustomerPageAction
{
    protected B_Exroom exr = new B_Exroom();
    protected B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["menu"] != null)
        {
            if (Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["cid"]);
                exr.DeleteByGroupID(id);
            }
        }
        DataTable talist = exr.Select_All();
        Page_list(talist);
        Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>考场管理<a href='AddExamExroom.aspx'>[添加考场]</a></li>");
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
                    exr.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                exr.DeleteByGroupID(DataConverter.CLng(item));
            }
            function.WriteSuccessMsg("操作成功!", "ExamExroom.aspx");
        }
    }

    protected string GetExaName(string ExaID)
    {
        int intexa = DataConverter.CLng(ExaID);
        if (intexa>0)
        {
            M_Exam_Sys_Papers pinfo = bps.GetSelect(intexa);
            return pinfo.p_name;
        }
        else
        {
            return "";
        }
    }

    protected string GetStuidoNum(string studiolist)
    {
        if (studiolist != "")
        {
            if (studiolist.IndexOf(',') > -1)
            {
                return studiolist.Split(',').Length.ToString();
            }
            else
            {
                return "1";
            }
        }
        else
        {
            return "0";
        }
    }


    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        RPT.DataSource = Cll;
        RPT.DataBind();
    }
    #endregion
}
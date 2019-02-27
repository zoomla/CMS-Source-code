using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
/*
 * 前台题目管理,提供组卷,不提供删除得管理功能
 */ 
public partial class manage_Question_QuestionManage : CustomerPageAction
{
    B_Exam_Sys_Questions questBll = new  B_Exam_Sys_Questions ();
    B_Exam_Class nodeBll = new B_Exam_Class();
    B_Exam_Version verBll = new B_Exam_Version();
    B_Temp tempBll = new B_Temp();
    B_TempUser tubll = new B_TempUser();
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
          
            switch (action)
            {
                case "getknows":
                    {
                        DataTable dt = knowBll.Select_All(DataConvert.CLng(Request.Form["nodeid"]), -1, 1, Request.Form["knowname"]);
                        result = GetTreeStr(FillKnows(dt), 0, "knows");
                    }
                    break;
                case "GetKnowsByVersion":
                    {
                        int version = Convert.ToInt32(Request["version"]);
                        M_Exam_Version verMod = verBll.SelReturnModel(version);
                        if (verMod == null || string.IsNullOrEmpty(verMod.Knows)) { result = "{}"; }
                        else {
                            DataTable dt = knowBll.SelByIDS(verMod.Knows);
                            dt = dt.DefaultView.ToTable(false, "k_id,k_name".Split(','));
                            result = JsonConvert.SerializeObject(dt);
                        }
                    }
                    break;
                default:
                    break;
            }
    
            Response.Clear();
            Response.Write(result);Response.Flush();Response.End();
        }
        if (!IsPostBack)
        {
            M_UserInfo mu = tubll.GetLogin();
            M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
            if (tempMod == null) { tempMod = new M_Temp(); }
            string list = questBll.GetCountByIDS(tempMod.Str1);
            function.Script(this, "RenderQList(" + list + ");");
            QuestType_Lit.Text = GetTreeStr(FillQuest(nodeBll.SelectQuesClasses()), 0, "quest");
            MyBind();
        }
    }
    private void MyBind()
    {
        //B_KeyWord keyBll = new B_KeyWord();
        //KeyRPT.DataSource = keyBll.SelAll(2);
        //KeyRPT.DataBind();
        //年级,难度,教材
        GradeRPT.DataSource = B_GradeOption.GetGradeList(6, 0);
        GradeRPT.DataBind();
        //DiffRPT.DataSource = B_GradeOption.GetGradeList(5, 0);
        //DiffRPT.DataBind();
        VersionRPT.DataSource = verBll.Sel();
        VersionRPT.DataBind();
    }
    //树形
    string hasChild_tlp = "<li data-pid=@pid data-id=@id><span class='fa fa-plus-circle treeicon'></span><a class='filter_class @type' data-val='@id' href='javascript:;'>@name</a><ul>@childs</ul></li>";
    string childs_tlp = "<li class='lastchild' data-pid=@pid data-id=@id style='@islast'><a class='filter_class @type' data-val='@id' href='javascript:;'>@name</a></li>";

    private string GetTreeStr(DataTable dt, int pid,string type="")
    {
        string html = "";
        DataRow[] drs= dt.Select("Pid=" + pid);
        for (int i = 0; i < drs.Length; i++)
        {
            DataRow item = drs[i];
            if (dt.Select("Pid="+DataConvert.CLng(item["ID"])).Length>0)
            {
                html += hasChild_tlp.Replace("@id", item["ID"].ToString()).Replace("@name", item["NodeName"].ToString()).Replace("@pid", item["Pid"].ToString()).Replace("@type",type)
                    .Replace("@childs", GetTreeStr(dt,DataConvert.CLng(item["ID"]), type));
            }
            else
            {
                html += childs_tlp.Replace("@pid", item["Pid"].ToString()).Replace("@id", item["ID"].ToString()).Replace("@name", item["NodeName"].ToString()).Replace("@type",type).Replace("@islast",i==drs.Length-1? "background-position:0 -1766px;":"");
            }
        }
        
        return html;
    }
    //试题科目
    private DataTable FillQuest(List<M_Exam_Class> list)
    {
        DataTable dt = InitTreeTable();
        foreach (M_Exam_Class item in list)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = item.C_id;
            dr["Pid"] = item.C_Classid;
            dr["NodeName"] = item.C_ClassName;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    //知识点
    private DataTable FillKnows(DataTable dt)
    {
        DataTable treedt = InitTreeTable();
        foreach (DataRow item in dt.Rows)
        {
            DataRow dr = treedt.NewRow();
            dr["ID"] = item["k_id"];
            dr["Pid"] = item["Pid"];
            dr["NodeName"] = item["k_name"];
            treedt.Rows.Add(dr);
        }
        return treedt;
    }
    private DataTable InitTreeTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Pid");
        dt.Columns.Add("NodeName");
        return dt;
    }
}

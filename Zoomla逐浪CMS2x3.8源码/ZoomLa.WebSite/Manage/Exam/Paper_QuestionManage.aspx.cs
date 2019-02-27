using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

public partial class manage_Question_Paper_QuestionManage : CustomerPageAction
{
    private B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    private B_Exam_Type typeBll = new B_Exam_Type();
    protected B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    B_User buser = new B_User();
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    public int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.IsTeach()) { function.WriteErrMsg("只有教师可以访问当前页!"); }
        Call.HideBread(Master);
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_Exam_Sys_Papers papermodel = paperBll.GetSelect(Pid);
        PTitle_L.Text = StringHelper.SubStr(papermodel.p_name, 10);
        SelIDS_IDS.Value = "," + papermodel.QIDS + ",";
        DataTable dt = null;
        if (!string.IsNullOrEmpty(papermodel.QIDS))
        { dt = questBll.SelByIDS(papermodel.QIDS, QType, "*"); }
        EGV.DataSource = dt;
        EGV.DataBind();
        function.Script(this, "ActiveTab(" + QType + ");");
    }
    public string GetQuesType()
    {
        int questype = DataConverter.CLng(Eval("p_type"));
        return questype == 10 ? "大题" : "小题";
    }
    //取类别
    public string GetClass(string classid)
    {
        int id = DataConverter.CLng(classid);
        B_Exam_Class bec = new B_Exam_Class();
        M_Exam_Class mec = bec.GetSelect(id);
        if (mec != null && mec.C_id > 0)
        {
            return mec.C_ClassName;
        }
        else
        {
            return "";
        }

    }
    //取题型
    public string GetType(string id1)
    {
        return M_Exam_Sys_Questions.GetTypeStr(DataConverter.CLng(id1));
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Dels_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            M_Exam_Sys_Papers papermodel = paperBll.GetSelect(Pid);
            string paper_qids = "," + papermodel.QIDS + ",";
            string[] quesids = Request.Form["idchk"].Split(',');
            foreach (var quesid in quesids)
            {
                if (paper_qids.Contains("," + quesid + ","))
                    paper_qids = paper_qids.Replace("," + quesid + ",", ",");   
            }
            papermodel.QIDS = paper_qids.Trim(',');
            paperBll.UpdateByID(papermodel);
        }
        MyBind();

    }
}
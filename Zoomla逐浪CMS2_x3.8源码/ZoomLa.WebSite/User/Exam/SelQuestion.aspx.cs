using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.Components;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data;
using Newtonsoft.Json;
using ZoomLa.BLL.Helper;

public partial class Manage_Exam_SelQuestion : System.Web.UI.Page
{
    private B_Exam_Type typeBll = new B_Exam_Type();
    private B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    protected B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    public int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
    public int IsLage { get { return DataConverter.CLng(Request.QueryString["islage"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (buser.IsTeach() || badmin.CheckLogin()) { }
        else { function.WriteErrMsg("当前页面只有教师才可访问"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind(string keyword = "")
    {
        M_UserInfo mu = buser.GetLogin();
        int issmall = 0;
        if (IsLage == 1)
        {
            issmall = 1;
            function.Script(this, "DisTab(10);");
            Sel_B.Visible = false;
            LargeSel_B.Visible = true;
        }
        //checkbox已选择的
        if (string.IsNullOrEmpty(SelIds_Hid.Value))
        { SelIds_Hid.Value = Request.QueryString["selids"]; }
        if (badmin.CheckLogin()) { EGV.DataSource = questBll.U_SelByFilter(0,QType, keyword,0, issmall); }
        else { EGV.DataSource = questBll.U_SelByFilter(0,QType, keyword, mu.UserID, issmall); }
        EGV.DataBind();
        function.Script(this, "ActiveTab(" + QType + ");");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
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
    //判断是否已选该试题
    public string GetCheck()
    {
        if (SelIds_Hid.Value.Contains(","+Eval("p_id")+","))
        {
            return "<input type='checkbox' name='idchk' checked='checked' value='" + Eval("p_id") + "'/>";
        }else{
            return "<input type='checkbox' name='idchk' value='" + Eval("p_id") + "'/>";
        }
    }
    //取题型
    public string GetType(string id)
    {
        return M_Exam_Sys_Questions.GetTypeStr(DataConverter.CLng(id));
    }
    protected void Sel_B_Click(object sender, EventArgs e)
    {
        M_Exam_Sys_Papers pmodel = paperBll.GetSelect(Pid);
        pmodel.QIDS = string.Join(",", SelIds_Hid.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        paperBll.UpdateByID(pmodel);
        function.Script(this, "parent.CLoseDIag();");
    }
    //大题添加小题
    protected void LargeSel_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(SelIds_Hid.Value))
        {
            DataTable dt = questBll.SelByIDSForType(SelIds_Hid.Value);
            string json = JsonConvert.SerializeObject(dt);
            function.Script(this, "parent.SelQuestion(" + json + ");");
        }
    }
   
    protected void Search_B_Click(object sender, EventArgs e)
    {
        MyBind(KeyWord_T.Text);
    }
}
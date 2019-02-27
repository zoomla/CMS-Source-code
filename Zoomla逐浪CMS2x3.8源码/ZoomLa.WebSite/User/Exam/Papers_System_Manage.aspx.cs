using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.Helper;

public partial class manage_Question_Papers_System_Manage : CustomerPageAction
{
    B_Exam_Class classBll=new B_Exam_Class();
    B_Exam_Sys_Papers paperBll=new B_Exam_Sys_Papers();
    B_User buser = new B_User();
    private DataTable classDT
    {
        get
        {
            if (ViewState["classdt"] == null)
            {
                ViewState["classdt"] = classBll.Select_All();
            }
            return ViewState["classdt"] as DataTable;
        }
        set { ViewState["classdt"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        classDT = null;
    }
    private void MyBind()
    {
        this.EGV.DataSource = paperBll.Sel();
        this.EGV.DataBind();
    }
    public string GetStyle(string type)
    {
        return paperBll.GetPType(type);
    }
    public string GetModus(string type)
    {
        if (type == "1")
        {
            return "固定试卷（手工）";
        }
        if (type == "2")
        {
            return "固定试卷（随机）";
        }
        if (type == "3")
        {
            return "随机试卷";
        }
        else
        {
            return "";
        }
    }
    public string GetClass()
    {
        int classid = DataConverter.CLng(Eval("p_class"));
        DataRow[] drs = classDT.Select("C_id="+classid);
        return drs.Length > 0 ? drs[0]["C_ClassName"].ToString() : "";
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            paperBll.DelByIDS(ids);
            MyBind();
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Combine_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (string.IsNullOrEmpty(ids)) function.WriteErrMsg("请先选定需要合并的试卷");
        M_UserInfo mu=buser.GetLogin();
        DataTable dt = paperBll.SelByIDS(ids);
        if (dt.Rows.Count < 1) { function.WriteErrMsg("选定的试卷不存在"); }
        M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
        paperMod.UserID = mu.UserID;
        paperMod.p_name = DateTime.Now.ToString("yyyyMMdd")+"合并试卷";
        paperMod.QIDS = "";
        foreach (DataRow dr in dt.Rows)
        {
            paperMod.QIDS += dr["QIDS"] + ",";
        }
        paperMod.p_type = Convert.ToInt32(dt.Rows[0]["p_type"]);
        paperMod.p_class = Convert.ToInt32(dt.Rows[0]["p_class"]);
        paperMod.QIDS = StrHelper.RemoveDupByIDS(paperMod.QIDS);
        paperMod.QIDS = StrHelper.PureIDSForDB(paperMod.QIDS);
        paperBll.Insert(paperMod);
        function.WriteSuccessMsg("试卷合并成功");
    }
}

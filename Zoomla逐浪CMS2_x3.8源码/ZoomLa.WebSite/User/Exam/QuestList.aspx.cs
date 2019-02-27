using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
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
using ZoomLa.BLL.Helper;

public partial class User_Exam_QuestList : System.Web.UI.Page
{
    public B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    B_Exam_Class bqc = new B_Exam_Class();
    B_ExamPoint bep = new B_ExamPoint();
    B_User buser = new B_User();
    M_Exam_Sys_Questions questMod = new M_Exam_Sys_Questions();
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();

    public int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
    //试题类别
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    public string Skey { get { return Skey_T.Text; } set { Skey_T.Text = value; } }
    public DataTable KnowsNames = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindNode();
        if (!IsPostBack)
        {
            Skey_T.Text = Request.QueryString["Skey"];
            MyBind();
        }
    }
    private void MyBind()
    {
        if (NodeID > 0)
        {
            AddQuest_L.Text = "<a href='AddEnglishQuestion.aspx?NodeID=" + NodeID + "'>[添加试题]</a>";
        }
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = questBll.U_SelByFilter(NodeID,QType, Skey, mu.UserID,0);
        EGV.DataSource = dt;
        EGV.DataBind();
        KnowsBind();
    }
    public void BindNode()
    {
        DataTable dt = bqc.Select_All();
        MyTree.liAllTlp = "<a href='QuestList.aspx'>全部内容</a>";
        MyTree.LiContentTlp = "<a href='QuestList.aspx?NodeID=@NodeID'>@NodeName</a>";
        MyTree.SelectedNode = NodeID;//选中节点
        MyTree.DataSource = dt;
        MyTree.DataBind();
    }
    public string GetTitle()
    {
        return function.GetStr(Eval("p_title").ToString(), 10);
    }
    //绑定知识点名称
    private void KnowsBind()
    {
        string knowsids = "";
        foreach (DataKey key in EGV.DataKeys)
        {
            knowsids += key.Value + ",";
        }
        KnowsNames = knowBll.SelByIDS(StrHelper.PureIDSForDB(knowsids));
        EGV.DataBind();
    }

    //批量删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            questBll.DelByIDS(ids);
            MyBind();
        }
    }
    //取类别
    public string GetClass(string classid)
    {
        int id = DataConverter.CLng(classid);
        M_Exam_Class mec = bqc.GetSelect(id);
        if (mec != null && mec.C_id > 0)
        {
            return mec.C_ClassName;
        }
        else
        {
            return "";
        }

    }
    public string GetTagKeys()
    {
        if (KnowsNames != null && KnowsNames.Rows.Count > 0 && !string.IsNullOrEmpty(Eval("Tagkey").ToString()))
        {
            string knownames = "";
            DataRow[] drs = KnowsNames.Select("k_id IN (" + Eval("Tagkey").ToString().Trim(',') + ")");
            foreach (DataRow item in drs)
            {
                knownames += item["k_name"].ToString() + ",";
            }
            string names = knownames.Trim(',');
            names = names.Length > 10 ? names.Substring(0, 10) + "..." : names;
            return names.Length > 10 ? names.Substring(0, 10) + "..." : names;
        }
        return "";
    }
    //取考点
    public string GetTestPoint(string ids)
    {
        int id = DataConverter.CLng(ids);
        bep = new B_ExamPoint();
        M_ExamPoint mep = bep.GetSelect(id);
        if (mep != null && mep.ID > 0)
        {
            return mep.TestPoint;
        }
        else
        {
            return "";
        }
    }
    //取题型
    public string GetType(string id)
    {
        return M_Exam_Sys_Questions.GetTypeStr(DataConverter.CLng(id));
    }
    // 获取内容
    private string GetCon(string con)
    {
        if (con.Length > 50)
        {
            return con.Substring(0, 40) + "...";
        }
        else
        {
            return con;
        }
    }
    // 导入数据
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExportExcel.aspx");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Search_B_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?p_id=" + dr["p_id"] + "'");
            Label prebtn = e.Row.FindControl("prebtn") as Label;
            prebtn.Text = "<a href='QuestShow.aspx?id=" + dr["p_id"] + "' title='浏览' target='_blank'><span class='fa fa-globe'></span>浏览</a>";
            if (DataConverter.CLng(dr["UserID"]) == buser.GetLogin().UserID)
            {
                prebtn.Text = "<a href='QuestShow.aspx?id=" + dr["p_id"] + "' title='浏览' target='_blank'><span class='fa fa-globe'></span></a>";
                e.Row.FindControl("delbtn").Visible = true;
                e.Row.FindControl("editbtn").Visible = true;
            }
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        switch (e.CommandName)
        {
            case "edit2":
                Response.Redirect("AddEngLishQuestion.aspx?id=" + e.CommandArgument);
                break;
            case "del2":
                questBll.U_DelByIDS(e.CommandArgument.ToString(), mu.UserID);
                MyBind();
                break;
        }
    }
}
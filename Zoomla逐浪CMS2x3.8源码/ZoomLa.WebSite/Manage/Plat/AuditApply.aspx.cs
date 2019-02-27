using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class Manage_Plat_AuditApply : System.Web.UI.Page
{
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_Plat_Group gpBll = new B_Plat_Group();
    B_User buser = new B_User();
    B_User_Plat upBll = new B_User_Plat();
    B_Common_UserApply ualyBll = new B_Common_UserApply();
    public int ZStatus { get { return DataConvert.CLng(Request.QueryString["s"]); } }
    private string ztype = "plat_applyopen";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + Request.RawUrl + "'>能力中心</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a></li><li><a href='" + Request.RawUrl + "'>申请管理</a></li>");
        }
    }
    public void MyBind()
    {
        EGV.DataSource = ualyBll.Search(ztype, "", "", ZStatus, 0);
        EGV.DataBind();
        switch (ZStatus)
        {
            case (int)ZLEnum.ConStatus.UnAudit:
                break;
            //case (int)ZLEnum.ConStatus.Audited:
            //    break;
            //case (int)ZLEnum.ConStatus.Reject:
            //    break;
            default:
                BatReject_Btn.Visible = false;
                BatAgree_Btn.Visible = false;
                break;
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "agree":
                break;
            case "reject":
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView dr = e.Row.DataItem as DataRowView;
        //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
        //}
    }
    protected void BatAgree_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        //修改公司状态,并更正名称
        DataTable dt = ualyBll.Search(ztype, ids, "", (int)ZLEnum.ConStatus.UnAudit, 0);
        foreach (DataRow dr in dt.Rows)
        {
            M_Common_UserApply ualyMod = new M_Common_UserApply().GetModelFromReader(dr);
            M_UserInfo mu = buser.SelReturnModel(ualyMod.UserID);
            //-------------创建公司和用户
            M_User_Plat upMod = upBll.NewUser(mu);
            M_Plat_Comp compMod = compBll.CreateByUser(upMod);
            compMod.CompName = ualyMod.CompName;
            compMod.CompShort = ualyMod.Info1;
            compMod.Telephone = ualyMod.Mobile;
            compMod.Mobile = ualyMod.Mobile;
            compMod.Status = 1;
            compBll.UpdateByID(compMod);
            upBll.Insert(upMod);
            //-------------创建部门
            DataTable userDT = GetUserDT();
            upBll.NewByUserDT(compMod, userDT);
        }
        ualyBll.ChangeByIDS(ids, (int)ZLEnum.ConStatus.Audited);
        function.WriteSuccessMsg("批量同意完成");
    }
    protected void BatReject_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        ualyBll.ChangeByIDS(ids, (int)ZLEnum.ConStatus.Reject);
        function.WriteSuccessMsg("批量拒绝完成");
    }
    private DataTable GetUserDT()
    {
        string[] groups = "证照管理,行政人事,经营管理,项目管理,财务部".Split(',');
        string name = function.GetRandomString(6, 3).ToLower();
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("gname", typeof(string)));
        dt.Columns.Add(new DataColumn("uname", typeof(string)));
        dt.Columns.Add(new DataColumn("honey", typeof(string)));
        for (int i = 0; i < groups.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr["gname"] = groups[i];
            dr["uname"] = name + "_0" + i;
            dr["honey"] = "";
            dt.Rows.Add(dr);
        }
        return dt;
    }
}
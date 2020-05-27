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

namespace ZoomLaCMS.Manage.Plat
{
    public partial class AuditApply : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_Common_UserApply ualyBll = new B_Common_UserApply();
        public int ZStatus { get { return DataConvert.CLng(Request.QueryString["s"]); } }
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
            EGV.DataSource = ualyBll.Search("plat_applyopen", "", "", ZStatus, 0);
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
            DataTable dt = ualyBll.CompCert_Sel((int)ZLEnum.ConStatus.UnAudit, ids);
            foreach (DataRow dr in dt.Rows)
            {
                M_Plat_Comp compMod = new M_Plat_Comp();
                int compID = DataConvert.CLng(JsonHelper.GetVal(DataConvert.CStr(dr["UserRemind"]), "compid"));
                compMod = compBll.SelReturnModel(compID);
                compMod.CompName = DataConvert.CStr(dr["Remind"]);
                compMod.CompShort = JsonHelper.GetVal(DataConvert.CStr(dr["UserRemind"]), "compshort");
                compMod.Telephone = JsonHelper.GetVal(DataConvert.CStr(dr["UserRemind"]), "telephone");
                compMod.Mobile = JsonHelper.GetVal(DataConvert.CStr(dr["UserRemind"]), "mobile");
                compMod.Status = 1;
                compBll.UpdateByID(compMod);
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
    }
}
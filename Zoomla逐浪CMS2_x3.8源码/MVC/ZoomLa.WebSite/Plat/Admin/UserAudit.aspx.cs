using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Admin
{
    public partial class UserAudit : System.Web.UI.Page
    {
        M_User_Plat upMod = new M_User_Plat();
        B_Common_UserApply ualyBll = new B_Common_UserApply();
        M_Common_UserApply ualyMod = null;
        public int ZStatus { get { return DataConvert.CLng(Request.QueryString["s"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            upMod = B_User_Plat.GetLogin();
            EGV.DataSource = ualyBll.JoinComp_Sel(upMod.CompID.ToString(), ZStatus);
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
                    EGV.Columns[(EGV.Columns.Count - 1)].Visible = false;
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
            int id = DataConvert.CLng(e.CommandArgument);
            ualyMod = ualyBll.SelReturnModel(id);
            switch (e.CommandName)
            {
                case "agree":
                    {
                        upMod = B_User_Plat.GetLogin();
                        DBCenter.UpdateSQL("ZL_User_Plat", "CompID=" + upMod.CompID + ",Plat_Role=''", "UserID=" + ualyMod.UserID);
                        ualyBll.ChangeByIDS(id.ToString(), (int)ZLEnum.ConStatus.Audited);
                    }
                    break;
                case "reject":
                    {
                        ualyBll.ChangeByIDS(id.ToString(), (int)ZLEnum.ConStatus.Reject);
                    }
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (ZStatus != 0) 
            //    {

            //    }
            //}
        }
        protected void BatAgree_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            upMod = B_User_Plat.GetLogin();
            //修改选定的会员的公司信息
            DataTable dt = ualyBll.JoinComp_Sel(upMod.CompID.ToString(), (int)ZLEnum.ConStatus.UnAudit, ids);
            foreach (DataRow dr in dt.Rows)
            {
                DBCenter.UpdateSQL("ZL_User_Plat", "CompID=" + upMod.CompID + ",Plat_Role=''", "UserID=" + dr["UserID"]);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

/*
 * 浏览已归档公文或文件
 */

namespace ZoomLaCMS.MIS.OA.FreeFlow
{
    public partial class FlowView : System.Web.UI.Page
    {
        B_Permission perBll = new B_Permission();
        B_OA_Document oaBll = new B_OA_Document();
        B_OA_Borrow borBll = new B_OA_Borrow();
        B_MisProcedure proceBll = new B_MisProcedure();
        B_Content conBll = new B_Content();
        M_Mis_AppProg progMod = new M_Mis_AppProg();
        B_Mis_AppProg progBll = new B_Mis_AppProg();
        B_User buser = new B_User();
        //根据需求扩展,是否未归档文件也可查看
        private string action { get { return Request.QueryString["action"]; } }
        public int appID
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["AppID"]);
            }
        }
        public int ModelID { get { return DataConvert.CLng(ViewState["ModelID"]); } set { ViewState["ModelID"] = value; } }
        private string ascx { get { return ViewState["ascx"] as string; } set { ViewState["ascx"] = value; } }
        private B_OAFormUI OAFormUI
        {
            get
            {
                var control = OAForm_Div.FindControl("ascx_" + ascx);
                if (control == null)//加载默认
                {
                    control = OAForm_Div.FindControl("ascx_def");
                    control.Visible = true;
                    return (B_OAFormUI)control;
                }
                else { control.Visible = true; return (B_OAFormUI)control; }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            M_OA_Document oaMod = oaBll.SelReturnModel(appID);
            if (oaMod.Status != (int)ZLEnum.ConStatus.Filed) { function.WriteErrMsg("该文件尚未归档,无法查看!"); }
            //验证是否拥有档案管理员或已有借阅权限
            M_UserInfo mu = buser.GetLogin();
            if (perBll.CheckAuth(mu.UserRole, "oa_pro_file") || borBll.HasAuth(mu.UserID, appID))
            {

            }
            else
            {
                function.WriteErrMsg("该文件已归档,你没有对应的查看权限!");
            }
            MyBind(oaMod);
        }
        public void MyBind(M_OA_Document oaMod)
        {
            M_UserInfo mu = buser.GetLogin();
            SendMan_L.Text = oaMod.UserName;
            stepNameL.Text = "已完结";
            ascx = proceBll.SelReturnModel(oaMod.ProID).FlowTlp;
            OAFormUI.SendDate_ASCX = oaMod.SendDate.ToString();
            createTimeL.Text = oaMod.SendDate.ToString("yyyy年MM月dd日 HH:mm");
            ModelID = Convert.ToInt32(proceBll.SelReturnModel(oaMod.ProID).FormInfo);
            DataTable dtContent = conBll.GetContent(Convert.ToInt32(oaMod.Content));
            OAFormUI.InitControl(ViewState, ModelID);
            OAFormUI.dataRow = dtContent.Rows[0];
            OAFormUI.MyBind();
            OAFormUI.Title_ASCX = oaMod.Title;
            OAFormUI.NO_ASCX = oaMod.No;
            OAFormUI.No_ASCX_T.Enabled = false;
        }
        public void AppProgBind()
        {
            DataTable dt = new DataTable();
            dt = progBll.SelByAppID(appID.ToString());
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        public string GetResult(object o)
        {
            return progMod.GetResult(DataConvert.CLng(o.ToString()));
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            AppProgBind();
        }
    }
}
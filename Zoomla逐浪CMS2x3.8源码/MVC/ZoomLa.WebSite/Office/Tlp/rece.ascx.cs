using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Tlp
{
    public partial class rece : ZoomLa.BLL.MIS.B_OAFormUI
    {
        B_User buser = new B_User();
        B_Permission perBll = new B_Permission();
        B_Mis_AppProg progBll = new B_Mis_AppProg();
        private int AppID { get { return DataConvert.CLng(Request.QueryString["AppID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                DataTable authdt = perBll.SelAuthByRoles(mu.UserRole);
                RPT.DataSource = progBll.SelHQDT(AppID, 1);
                RPT.DataBind();
                // No_T.Enabled = perBll.CheckAuth(authdt,"oa_pro_no");
            }
        }
        public string GetHQInfo(int stepnum)
        {
            if (AppID < 1) { return ""; }
            return progBll.SelHQInfo(AppID, stepnum);
        }
        //public DataTable GetHQDT(int stepnum)
        //{

        //}
        public string GetResult()
        {
            return new M_Mis_AppProg().GetResult(DataConvert.CLng(Eval("Result", "")));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Tlp
{
    public partial class receprint :B_OAFormUI
    {
        B_Mis_AppProg progBll = new B_Mis_AppProg();
        private int AppID { get { return DataConvert.CLng(Request.QueryString["AppID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RPT.DataSource = progBll.SelHQDT(AppID, 1);
                RPT.DataBind();
            }
        }
        public string GetHQInfo(int stepnum)
        {
            if (AppID < 1) { return ""; }
            B_Mis_AppProg progBll = new B_Mis_AppProg();
            return progBll.SelHQInfo(AppID, stepnum);
        }
        public string GetResult()
        {
            return new M_Mis_AppProg().GetResult(DataConvert.CLng(Eval("Result", "")));
        }
    }
}
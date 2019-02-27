using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data;

namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class ImgWorkFlow : System.Web.UI.Page
    {
        protected B_MisProLevel stepBll = new B_MisProLevel();
        B_MisProcedure prodBll = new B_MisProcedure();
        B_User buser = new B_User();
        B_Admin badmin = new B_Admin();
        public int ProID { get { return DataConvert.CLng(Request.QueryString["proid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (buser.CheckLogin() || badmin.CheckLogin())
                    MyBind();
                else
                    function.WriteErrMsg("您没有登录!");
            }
        }
        public void MyBind()
        {
            DataTable dt = stepBll.SelByProID(ProID);
            dt.Columns["stepName"].ColumnName = "StepName";
            ImgData_Hid.Value = JsonHelper.JsonSerialDataTable(dt);
        }
    }
}
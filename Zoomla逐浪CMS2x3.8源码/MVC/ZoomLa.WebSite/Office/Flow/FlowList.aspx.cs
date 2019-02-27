using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class FlowList : System.Web.UI.Page
    {
        B_MisProcedure proceBll = new B_MisProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            Egv.DataSource = proceBll.Sel();
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetAllowAttach(string allowAttach)
        {
            switch (allowAttach)
            {
                case "0":
                    return "不允许";
                case "1":
                    return "允许";
                default:
                    return "";
            }
        }
    }
}
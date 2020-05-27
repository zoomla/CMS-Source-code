using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class SelModelFieds : System.Web.UI.Page
    {
        B_Model Bll = new B_Model();
        B_ModelField FieldBll = new B_ModelField();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["mid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.HideBread(this.Master);
                MyBind();
            }
        }
        public void MyBind()
        {
            if (Mid > 0)
            {
                RPT.Visible = false;
                DataTable tablelist = FieldBll.SelByModelID(Mid);
                tablelist.DefaultView.RowFilter = "Sys_type='False'";
                this.Fied_RPT.DataSource = tablelist.DefaultView.ToTable();
                this.Fied_RPT.DataBind();
            }
            else
            {
                DataTable dt = this.Bll.GetModel("'" + Bll.GetModelType(12) + "'", "");
                RPT.DataSource = dt;

                RPT.DataBind();
            }

        }
    }
}
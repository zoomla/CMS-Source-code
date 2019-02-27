using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User.Group
{
    public partial class GroupSort : System.Web.UI.Page
    {
        B_Group gpBll = new B_Group();
        private int Pid { get { return DataConvert.CLng(Request.QueryString["Pid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Pid < 0) { function.WriteErrMsg("参数不正确"); }
                DataTable dt = gpBll.Sel(Pid);
                dt.Columns["GroupID"].ColumnName = "ID";
                RPT.DataSource = dt;
                RPT.DataBind();
                Call.HideBread(Master);
            }
        }
        protected void BatOrder_Btn_Click(object sender, EventArgs e)
        {
            string[] idArr = (Request.Form["idchk"] ?? "").Split(',');
            for (int i = 0; i < idArr.Length; i++)
            {
                int id = Convert.ToInt32(idArr[i]);
                gpBll.UpdateOrder(id, Convert.ToInt32(Request.Form["idtxt_" + id]));
            }
            function.WriteSuccessMsg("排序更新完成");
        }
    }
}
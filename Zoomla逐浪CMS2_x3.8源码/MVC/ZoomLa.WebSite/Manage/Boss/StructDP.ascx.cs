using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Boss
{
    public partial class StructDP : System.Web.UI.UserControl
    {
        public delegate void DeleMyBind();
        public DeleMyBind MyBind = null;
        public int StructID { get { return DataConvert.CLng(Struct_DP.SelectedValue); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init();
            }
        }
        //------------
        public new void Init()
        {
            string sql = "SELECT ID,Name FROM ZL_Structure";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            Struct_DP.DataSource = dt;
            Struct_DP.DataBind();
            Struct_DP.Items.Insert(0, new ListItem("全部用户", "0"));
        }
        protected void Struct_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}
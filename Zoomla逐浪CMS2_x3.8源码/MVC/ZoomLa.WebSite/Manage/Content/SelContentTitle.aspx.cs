using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.Helper;


namespace ZoomLaCMS.Manage.Content
{
    public partial class SelContentTitle : System.Web.UI.Page
    {
        B_Content contentBll = new B_Content();
        public string GID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GID = Request.QueryString["ids"] ?? "";
                MyBind();
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            DataTable dt = contentBll.Search(ImgName_T.Text);
            Content_RPT.DataSource = dt;
            Content_RPT.DataBind();
            ContentCount_Hid.Value = dt.Rows.Count.ToString();

            if (!string.IsNullOrEmpty(GID.Replace(",", "")))//已关联的内容
            {
                string ids = StrHelper.PureIDSForDB(GID);
                DataTable tempdt = contentBll.Search("", ids);
                foreach (DataRow dr in tempdt.Rows)
                {
                    ids_hid.Value += "," + dr["GeneralID"] + ",";
                }
                GID = "";
            }
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}
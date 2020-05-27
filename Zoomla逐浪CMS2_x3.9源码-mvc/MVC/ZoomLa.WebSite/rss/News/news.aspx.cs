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

namespace ZoomLaCMS.rss.News
{
    public partial class news : System.Web.UI.Page
    {
        M_CommonData conMod = new M_CommonData();
        B_Content conBll = new B_Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            conMod = conBll.GetCommonData(id);
            Title_L.Text = conMod.Title;
            CDate_L.Text = conMod.CreateTime.ToString("yyyy年MM月dd日");
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_C_Article Where ID=" + conMod.ItemID);
            content_div.InnerHtml = dt.Rows[0]["Content"].ToString();
        }
    }
}
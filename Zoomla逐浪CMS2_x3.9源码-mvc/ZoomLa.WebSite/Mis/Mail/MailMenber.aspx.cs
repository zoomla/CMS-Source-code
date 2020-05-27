using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.Mail
{
    public partial class MailMenber : System.Web.UI.Page
    {
        B_User buser = new B_User();
        DataTable dt = new DataTable();
        string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["type"];
            buser.CheckIsLogin(Request.Url.LocalPath);
        }
        protected void Bind()
        {
            DataTable dt = new DataTable();
            B_Structure bst = new B_Structure();
            dt = bst.Sel("UserID=" + buser.GetLogin().UserID + " And [Group]=2", "ID desc");
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        #endregion
    }
}
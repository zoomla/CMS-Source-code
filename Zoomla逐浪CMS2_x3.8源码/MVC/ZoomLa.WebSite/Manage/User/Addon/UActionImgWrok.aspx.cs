using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Chat;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.User.Addon
{
    public partial class UActionImgWrok : System.Web.UI.Page
    {

        B_UAction actionBll = new B_UAction();
        B_User buser = new B_User();
        public string IDFlag { get { return Request.QueryString["idflag"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = actionBll.SelByFlag(IDFlag);
            dt.Columns["title"].ColumnName = "StepName";
            code.Value = JsonConvert.SerializeObject(dt.Rows);//JsonHelper.JsonSerialDataTable(dt);
        }
    }
}
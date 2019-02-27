using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Project;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Note.Diag
{
    public partial class Progress : System.Web.UI.Page
    {
        M_Pro_Progress progMod = new M_Pro_Progress();
        B_Pro_Progress progBll = new B_Pro_Progress();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        public int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
        public string Action { get { return Request.QueryString["action"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
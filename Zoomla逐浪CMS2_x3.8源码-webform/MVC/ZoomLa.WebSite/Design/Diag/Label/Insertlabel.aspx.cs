using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Design.Diag.Label
{
    public partial class Insertlabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(CustomerPageAction.customPath + "Template/InsertLabel.aspx?n=" + Request.QueryString["n"]);
        }
    }
}
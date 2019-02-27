using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Design_Diag_Label_Insertlabel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(CustomerPageAction.customPath + "Template/InsertLabel.aspx?n=" + Request.QueryString["n"]);
    }
}
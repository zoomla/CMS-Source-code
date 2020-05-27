using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Design_Diag_Label_edit : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Permission.CheckAuthEx("design");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Design_Diag_Text_edit : System.Web.UI.Page
{    
    /*
     * 
     * 所有操作直接针对html标签,完毕后,再更新model层
     */
    public string Mid { get { return Request.QueryString["ID"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
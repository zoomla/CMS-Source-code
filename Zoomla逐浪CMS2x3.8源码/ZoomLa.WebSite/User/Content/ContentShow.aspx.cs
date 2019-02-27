using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Content_ContentShow : System.Web.UI.Page
{
    //仅用于商品页跳转
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("MyProduct.aspx?NodeID=" + Request.QueryString["NodeID"]);
    }
}
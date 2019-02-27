using System;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
public partial class User_MySubscription : System.Web.UI.Page
{
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    private void MyBind()
    {

    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
}

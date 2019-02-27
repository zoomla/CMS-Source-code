using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using System.Data;
using ZoomLa.BLL;

public partial class Plat_Blog_Project : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/User/Content/Note/ProList.aspx");
    }
}
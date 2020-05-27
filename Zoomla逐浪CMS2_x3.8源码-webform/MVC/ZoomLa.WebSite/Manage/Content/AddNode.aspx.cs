using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using ZoomLa.Components;
using System.Text;

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddNode :CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("EditNode.aspx?" + Request.QueryString);
        }
    }
}
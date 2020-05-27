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

namespace FreeHome.FriendSearch
{
        public partial class FriendSearch_result : System.Web.UI.Page
        {
            private string sextext = null;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    sextext = Request.Form["RadioButtonList1"].ToString();
                    this.Label1.Text = sextext;
                }
            }
        }

}

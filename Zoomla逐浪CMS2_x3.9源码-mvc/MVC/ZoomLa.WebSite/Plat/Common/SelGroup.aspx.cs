using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Plat.Common
{
    public partial class SelGroup : System.Web.UI.Page
    {
        public string GroupType { get { return Request.QueryString["type"] ?? ""; } }
        public string Source { get { return Request.QueryString["plat"] ?? ""; } }
        //selgroup.aspx?source=plat#atuser
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            platgroup_tab.Visible = true;
        }
    }
}
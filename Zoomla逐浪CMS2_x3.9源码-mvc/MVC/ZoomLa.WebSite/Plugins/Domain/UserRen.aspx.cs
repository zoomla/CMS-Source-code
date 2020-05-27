namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;
    using ZoomLa.Model;

    /*
     * IDC续费页面，接受订单的OrderNo
     * 续费后,原订单终止并到期,新订单重新生效
     */
    public partial class UserRen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
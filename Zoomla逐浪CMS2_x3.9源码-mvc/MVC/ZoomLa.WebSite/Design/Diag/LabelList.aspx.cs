using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
/*
 * 当前页面所使用的标签列表,并提供,管理,删除功能
 */

namespace ZoomLaCMS.Design.Diag
{
    public partial class LabelList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                //Call.HideBread(Master);
            }
        }
    }
}
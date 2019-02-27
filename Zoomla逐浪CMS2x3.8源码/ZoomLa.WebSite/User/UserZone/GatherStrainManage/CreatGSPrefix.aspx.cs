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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;

namespace ZoomLa.GatherStrainManage
{
    public partial class CreatGSPrefix : Page 
    {
        B_User ubll = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;
using System.Xml;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class Default : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Server.Transfer(customPath + "WeiXin/Home.aspx");
            }
        }
    }
}
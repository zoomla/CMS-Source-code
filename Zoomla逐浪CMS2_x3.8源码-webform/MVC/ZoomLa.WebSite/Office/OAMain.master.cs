﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Mis
{
    public partial class OAMain : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
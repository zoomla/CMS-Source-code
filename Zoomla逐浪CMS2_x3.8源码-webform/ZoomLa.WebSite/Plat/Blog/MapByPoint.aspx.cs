﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

public partial class Plat_Blog_MapByPoint : System.Web.UI.Page
{
    public string Point { get { return Request.QueryString["Point"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Point)) { function.WriteErrMsg("参数不正确"); }
    }
}
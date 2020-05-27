using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

public partial class Common_Label_Map_SimpBaiduMap : System.Web.UI.Page
{
    public string Point
    {
        get
        {
            string _point = Request.QueryString["Point"];
            if (string.IsNullOrEmpty(_point)) { _point = "116.404,39.915"; }
            return _point;
        }
    }
    public string Field { get { return Request.QueryString["Field"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
}
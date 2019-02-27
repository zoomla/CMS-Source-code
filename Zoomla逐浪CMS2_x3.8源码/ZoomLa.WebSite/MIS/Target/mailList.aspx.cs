using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class MIS_Target_mailList : System.Web.UI.Page
{
    B_User buser = new B_User();
    private int id { get { return DataConverter.CLng(Request["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        if (!IsPostBack)
        {
            if (id > 0)
            {

            }
        }
    }
}
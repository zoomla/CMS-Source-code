using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public partial class Edit_Submission : System.Web.UI.Page
{
    protected B_ModelField bfield = new B_ModelField();
    B_User b_User = new B_User();
    B_Node bnode = new B_Node();
    protected void Page_Load(object sender, EventArgs e)
    {
        b_User.CheckIsLogin();
        if (Request["id"] != "")
        {
            GetContents(Convert.ToInt32(Request["id"]));
        }
    }

    private void GetContents(int id)
    {
       
    }

}
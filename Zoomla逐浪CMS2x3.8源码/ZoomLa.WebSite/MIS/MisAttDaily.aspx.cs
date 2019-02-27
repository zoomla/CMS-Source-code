using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MIS_MisAttDaily : System.Web.UI.Page
{
    B_MisAttendance Battendance = new B_MisAttendance();
    B_User buser = new B_User();
    M_UserInfo us = new M_UserInfo();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected string[] Getyear()
    {
        return "".Split(',');
    }

    protected string[] Getmonth()
    {
        return "".Split(',');
    }

    protected string[] Getday()
    {
        return "".Split(',');
    }

    protected string[] Gethid()
    {
        return "".Split(',');
    }

    protected string[] Gethidmonth()
    {
        return "".Split(',');
    }

    protected string[] Gethidyear()
    {
        return "".Split(',');
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class MIS_MisInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected string GetStatus(int stu)
    {
        if (stu == 1)
        {
            return "结束"; 
        }
        else
        {
            return "未结束";
        }
    }
    protected void Repeater1_ItemCommand(object obj,RepeaterCommandEventArgs e)
    {
       
    }
    protected void Repeater1_ItemBind(object sender, RepeaterItemEventArgs e)
    {
       
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using ZoomLa.Model;
using ZoomLa.BLL;


public partial class InputUrl : CustomerPageAction
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>��չ����</li><li>IP����</li><li>Url����</li>");
    }
    protected void add_Click(object sender, EventArgs e)
    {
      
        
    }
}

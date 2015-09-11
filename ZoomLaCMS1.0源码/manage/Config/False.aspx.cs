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

public partial class manage_Config_False : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = this.Request.Params["message"].Trim().ToString();//Trim()移出开头末尾的空格
        Label2.Text = "错误信息：" + s;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}

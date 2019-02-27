using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;

public partial class Manage_Copyright_BqyReister : System.Web.UI.Page
{
    private int type { get { return DataConvert.CLng(Request.QueryString["type"] ?? ""); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='"+ CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Register.aspx\">版权中心</a></li><li class=\"active\">版权配置</li>");
        if (!IsPostBack)
        {
            if (true)//判断是否配置了版权印Appid
            {
                Prompt_Div.Visible = true;
                
            }
            if (type > 0)
            {
                Register_Div.Visible = false;
                Config_Div.Visible = true;
            }
        }
    }

    protected void Register_B_Click(object sender, EventArgs e)
    {

    }
}
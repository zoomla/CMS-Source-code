using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Common_Dialog_SelGroup : System.Web.UI.Page
{
    B_User buser = new B_User();
    public string GroupType { get { return Request.QueryString["type"] ?? ""; } }
    public string Source { get { return Request.QueryString["source"] ?? "user"; } }
    public string Config { get { return Request.QueryString["config"] ?? ""; } }
    //selgroup.aspx?source=plat#atuser
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        switch (Source)
        {
            case "plat":
                platgroup_tab.Visible = true;
                break;
            case "oa":
                structgroup_tab.Visible = true;
                break;
            case "user":
            default:
                group_tab.Visible = true;
                break;
        }
    }
}
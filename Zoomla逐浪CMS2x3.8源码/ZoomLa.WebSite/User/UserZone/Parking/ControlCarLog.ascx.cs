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
using ZoomLa.Sns;
using System.Collections.Generic;

public partial class User_UserZone_Parking_ControlCarLog : System.Web.UI.UserControl
{
    Parking_BLL pbll = new Parking_BLL();
    public int UserID
    {
        get
        {
            if (ViewState["userid"] != null)
                return int.Parse(ViewState["userid"].ToString());
            else
                return 0;
            }
        set { ViewState["userid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }

    }
    private void Bind()
    {
        List<ZL_Sns_CarLog> list = pbll.GetUserIDCarLog(UserID);
        
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 6;
        cc.CurrentPageIndex = 0;
        Repeater1.DataSource = cc;
        Repeater1.DataBind();
    }
}

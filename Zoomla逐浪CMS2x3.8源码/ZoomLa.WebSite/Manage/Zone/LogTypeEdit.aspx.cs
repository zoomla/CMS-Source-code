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
using BDUBLL;
using BDUModel;
using System.Collections.Generic;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_Zone_LogTypeEdit : CustomerPageAction
{
    //GSManageBLL gsbll = new GSManageBLL();
    B_Admin ubll = new B_Admin();
    private Guid id
    {
        get
        {
            if (ViewState["pid"] != null)
                return new Guid(ViewState["pid"].ToString());
            else
                return Guid.Empty;
        }
        set
        {
            ViewState["pid"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        ubll.CheckIsLogin();

        if (!IsPostBack)
        {
            ViewState["pid"] = base.Request.QueryString["pid"];
            //GSType gt = gsbll.GetTypeByID(id);
            //this.Nametxt.Text = gt.GSTypeName;
        }
    }

    protected void addbtn_Click(object sender, EventArgs e)
    {
        //GSType gt = gsbll.GetTypeByID(id);
        //gt.GSTypeName = this.Nametxt.Text;
        //gsbll.UpdateType(gt);
        Response.Redirect("LogTypeManage.aspx");
    }
}

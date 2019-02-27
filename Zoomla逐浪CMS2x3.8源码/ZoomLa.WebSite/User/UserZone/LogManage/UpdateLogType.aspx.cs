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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class UpdateLogType : Page
{
    private new Guid ID
    {
        get
        {
            if (ViewState["ID"] != null)
                return new Guid(ViewState["ID"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["ID"] = value;
        }
    }
    LogManageBLL logbll = new LogManageBLL();
    B_User buser = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        if (!IsPostBack)
        {
            if (Request.QueryString["logTypeID"] != null)
            {
                ViewState["ID"] = Request.QueryString["logTypeID"];

                GetLogType();
              
            }
        }
    }

    private void GetLogType()
    {
        UserLogType logType = logbll.GetLogTypeByID(ID);
        if (logType != null)
        {
            txtLogTypeName.Text = logType.LogTypeName;
            dropReadAre.SelectedValue = logType.LogArea.ToString();
            if (logType.LogArea == 2)
            {
                this.txtPWD.Text = logType.LogTypePWD;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "spanDisy();", true);
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UserLogType logType = new UserLogType();
        logType.ID = ID;
        logType.LogArea = int.Parse(this.dropReadAre.SelectedValue);
        logType.LogTypeName = this.txtLogTypeName.Text;
        if (logType.LogArea == 2)
        {
            logType.LogTypePWD = txtPWD.Text;
        }
        else
            logType.LogTypePWD = null;
        logbll.UpdateLogType(logType);
        Page.ClientScript.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='" + ID + "';window.parent.hidePopWin(true);</script>");
    }
}


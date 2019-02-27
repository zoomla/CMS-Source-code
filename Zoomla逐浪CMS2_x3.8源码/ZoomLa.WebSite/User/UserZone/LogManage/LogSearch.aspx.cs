using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDUBLL;
using System.Data;

public partial class User_UserZone_LogManage_LogSearch : System.Web.UI.Page
{
    LogManageBLL lmb = new LogManageBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<BDUModel.UserLogType> list = new List<BDUModel.UserLogType>();
            list = lmb.GetLogTypeByUserID(0);
            LogType.DataSource = list;
            LogType.DataTextField = "logTypeName";
            LogType.DataValueField = "iD";
            LogType.DataBind();
            LogType.Items.Insert(0, new ListItem("全部", ""));
        }
    }
}
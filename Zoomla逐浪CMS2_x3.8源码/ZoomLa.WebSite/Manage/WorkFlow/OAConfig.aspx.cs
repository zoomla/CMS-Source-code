using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using System.Runtime.Serialization.Json;

public partial class Manage_WorkFlow_OAConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindModel.Text = OAConfig.ModelID.ToString();
            UNameConfigR.SelectedValue = OAConfig.UNameConfig;
            allowMsgR.Checked = OAConfig.AllowMsg == "1" ? true : false; ;
            allowUIR.Checked = OAConfig.AllowUI == "1" ? true : false;
            oaTitleT.Text = OAConfig.OATitle;
            MailSize_T.Text = OAConfig.MailSize.ToString();
            logoT.Text = OAConfig.OALogo;
            GetNodes();
            Leader_T.Text = OAConfig.LeaderSignTemplate;
            Parter_T.Text = OAConfig.ParterSignTemplate;
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li class='active'>OA配置</li>");
    }

    private void GetNodes()
    {
        OAConfig.ReInstance();
        DataTable dt = JsonHelper.JsonToDT(OAConfig.NodeMap);
        RPT.DataSource = dt;
        RPT.DataBind();
        
    }
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        OAConfig.ModelID = Convert.ToInt32(bindModel.Text);
        OAConfig.UNameConfig = UNameConfigR.SelectedValue;
        OAConfig.AllowMsg = allowMsgR.Checked ? "1" : "0";
        OAConfig.AllowUI = allowUIR.Checked ? "1" : "0";
        OAConfig.OATitle = oaTitleT.Text;
        OAConfig.OALogo = logoT.Text;
        OAConfig.MailSize = Convert.ToInt32(MailSize_T.Text.Trim());
        OAConfig.LeaderSignTemplate = Leader_T.Text;
        OAConfig.ParterSignTemplate = Parter_T.Text;
        OAConfig.Update();
        function.WriteSuccessMsg("保存成功");
    }
    protected void NodeSavBtn_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("NodeName");
        dt.Columns.Add("NodeID");
        for (int i = 0; i < RPT.Items.Count; i++)
        {
            string[] tempstr = new string[] { ((HiddenField)RPT.Items[i].FindControl("ID_Hid")).Value, ((TextBox)RPT.Items[i].FindControl("tbNodeName")).Text, ((TextBox)RPT.Items[i].FindControl("tbNodeID")).Text };
            dt.Rows.Add(tempstr);
        }
        OAConfig.NodeMap = JsonHelper.JsonSerialDataTable(dt);
        OAConfig.Update();
    }
}
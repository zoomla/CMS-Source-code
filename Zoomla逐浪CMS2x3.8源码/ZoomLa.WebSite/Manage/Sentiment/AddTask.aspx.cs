using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Sentiment;
using ZoomLa.Common;
using ZoomLa.Model.Sentiment;
using ZoomLa.SQLDAL;

public partial class Manage_Sentiment_AddSentiment : System.Web.UI.Page
{
    B_Sen_Task senBll = new B_Sen_Task();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Default.aspx'>企业办公</a></li><li><a href='Default.aspx'>舆情监测</a></li><li><a href='TaskList.aspx'>监测任务</a></li><li class='active'>新建任务</li>");
        }
    }
    public void MyBind()
    {
        if (Mid > 0)
        {
            M_Sen_Task senMod = senBll.SelReturnModel(Mid);
            Title_T.Text = senMod.Title;
            Condition_T.Text = senMod.Condition;
            SuitKey_T.Text = senMod.SuitKey;
            SType_DP.SelectedValue = senMod.SType;
            Status_Chk.Checked = senMod.Status == 1;
            function.Script(this, "SetChkVal('source_chk','" + senMod.Source + "');");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Sen_Task senMod = new M_Sen_Task();
        if (Mid > 0)
        {
            senMod = senBll.SelReturnModel(Mid);
        }
        senMod.Title = Title_T.Text;
        senMod.Source = Request.Form["source_chk"];
        senMod.Condition = Condition_T.Text;
        senMod.SuitKey = SuitKey_T.Text;
        senMod.SType = SType_DP.SelectedValue;
        senMod.Status = Status_Chk.Checked ? 1 : 0;
        if (Mid > 0)
        {
            senBll.UpdateByID(senMod);
        }
        else
        {
            senBll.Insert(senMod);
        }
        Response.Redirect("TaskList.aspx");
    }
}
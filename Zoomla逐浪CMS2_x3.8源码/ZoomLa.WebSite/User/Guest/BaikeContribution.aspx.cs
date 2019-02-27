using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
//未审核下可删除和修改,否则只可浏览
public partial class User_Guest_BaikeContribution : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_BaikeEdit editBll = new B_BaikeEdit();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = editBll.U_Sel(mu.UserID, Convert.ToInt32(Filter_DP.SelectedValue));
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetStatus()
    {
        int status = DataConvert.CLng(Eval("Status"));
        return editBll.GetStatus(status);
    }
    protected string GetBasic(string type)
    {
        return "";
    }
    protected void Filter_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del":
                editBll.Del(Convert.ToInt32(e.CommandArgument));
                break;
        }
        MyBind();
    }
}